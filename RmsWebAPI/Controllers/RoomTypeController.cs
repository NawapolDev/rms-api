using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using RmsWebAPI.Class;
using RmsWebAPI.Models;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace RmsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IConfiguration _config;
        public RoomTypeController(IConfiguration config)
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
                    " FROM public.roomtype" +
                    " ORDER BY room_size DESC";
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
                    " FROM public.roomtype" +
                    " WHERE type_name LIKE '%" + name + "%'";
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
        public IActionResult Insert(RoomType type)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Open();

            try
            {
                string dupquery = "SELECT type_name" +
                    " FROM public.roomtype" +
                    " WHERE type_name = '" + type.Type_name + "' AND room_size = " + type.Room_size;
                NpgsqlCommand dupCmd = new NpgsqlCommand(dupquery, db.GetConnection(), null);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(dupCmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    db.Close();
                    return StatusCode(409,  "Duplicate type name.");
                }

                string query = "INSERT INTO public.roomtype(type_name, room_size, quantity, price, active, createdate, createby)" +
                    " VALUES(@type_name, @room_size, @quantity, @price, @active, @createdate, @createby)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection(), null);
                cmd.Parameters.Add("@type_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = type.Type_name;
                cmd.Parameters.Add("@room_size", NpgsqlTypes.NpgsqlDbType.Integer).Value = type.Room_size;
                cmd.Parameters.Add("@quantity", NpgsqlTypes.NpgsqlDbType.Integer).Value = type.Quantity;
                cmd.Parameters.Add("@price", NpgsqlTypes.NpgsqlDbType.Integer).Value = type.Price;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = type.Active;
                cmd.Parameters.AddWithValue("@createdate", type.Createdate);
                cmd.Parameters.Add("@createby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = type.Createby;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Insert room type succesfully.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromRoute] string id, RoomType type)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Open();
            try
            {
                string dupquery = "SELECT type_name" +
                    " FROM public.roomtype" +
                    " WHERE type_name = '" + type.Type_name + "' AND room_size = " + type.Room_size;
                NpgsqlCommand dupCmd = new NpgsqlCommand(dupquery, db.GetConnection(), null);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(dupCmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    db.Close();
                    return StatusCode(409, "Duplicate type name.");
                }

                string query = "UPDATE public.roomtype" +
                    " SET type_name = @type_name," +
                    " room_size = @room_size," +
                    " quantity = @quantity," +
                    " price = @price," +
                    " active = @active," +
                    " modifieddate = @modifieddate," +
                    " modifiedby = @modifiedby" +
                    " WHERE type_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@type_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = type.Type_name;
                cmd.Parameters.Add("@room_size", NpgsqlTypes.NpgsqlDbType.Integer).Value = type.Room_size;
                cmd.Parameters.Add("@quantity", NpgsqlTypes.NpgsqlDbType.Integer).Value = type.Quantity;
                cmd.Parameters.Add("@price", NpgsqlTypes.NpgsqlDbType.Integer).Value = type.Price;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = type.Active;
                cmd.Parameters.AddWithValue("@modifieddate", type.Modifieddate);
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = type.Modifiedby;
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
