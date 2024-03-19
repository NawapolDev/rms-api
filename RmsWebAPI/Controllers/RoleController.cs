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
    public class RoleController : ControllerBase
    {
        private readonly IConfiguration _config;
        public RoleController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet("get")]
        public IActionResult Get()
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT *" +
                    " FROM public.role";
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
        public IActionResult Search([FromRoute] string name)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT *" +
                    " FROM public.role" +
                    " WHERE role_name LIKE '%" + name + "%'";
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
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(Role role)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "INSERT INTO public.role(role_name, active, createdate, createby)" +
                    " VALUES(@role_name, @active, @createdate, @createby)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection(), null);
                cmd.Parameters.Add("@role_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = role.Role_name;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = role.Active;
                cmd.Parameters.AddWithValue("@createdate", role.Createdate);
                cmd.Parameters.Add("@createby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = role.Createby;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Insert role succesfully.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromRoute] string id, Role role)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.role" +
                    " SET role_name = @type_name," +
                    " active = @active," +
                    " WHERE role_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@role_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = role.Role_name;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = role.Active;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
