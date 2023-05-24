using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Point_s_Management
{
    internal class clsDatabase
    {
        public static SqlConnection connect;

        public static bool OpenConnection()
        {
            try
            {
                connect = new SqlConnection(@"Server=.\sqlexpress;Database=SinhVien; 
                            Integrated Security = true");
                connect.Open();
            }

            catch
            {
                return false;
            }

            return true;
        }

        public static bool CloseConnection()
        {
            try
            {
                connect.Close();
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
