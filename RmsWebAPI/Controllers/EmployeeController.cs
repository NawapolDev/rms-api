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
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;
        public EmployeeController(IConfiguration config)
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
                    " FROM public.employee";
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
            db.Close();
            try
            {
                string query = "SELECT *" +
                    " FROM public.employee" +
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
        public IActionResult Insert(Employee emp)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "INSERT INTO public.employee" +
                    " (firstname, lastname, username, password, idcard, phone, email, address, subdistrict, district, province, country, createdate, createby, active, postcode)" +
                    " VALUES (@firstname, @lastname, @username, @password, @idcard, @phone, @email, @address, @subdistrict, @district, @province, @country, @createdate, @createby, @active, @postcode)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@firstname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Firstname;
                cmd.Parameters.Add("@lastname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Lastname;
                cmd.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Username;
                cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Password;
                cmd.Parameters.Add("@idcard", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Idcard;
                cmd.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Phone;
                cmd.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Email;
                cmd.Parameters.Add("@address", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Address;
                cmd.Parameters.Add("@subdistrict", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Subdistrict;
                cmd.Parameters.Add("@district", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.District;
                cmd.Parameters.Add("@province", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Province;
                cmd.Parameters.Add("@country", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Country;
                cmd.Parameters.Add("@postcode", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Postcode;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = emp.Active;
                cmd.Parameters.AddWithValue("@createdate", emp.Createdate);
                cmd.Parameters.Add("@createby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Createby;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Insert employee succesfully.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromRoute]string id, Employee emp)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.employee" +
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
                    " WHERE emp_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@firstname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Firstname;
                cmd.Parameters.Add("@lastname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Lastname;
                cmd.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Username;
                cmd.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Password;
                cmd.Parameters.Add("@idcard", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Idcard;
                cmd.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Phone;
                cmd.Parameters.Add("@email", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Email;
                cmd.Parameters.Add("@address", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Address;
                cmd.Parameters.Add("@subdistrict", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Subdistrict;
                cmd.Parameters.Add("@district", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.District;
                cmd.Parameters.Add("@province", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Province;
                cmd.Parameters.Add("@country", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Country;
                cmd.Parameters.Add("@postcode", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Postcode;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = emp.Active;
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = emp.Modifiedby;
                cmd.Parameters.AddWithValue("@modifieddate", emp.Modifieddate);
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
