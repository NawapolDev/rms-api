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
    public class RoomController : ControllerBase
    {
        private readonly IConfiguration _config;
        public RoomController(IConfiguration config)
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
                    " FROM public.room";
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
                    " FROM public.room" +
                    " WHERE room_name LIKE '%" + name + "%'";
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
        [Route("getwithtype")]
        public IActionResult GetWithType()
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Open();
            try
            {
                string query = "SELECT room_id, room_name, public.room.type_id,type_name, public.room.createdate, public.room.createby, public.room.active, public.room.modifieddate, public.room.modifiedby" +
                    " FROM public.room" +
                    " INNER JOIN public.roomtype ON public.room.type_id = public.roomtype.type_id;";

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
        public IActionResult Insert(Room room)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "INSERT INTO public.room(room_name, type_id, createdate, createby, active)" +
                    " VALUES(@room_name, @type_id, @createdate, @createby, @active)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection(), null);
                cmd.Parameters.Add("@room_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = room.Room_name;
                cmd.Parameters.Add("@type_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = room.Typeid;
                cmd.Parameters.AddWithValue("@createdate", room.Createdate);
                cmd.Parameters.Add("@createby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = room.Createby;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = room.Active;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Insert room succesfully.");
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update([FromRoute]string id,Room room) 
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.room" +
                    " SET room_name=@room_name, type_id=@type_id, active=@active, modifieddate=@modifieddate, modifiedby=@modifiedby" +
                    " WHERE room_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.Add("@room_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = room.Room_name;
                cmd.Parameters.Add("@type_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = room.Typeid;
                cmd.Parameters.Add("@active", NpgsqlTypes.NpgsqlDbType.Char).Value = room.Active;
                cmd.Parameters.AddWithValue("@modifieddate", room.Modifieddate);
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = room.Modifiedby;
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
