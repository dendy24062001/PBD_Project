using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClassLibraryBookStore
{
    public class Jabatan
    {
        private string idJabatan;
        private string namaJabatan;

        public Jabatan(string idJabatan, string namaJabatan)
        {
            IdJabatan = idJabatan;
            NamaJabatan = namaJabatan;
        }

        public string IdJabatan { get => idJabatan; set => idJabatan = value; }
        public string NamaJabatan { get => namaJabatan; set => namaJabatan = value; }

        #region Method
        /*public static void TambahData(Jabatan j)
        {
            string sql = "Insert into jabatan(IdJabatan, NamaJabatan) values('" + j.IdJabatan + "','" + j.NamaJabatan.Replace("'", "\\'") + "')";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static void UbahData(Jabatan j)
        {
            string sql = "update jabatan set namaJabatan='" + j.NamaJabatan.Replace("'", "\\'") + "' where IdJabatan='" + j.IdJabatan + "'";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static string HapusData(Jabatan j)
        {
            string sql = "DELETE FROM jabatan WHERE IdJabatan = '" + j.IdJabatan + "'";

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
        */
        public static List<Jabatan> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select * from jabatan";
            }
            else
            {
                sql = "select * from jabatan where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Jabatan> listOfJabatan = new List<Jabatan>();

            while (hasil.Read() == true)
            {
                Jabatan j = new Jabatan(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString());
                listOfJabatan.Add(j);
            }
            return listOfJabatan;
        }

        /*public static string GenerateKode()
        {
            string sql = "select max(IdJabatan) from jabatan";
            string hasilKode = "";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                int kodeTerbaru = int.Parse(hasil.GetValue(0).ToString()) + 1;

                hasilKode = kodeTerbaru.ToString();
            }
            else
            {
                hasilKode = "1";
            }
            return hasilKode;
        }*/
        #endregion
    }
}