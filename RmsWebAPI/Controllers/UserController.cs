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
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult Get()
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT *" +
                    " FROM public.user";
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
        [Route("getbyid/{id}")]
        public IActionResult GetById([FromRoute]string id)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT *" +
                    " FROM public.user" +
                    " WHERE u_id = '" + id + "'";
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
                    " FROM public.user" +
                    " WHERE firstname LIKE '%" + name + "%'";
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
        public IActionResult Insert(User user)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "INSERT INTO public.user" +
                    " (firstname, lastname, username, password, idcard, phone, email, address, subdistrict, district, province, country, createdate, active, role_id)" +
                    " VALUES (@firstname, @lastname, @username, @password, @idcard, @phone, @email, @address, @subdistrict, @district, @province, @country, @createdate, @active, @role_id)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@firstname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Firstname;
                cmd.Parameters.Add("@lastname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Lastname;
                cmd.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Username;
                cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Password;
                cmd.Parameters.Add("@idcard", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Idcard;
                cmd.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Phone;
                cmd.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Email;
                cmd.Parameters.Add("@address", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Address;
                cmd.Parameters.Add("@subdistrict", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Subdistrict;
                cmd.Parameters.Add("@district", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.District;
                cmd.Parameters.Add("@province", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Province;
                cmd.Parameters.Add("@country", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Country;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = user.Active;
                cmd.Parameters.Add("@role_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = user.Role_id;
                cmd.Parameters.AddWithValue("@createdate", user.Createdate);
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Insert user succesfully.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromRoute] string id, User user)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.user" +
                    " SET" +
                    " firstname = @firstname," +
                    " lastname = @lastname," +
                    " idcard = @idcard," +
                    " phone = @phone," +
                    " email = @email," +
                    " address = @address," +
                    " subdistrict = @subdistrict," +
                    " district = @district," +
                    " province = @province," +
                    " country = @country," +
                    " active = @active," +
                    " modifiedby = @modifiedby," +
                    " modifieddate = @modifieddate" +
                    " WHERE mb_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@firstname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Firstname;
                cmd.Parameters.Add("@lastname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Lastname;
                cmd.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Username;
                cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Password;
                cmd.Parameters.Add("@idcard", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Idcard;
                cmd.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Phone;
                cmd.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Email;
                cmd.Parameters.Add("@address", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Address;
                cmd.Parameters.Add("@subdistrict", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Subdistrict;
                cmd.Parameters.Add("@district", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.District;
                cmd.Parameters.Add("@province", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Province;
                cmd.Parameters.Add("@country", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Country;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = user.Active;
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.Modifiedby;
                cmd.Parameters.AddWithValue("@modifieddate", user.Modifieddate);
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
