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
                    " FROM public.reservation" +
                    " ORDER BY createdate DESC, approve ASC";
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
                string query = "SELECT rsv_id, public.reservation.u_id, public.reservation.room_id, checkintime, checkouttime, checkindate,  public.reservation.createdate,  public.reservation.createby, totalprice, approve, approveby,  public.reservation.modifieddate,  public.reservation.modifiedby, paymentslip_file, paymentslip_url, public.user.firstname as u_firstname, public.user.phone as u_phone, room.room_name as r_name, room.type_id, roomtype.type_name" +
                    " FROM public.reservation" +
                    " LEFT JOIN public.user ON reservation.u_id = public.user.u_id" +
                    " LEFT JOIN public.room ON reservation.room_id = room.room_id" +
                    " LEFT JOIN public.roomtype ON roomtype.type_id = room.type_id" +
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
        [Route("getbyuserid/{id}")]
        public IActionResult GetByUserId([FromRoute]string id)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT rsv_id, public.reservation.u_id, public.reservation.room_id, checkintime, checkouttime, checkindate,  public.reservation.createdate,  public.reservation.createby, totalprice, approve, approveby,  public.reservation.modifieddate,  public.reservation.modifiedby, paymentslip_file, paymentslip_url, public.user.firstname as u_firstname, public.user.phone as u_phone, room.room_name as r_name, room.type_id, roomtype.type_name" +
                    " FROM public.reservation" +
                    " LEFT JOIN public.user ON reservation.u_id = public.user.u_id" +
                    " LEFT JOIN public.room ON reservation.room_id = room.room_id" +
                    " LEFT JOIN public.roomtype ON roomtype.type_id = room.type_id" +
                    " WHERE public.reservation.u_id = '" + id + "'";
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
        [Route("getunpaid/{id}")]
        public IActionResult GetUnpaidByUserId([FromRoute]string id)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT rsv_id, public.reservation.u_id, public.reservation.room_id, checkintime, checkouttime, checkindate,  public.reservation.createdate,  public.reservation.createby, totalprice, approve, approveby,  public.reservation.modifieddate,  public.reservation.modifiedby, paymentslip_file, paymentslip_url, public.user.firstname as u_firstname, public.user.phone as u_phone, room.room_name as r_name, room.type_id, roomtype.type_name" +
                    " FROM public.reservation" +
                    " LEFT JOIN public.user ON reservation.u_id = public.user.u_id" +
                    " LEFT JOIN public.room ON reservation.room_id = room.room_id" +
                    " LEFT JOIN public.roomtype ON roomtype.type_id = room.type_id" +
                    " WHERE public.reservation.u_id = '" + id + "'" +
                    " AND public.reservation.paymentslip_file IS NULL" +
                    " ORDER BY reservation.createdate DESC";
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
        [Route("getunapprove/{id}")]
        public IActionResult GetUnapproveByUserId([FromRoute]string id)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT rsv_id, public.reservation.u_id, public.reservation.room_id, checkintime, checkouttime, checkindate,  public.reservation.createdate,  public.reservation.createby, totalprice, approve, approveby,  public.reservation.modifieddate,  public.reservation.modifiedby, paymentslip_file, paymentslip_url, public.user.firstname as u_firstname, public.user.phone as u_phone, room.room_name as r_name, room.type_id, roomtype.type_name" +
                    " FROM public.reservation" +
                    " LEFT JOIN public.user ON reservation.u_id = public.user.u_id" +
                    " LEFT JOIN public.room ON reservation.room_id = room.room_id" +
                    " LEFT JOIN public.roomtype ON roomtype.type_id = room.type_id" +
                    " WHERE public.reservation.u_id = '" + id + "'" +
                    " AND public.reservation.paymentslip_file IS NOT NULL" +
                    " AND reservation.approve = '0'" +
                    " ORDER BY reservation.createdate DESC";
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
        [Route("getapprove/{id}")]
        public IActionResult GetApproveByUserId([FromRoute]string id)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Close();
            try
            {
                string query = "SELECT rsv_id, public.reservation.u_id, public.reservation.room_id, checkintime, checkouttime, checkindate,  public.reservation.createdate,  public.reservation.createby, totalprice, approve, approveby,  public.reservation.modifieddate,  public.reservation.modifiedby, paymentslip_file, paymentslip_url, public.user.firstname as u_firstname, public.user.phone as u_phone, room.room_name as r_name, room.type_id, roomtype.type_name" +
                    " FROM public.reservation" +
                    " LEFT JOIN public.user ON reservation.u_id = public.user.u_id" +
                    " LEFT JOIN public.room ON reservation.room_id = room.room_id" +
                    " LEFT JOIN public.roomtype ON roomtype.type_id = room.type_id" +
                    " WHERE public.reservation.u_id = '" + id + "'" +
                    " AND public.reservation.paymentslip_file IS NOT NULL" +
                    " AND reservation.approve = '1'" +
                    " ORDER BY reservation.createdate DESC";
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
                // cmd.Parameters.Add("@checkintime", NpgsqlTypes.NpgsqlDbType.Time).Value = rsv.CheckInTime;
                // cmd.Parameters.Add("@checkouttime", NpgsqlTypes.NpgsqlDbType.Time).Value = rsv.CheckOutTime;
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
        public async Task<IActionResult> UpdatePayment([FromRoute]string id, [FromForm]Reservation rsv)
        {
            if (rsv == null || rsv.File == null || rsv.File.Length == 0)
            return BadRequest("No file uploaded or invalid data.");

            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            db.Open();
            try
            {
                // Read file content into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await rsv.File.CopyToAsync(memoryStream);
                    rsv.PaymentSlip_file = memoryStream.ToArray();
                }

                string query = "UPDATE public.reservation" +
                    " SET modifieddate = @modifieddate," +
                    " modifiedby = @modifiedby," +
                    " paymentslip_file = @paymentslip_file" +
                    " WHERE rsv_id = '" + id + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@modifieddate", rsv.ModifiedDate);
                cmd.Parameters.Add("@modifiedby", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rsv.ModifiedBy;
                cmd.Parameters.Add("@paymentslip_file", NpgsqlTypes.NpgsqlDbType.Bytea).Value = rsv.PaymentSlip_file;
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
                cmd.Parameters.Add("@approve", NpgsqlTypes.NpgsqlDbType.Char).Value = rsv.Approve;
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

        [HttpPost]
        [Route("available")]
        public IActionResult FindAvailable([FromForm]int type_id, [FromForm]string checkindate, [FromForm]string checkintime, [FromForm]string checkouttime)
        {
            DBManager db = new DBManager(_config["ConnectionStrings:rmsdb"]);
            DataTable dt = new DataTable();
            db.Open();
            try
            {
                string query = "SELECT *" +
                    " FROM room" +
                    " WHERE type_id = " + type_id +
                    " AND active = '1'" +
                    " AND room_id NOT IN (" +
                    "   SELECT room_id" +
                    "   FROM reservation" +
                    "   WHERE checkindate = '" + checkindate + "'" +
                    "   AND (" +
                    "       (checkintime >= '" + checkintime + "' AND checkintime < '" + checkouttime + "')" +
                    "       OR (checkouttime > '" + checkintime + "' AND checkouttime <= '" + checkouttime + "')" +
                    "       OR (checkintime <= '" + checkintime + "' AND checkouttime >= '" + checkouttime + "')" +
                    "   )" +
                    ");";
                NpgsqlCommand cmd = new NpgsqlCommand(query, db.GetConnection());
                //cmd.Parameters.Add("@type_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = type_id;
                //cmd.Parameters.AddWithValue("@checkindate", checkindate);
                //cmd.Parameters.AddWithValue("@checkintime", checkintime);
                //cmd.Parameters.AddWithValue("@checkouttime", checkouttime);
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
    }
}
