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
    public class MemberController : ControllerBase
    {
        private readonly IConfiguration _config;
        public MemberController(IConfiguration config)
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
                    " FROM public.member";
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
                    " FROM public.member" +
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
        public IActionResult Insert(Member mem)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "INSERT INTO public.member" +
                    " (firstname, lastname, username, password, idcard, phone, email, address, subdistrict, district, province, country, createdate, active, postcode)" +
                    " VALUES (@firstname, @lastname, @username, @password, @idcard, @phone, @email, @address, @subdistrict, @district, @province, @country, @createdate, @active, @postcode)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@firstname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Firstname;
                cmd.Parameters.Add("@lastname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Lastname;
                cmd.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Username;
                cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Password;
                cmd.Parameters.Add("@idcard", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Idcard;
                cmd.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Phone;
                cmd.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Email;
                cmd.Parameters.Add("@address", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Address;
                cmd.Parameters.Add("@subdistrict", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Subdistrict;
                cmd.Parameters.Add("@district", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.District;
                cmd.Parameters.Add("@province", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Province;
                cmd.Parameters.Add("@country", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Country;
                cmd.Parameters.Add("@postcode", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Postcode;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = mem.Active;
                cmd.Parameters.AddWithValue("@createdate", mem.Createdate);
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Insert member succesfully.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromRoute] string id, Member mem)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.member" +
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
                    " postcode = @postcode," +
                    " active = @active," +
                    " modifiedby = @modifiedby," +
                    " modifieddate = @modifieddate" +
                    " WHERE mb_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@firstname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Firstname;
                cmd.Parameters.Add("@lastname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Lastname;
                cmd.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Username;
                cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Password;
                cmd.Parameters.Add("@idcard", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Idcard;
                cmd.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Phone;
                cmd.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Email;
                cmd.Parameters.Add("@address", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Address;
                cmd.Parameters.Add("@subdistrict", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Subdistrict;
                cmd.Parameters.Add("@district", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.District;
                cmd.Parameters.Add("@province", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Province;
                cmd.Parameters.Add("@country", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Country;
                cmd.Parameters.Add("@postcode", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Postcode;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = mem.Active;
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mem.Modifiedby;
                cmd.Parameters.AddWithValue("@modifieddate", mem.Modifieddate);
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
