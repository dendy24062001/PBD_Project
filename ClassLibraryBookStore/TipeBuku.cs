using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClassLibraryBookStore
{
    public class TipeBuku
    {
        private string idTipeBuku;
        private string jenisBuku;

        public TipeBuku(string idTipeBuku, string jenisBuku)
        {
            IdTipeBuku = idTipeBuku;
            JenisBuku = jenisBuku;
        }

        public string IdTipeBuku { get => idTipeBuku; set => idTipeBuku = value; }
        public string JenisBuku { get => jenisBuku; set => jenisBuku = value; }


        #region Method
        public static void TambahData(TipeBuku t)
        {
            string sql = "Insert into tipebuku(IdTipeBuku, JenisBuku) values('" + t.IdTipeBuku + "','" + t.JenisBuku.Replace("'", "\\'") + "')";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static void UbahData(TipeBuku t)
        {
            string sql = "update tipebuku set JenisBuku='" + t.JenisBuku.Replace("'", "\\'") + "' where idTipeBuku='" + t.IdTipeBuku + "'";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static string HapusData(TipeBuku t)
        {
            string sql = "DELETE FROM tipebuku WHERE idTipeBuku = '" + t.IdTipeBuku + "'";

            try
            {
                Koneksi.JalankanPerintahDML(sql);
                return "1";
            }
            catch (MySqlException ex)
            {
                return ex.Message + ". Perintah sql : " + sql;
            }
        }

        public static List<TipeBuku> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select * from tipebuku";
            }
            else
            {
                sql = "select * from tipebuku where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<TipeBuku> listOfKategori = new List<TipeBuku>();

            while (hasil.Read() == true)
            {
                TipeBuku t = new TipeBuku(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString());
                listOfKategori.Add(t);
            }
            return listOfKategori;
        }

        public static string GenerateKode()
        {
            string sql = "select max(IdTipeBuku) from tipebuku";
            string hasilKode = "";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                int kodeTerbaru = int.Parse(hasil.GetValue(0).ToString()) + 1;

                hasilKode = kodeTerbaru.ToString();
            }
            else
            {
                hasilKode = "01";
            }
            return hasilKode;
        }
        #endregion
    }
}