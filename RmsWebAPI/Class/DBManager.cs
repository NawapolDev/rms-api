using Npgsql;

namespace RmsWebAPI.Class
{
    public class DBManager
    {
        protected NpgsqlConnection conn;

        protected NpgsqlCommand sqlCmd;
        public DBManager(string cons)
        {
            conn = new NpgsqlConnection(cons);
        }
        public void Open()
        {
            conn.Open();
        }

        public void Close()
        {
            conn.Close();
        }

        public NpgsqlConnection GetConnection()
        {
            return conn;
        }

        public NpgsqlTransaction BeginTransaction()
        {
            return conn.BeginTransaction();
        }
    }
}
