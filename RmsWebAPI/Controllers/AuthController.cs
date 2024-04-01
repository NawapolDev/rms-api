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
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Open();
            try
            {
                string query = "SELECT *, role.role_name" +
                    " FROM public.user" +
                    " INNER JOIN public.role ON public.role.role_id = public.user.role_id" +
                    " WHERE username = '" + user.Username + "' AND public.user.password = '" + user.Password + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                db.Close();
                if (dt.Rows.Count == 0)
                {
                    return NotFound("User not found.");
                }
                if (dt.Rows.Count > 0)
                {
                    char activeValue = Convert.ToChar(dt.Rows[0]["active"]);
                    if (activeValue == '0')
                    {
                        return Forbid();
                    }
                }
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok(JsonConvert.SerializeObject(dt, Formatting.Indented));
        }
    }
}
