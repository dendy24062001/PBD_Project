using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBookStore
{
    public class Pembayaran
    {
        private string idPembayaran;
        private string jenisPembayaran;

        #region Constructors
        public Pembayaran(string idPembayaran, string jenisPembayaran)
        {
            this.idPembayaran = idPembayaran;
            this.jenisPembayaran = jenisPembayaran;
        }
        #endregion

        #region Properties
        public string IdPembayaran { get => idPembayaran; set => idPembayaran = value; }
        public string JenisPembayaran { get => jenisPembayaran; set => jenisPembayaran = value; }
        #endregion

        #region Method
        public static List<Pembayaran> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select * from pembayaran";
            }
            else
            {
                sql = "select * from pembayaran where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Pembayaran> listOfPembayaran = new List<Pembayaran>();

            while (hasil.Read() == true)
            {
                Pembayaran p = new Pembayaran(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString());
                listOfPembayaran.Add(p);
            }
            return listOfPembayaran;
        }
        #endregion
    }
}
