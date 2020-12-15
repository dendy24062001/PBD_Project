using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ClassLibraryBookStore
{
    public class Buku
    {

        #region fields
        private string idBuku;
        private string nama;
        private int harga;
        private int stok;
        private Penerbit penerbit;
        private TipeBuku tipeBuku;
        
        #endregion
        
        #region Constructors
        public string IdBuku { get => idBuku; set => idBuku = value; }
        public string Nama { get => nama; set => nama = value; }
        public int Harga { get => harga; set => harga = value; }
        public int Stok { get => stok; set => stok = value; }
        public TipeBuku TipeBuku { get => tipeBuku; set => tipeBuku = value; }
        public Penerbit Penerbit { get => penerbit; set => penerbit = value; }
        #endregion

        #region Properties
        public Buku(string idBuku, string nama, int harga, int stok, TipeBuku tipeBuku,Penerbit penerbit)
        {
            IdBuku = idBuku;
            Nama = nama;
            Harga = harga;
            Stok = stok;
            Penerbit = penerbit;
            TipeBuku = tipeBuku;
        }
        #endregion

        #region Methods

        public static void TambahData(Buku b)
        {
            string sql = "insert into buku(IdBuku,Judul,Harga,stok,IdTipeBuku,IdPenerbit)" +
                         "VALUES('" + b.IdBuku + "', '" + b.Nama + "','" + b.Harga + "','" + b.Stok  + "', '" + b.TipeBuku.IdTipeBuku + "','" + b.Penerbit.IdPenerbit +  "')";
            //INSERT INTO `book_store`.`buku` (`IdBuku`, `Judul`, `Harga`, `stok`, `IdTipeBuku`, `IdPenerbit`) VALUES ('01002', 'Kata', '50000', '40', '2', '2');
            Koneksi.JalankanPerintahDML(sql);
        }

        public static string GenerateKode(TipeBuku t)
        {
            string sql = "select max(right(IdBuku,3)) from buku where IdTipeBuku ='" + t.IdTipeBuku + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            string hasilKode = "";
            if (hasil.Read() == true)
            {
                if (hasil.GetValue(0).ToString() != "")
                {
                    int kodeTerbaru = int.Parse(hasil.GetValue(0).ToString()) + 1;
                    hasilKode = t.IdTipeBuku.ToString().PadLeft(2, '0') + kodeTerbaru.ToString().PadLeft(3, '0');
                }
                else
                {
                    hasilKode = t.IdTipeBuku.ToString().PadLeft(2, '0') + "001";
                }
            }

            return hasilKode;
        }
        public static void UbahData(Buku b)
        {
            string sql = "update buku set Judul='" + b.Nama.Replace("'", "\\'") + "',Harga='" + b.Harga + "',IdTipeBuku='" + b.TipeBuku.IdTipeBuku + "', IdPenerbit='" + b.Penerbit.IdPenerbit + "' where IdBuku='" + b.IdBuku + "'";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static void HapusData(Buku b)
        {
            string sql = "DELETE FROM buku WHERE IdBuku = '" + b.IdBuku + "'";

            Koneksi.JalankanPerintahDML(sql);
        }

        public static List<Buku> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select b.IdBuku,b.Judul as 'judul buku',b.Harga,b.Stok,b.IdTipeBuku,t.JenisBuku as 'Jenis Buku',b.IdPenerbit, p.NamaPenerbit as 'Nama Penerbit', p.AlamatPenerbit" +
                      " from buku b inner join tipebuku t on b.IdTipeBuku = t.IdTipeBuku inner join penerbit p on b.IdPenerbit = p.IdPenerbit";
            }
            else
            {
                sql = "select b.IdBuku,b.Judul as 'judul buku',b.Harga,b.Stok,b.IdTipeBuku,t.JenisBuku as 'Jenis Buku',b.IdPenerbit,p.NamaPenerbit as 'Nama Penerbit', p.AlamatPenerbit" +
                      " from buku b inner join tipebuku t on b.IdTipeBuku = t.IdTipeBuku inner join penerbit p on b.IdPenerbit = p.IdPenerbit" +
                     " where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Buku> listOfBook = new List<Buku>();

            while (hasil.Read() == true)
            {
                TipeBuku t = new TipeBuku(hasil.GetValue(4).ToString(), hasil.GetValue(5).ToString());
                Penerbit p = new Penerbit(hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString());
                Buku b = new Buku(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString(), hasil.GetInt32(2), hasil.GetInt32(3), t, p);
                listOfBook.Add(b);
            }
            return listOfBook;
        }
        #endregion

        #region UpdateStok
        public static void UpdateStok(string jenisTransaksi, string idBuku, int jumlah)
        {
            string sql = "";
            if (jenisTransaksi == "penjualan")
            {
                sql = "update buku set Stok=Stok-" + jumlah + "where IdTipeBuku='" + idBuku + "'";
            }
            else if (jenisTransaksi == "pembelian")
            {
                sql = "update buku set Stok=Stok+" + jumlah + " where IdTipeBuku='" + idBuku + "'";
            }
            Koneksi.JalankanPerintahDML(sql);
        }
        #endregion
    }
}