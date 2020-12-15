using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace ClassLibraryBookStore
{
    public class Koneksi
    {
        private MySqlConnection koneksiDataBase;

        #region Constructors
        public Koneksi()
        {
            KoneksiDataBase = new MySqlConnection();
            KoneksiDataBase.ConnectionString = ConfigurationManager.ConnectionStrings["namakoneksi"].ConnectionString;
            Connect();
        }

        public Koneksi(string namaServer, string namaDatabase, string username, string password)
        {
            string strConnectionString = "";
            if (password != "")
            {
                strConnectionString = "Server=" + namaServer + ";Database=" + namaDatabase + ";Uid=" + username + ";Pwd=" + password + ";";

            }
            else
            {
                strConnectionString = "Server=" + namaServer + ";Database=" + namaDatabase + ";Uid=" + username + ";";
            }
            this.KoneksiDataBase = new MySqlConnection();
            this.KoneksiDataBase.ConnectionString = strConnectionString;

            //panggil methode Connect
            this.Connect();
            UpdateAppConfig(strConnectionString);
        }
        #endregion
        #region Properties
        public MySqlConnection KoneksiDataBase { get => koneksiDataBase; set => koneksiDataBase = value; }
        #endregion

        #region Methods

        public void Connect()
        {
            if (koneksiDataBase.State == System.Data.ConnectionState.Open)
            {
                KoneksiDataBase.Close();
            }
            KoneksiDataBase.Open();
        }

        public void UpdateAppConfig(string con)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["namakoneksi"].ConnectionString = con;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public static void JalankanPerintahDML(string sql)
        {
            Koneksi k = new Koneksi();
            k.Connect();
            MySqlCommand c = new MySqlCommand(sql, k.KoneksiDataBase);
            c.ExecuteNonQuery();
        }

        public static MySqlDataReader JalankanPerintahQuery(string sql)
        {
            Koneksi k = new Koneksi();
            MySqlCommand c = new MySqlCommand(sql, k.KoneksiDataBase);
            MySqlDataReader hasil = c.ExecuteReader();
            return hasil;
        }
        #endregion

        #region UserBaru
        public static string GetNamaServer()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["namakoneksi"].ConnectionString;

            return con.DataSource;
        }
        public static string GetNamaDatabase()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["namakoneksi"].ConnectionString;

            return con.Database;
        }
        #endregion
    }
}
