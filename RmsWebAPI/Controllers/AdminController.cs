using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using RmsWebAPI.Class;
using RmsWebAPI.Models;
using System.Data;

namespace RmsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IConfiguration _config;

        public AdminController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();

            db.Open();
            try
            {
                string query = "SELECT *" +
                    " FROM public.admin";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection(), null);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(dt, Formatting.Indented));
        }

        [HttpGet]
        [Route("search/{name}")]
        public IActionResult Search([FromRoute]string name)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();

            db.Open();
            try
            {
                string query = "SELECT *" +
                    " FROM public.admin" +
                    " WHERE Firstname LIKE '%" + name + "%'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection(), null);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                db.Close();
            }
            catch(Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(dt, Formatting.Indented));
        }

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(Admin admin)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Open();
            try
            {
                string query = "INSERT INTO public.admin(firstname, lastname, username, password, createdate, active)" +
                    " VALUES(@firstname, @lastname, @username, @password, @createdate, @active)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@firstname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = admin.Firstname;
                cmd.Parameters.Add("@lastname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = admin.Lastname;
                cmd.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar).Value = admin.Username;
                cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = admin.Password;
                cmd.Parameters.AddWithValue("@createdate", admin.Createdate);
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = admin.Active;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                db.Close();
            }
            catch(Exception ex) 
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromRoute]string id,Admin admin)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.admin" +
                    " SET active = @active" +
                    " WHERE adm_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = admin.Active;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Update admin id : '" + id + "' succesfully.");
        }
    }
}
