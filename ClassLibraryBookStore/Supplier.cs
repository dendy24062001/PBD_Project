using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClassLibraryBookStore
{
    public class Supplier
    {
        private string idSupplier;
        private string nama;
        private string alamat;

        public Supplier(string idSupplier, string nama, string alamat)
        {
            IdSupplier = idSupplier;
            Nama = nama;
            Alamat = alamat;
        }

        public string IdSupplier { get => idSupplier; set => idSupplier = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Alamat { get => alamat; set => alamat = value; }

        #region Method
        public static void TambahData(Supplier p)
        {
            string sql = "Insert into Supplier(idSupplier, Nama, Alamat) values('" + p.IdSupplier + "','" + p.Nama.Replace("'", "\\'") + "','" + p.Alamat + "')";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static void UbahData(Supplier p)
        {
            string sql = "update Supplier set nama='" + p.Nama.Replace("'", "\\'") + "', alamat='" + p.Alamat + "' where IdSupplier='" + p.IdSupplier + "'";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static string HapusData(Supplier p)
        {
            string sql = "DELETE FROM Supplier WHERE idSupplier = '" + p.IdSupplier + "'";

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

        public static List<Supplier> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select * from supplier";
            }
            else
            {
                sql = "select * from supplier where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Supplier> listOfSupplier = new List<Supplier>();

            while (hasil.Read() == true)
            {
                Supplier p = new Supplier(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString());
                listOfSupplier.Add(p);
            }
            return listOfSupplier;
        }

        public static string GenerateKode()
        {
            string sql = "select max(IdSupplier) from Supplier";
            string hasilKode = "";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                int kodeTerbaru = int.Parse(hasil.GetValue(0).ToString()) + 1;

                hasilKode = kodeTerbaru.ToString().PadLeft(1);
            }
            else
            {
                hasilKode = "1";
            }
            return hasilKode;
        }
        #endregion
    }
}