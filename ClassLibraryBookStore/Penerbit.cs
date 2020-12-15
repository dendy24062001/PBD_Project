using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBookStore
{
    public class Penerbit
    {
        private string idPenerbit;
        private string namaPenerbit;
        private string alamatPenerbit;

        public Penerbit(string idPenerbit, string namaPenerbit, string alamatPenerbit)
        {
            this.idPenerbit = idPenerbit;
            this.namaPenerbit = namaPenerbit;
            this.AlamatPenerbit = alamatPenerbit;
        }

        public string IdPenerbit { get => idPenerbit; set => idPenerbit = value; }
        public string NamaPenerbit { get => namaPenerbit; set => namaPenerbit = value; }
        public string AlamatPenerbit { get => alamatPenerbit; set => alamatPenerbit = value; }

        public static List<Penerbit> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select * from penerbit";
            }
            else
            {
                sql = "select * from penerbit where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Penerbit> listOfPenerbit = new List<Penerbit>();

            while (hasil.Read() == true)
            {
                Penerbit p = new Penerbit(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString());
                listOfPenerbit.Add(p);
            }
            return listOfPenerbit;
        }
    }
}