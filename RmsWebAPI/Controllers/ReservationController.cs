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
    public class ReservationController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ReservationController(IConfiguration config)
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
                    " FROM public.reservation";
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
                    " FROM public.reservation" +
                    " WHERE rsv_id = '" + id + "'";
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
                string query = "SELECT reservation.*, user.firstname" +
                    " FROM public.reservation" +
                    " INNER JOIN user on reservation.u_id = user.u_id" +
                    " WHERE user.firstname LIKE '%" + name + "%'";
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
        public IActionResult Insert(Reservation rsv)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "INSERT INTO public.reservation(u_id, room_id, checkintime, checkouttime, checkindate, createdate, createby, totalprice, approve)" +
                    " VALUES (@u_id, @room_id, @checkintime, @checkouttime, @checkindate, @createdate, @createby, @totalprice, @approve)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection(), null);
                cmd.Parameters.Add("@u_id", NpgsqlTypes.NpgsqlDbType.Uuid).Value = rsv.u_id;
                cmd.Parameters.Add("@room_id", NpgsqlTypes.NpgsqlDbType.Uuid).Value = rsv.Room_id;
                cmd.Parameters.AddWithValue("@checkintime", rsv.CheckInTime);
                cmd.Parameters.AddWithValue("@checkouttime", rsv.CheckOutTime);
                cmd.Parameters.AddWithValue("@checkindate", rsv.CheckInDate);
                cmd.Parameters.AddWithValue("@createdate", rsv.CreateDate);
                cmd.Parameters.Add("@createby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rsv.CreateBy;
                cmd.Parameters.Add("@totalprice", NpgsqlTypes.NpgsqlDbType.Integer).Value = rsv.Totalprice;
                cmd.Parameters.Add("@approve", NpgsqlTypes.NpgsqlDbType.Char).Value = rsv.Approve;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Insert reservation succesfully.");
        }

        [HttpPut]
        [Route("update/payment/{id}")]
        public IActionResult UpdatePayment(Reservation rsv, string id)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.reservation" +
                    " SET modifieddate = @modifieddate," +
                    " modifiedby = @modifiedby," +
                    " paymentslip_file = @paymentslip_file," +
                    " paymentslip_url = @paymentslip_url" +
                    " WHERE rsv_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@modifieddate", rsv.ModifiedDate);
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rsv.ModifiedBy;
                cmd.Parameters.Add("@paymentslip_file", NpgsqlTypes.NpgsqlDbType.Bytea).Value = rsv.PaymentSlip_file;
                cmd.Parameters.Add("@paymentslip_url", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rsv.PaymentSlip_url;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Updated payment of id : " + id);
        }

        [HttpPut]
        [Route("update/approve/{id}")]
        public IActionResult UpdateApprove(Reservation rsv, string id)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                string query = "UPDATE public.reservation" +
                    " SET modifieddate = @modifieddate," +
                    " modifiedby = @modifiedby," +
                    " approve = @approve," +
                    " approveby = @approveby" +
                    " WHERE rsv_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@modifieddate", rsv.ModifiedDate);
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rsv.ModifiedBy;
                cmd.Parameters.Add("@approve", NpgsqlTypes.NpgsqlDbType.Bytea).Value = rsv.Approve;
                cmd.Parameters.Add("@approveby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rsv.Approveby;
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                return BadRequest(ex.Message);
            }
            return Ok("Updated approve of id : " + id);
        }
    }
}
