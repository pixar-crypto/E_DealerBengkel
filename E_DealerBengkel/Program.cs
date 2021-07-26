using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_DealerBengkel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        public static string koneksi()
        {
            return "integrated security=true; data source=localhost;initial catalog=VroomDG";
        }

        public static string toRupiah(int angka)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:N}", angka);
        }

        public static int toAngka(string rupiah)
        {
            return int.Parse(Regex.Replace(rupiah, @",.*|\D", ""));
        }

        public static string autogenerateID(string firstText, string sp)
        {
            string result = "";
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand sqlCmd;
            int num = 0;
            try
            {
                sqlCmd = new SqlCommand(sp, sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCon.Open();
                SqlDataReader dr = sqlCmd.ExecuteReader();
                dr.Read();
                if (dr["idReturn"].ToString() == "")
                {
                    num = 1;
                }
                else
                {
                    num = Int32.Parse(dr["idReturn"].ToString());
                }
                if (num < 10000)
                {
                    result = firstText + "000" + num;
                }
                else if (num < 100000)
                {
                    result = firstText + "00" + num;
                }
                else if (num < 1000000)
                {
                    result = firstText + "0" + num;
                }
                else
                {
                    result = firstText + num;
                }
                dr.Close();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }

            return result;
        }
    }
}
