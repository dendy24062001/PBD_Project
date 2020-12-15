using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClassLibraryBookStore
{
    public class Customers
    {
        private string idCustomer;
        private string nama;
        private string alamat;
        private string telepon;

        public Customers(string idCustomer, string nama, string alamat, string telepon)
        {
            IdCustomer = idCustomer;
            Nama = nama;
            Alamat = alamat;
            Telepon = telepon;
        }

        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Alamat { get => alamat; set => alamat = value; }
        public string Telepon { get => telepon; set => telepon = value; }



        #region Method
        public static void TambahData(Customers c)
        {
            string sql = "Insert into customers(IdCustomer, Nama, Alamat, Telepon) values('" + c.IdCustomer + "','" + c.Nama.Replace("'", "\\'") + "','" + c.Alamat + "','" + c.Telepon + "')";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static void UbahData(Customers c)
        {
            string sql = "update customers set nama='" + c.Nama.Replace("'", "\\'") + "', alamat='" + c.Alamat + "', telepon='" + c.Telepon + "' where IdCustomer='" + c.IdCustomer + "'";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static string HapusData(Customers c)
        {
            string sql = "DELETE FROM customers WHERE IdCustomer = '" + c.IdCustomer + "'";

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

        public static List<Customers> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select * from customers";
            }
            else
            {
                sql = "select * from customers where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Customers> listOfPelanggan = new List<Customers>();

            while (hasil.Read() == true)
            {
                Customers c = new Customers(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(), hasil.GetValue(3).ToString());
                listOfPelanggan.Add(c);
            }
            return listOfPelanggan;
        }

        public static string GenerateKode()
        {
            string sql = "select max(idCustomer) from customers";
            string hasilKode = "";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                int kodeTerbaru = int.Parse(hasil.GetValue(0).ToString()) + 1;

                hasilKode = kodeTerbaru.ToString().PadLeft(1);
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