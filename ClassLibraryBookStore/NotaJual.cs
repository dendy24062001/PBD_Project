using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ClassLibraryBookStore
{
    public class NotaJual
    {
        private string noNota;
        private DateTime tanggal;
        private Pegawai pegawai;
        private Customers customers;
        private List<NotaJualDetil> listNotaJualDetil;
        private Pembayaran pembayaran;

        public NotaJual(string noNota, DateTime tanggal, Pegawai pegawai, Customers customers, Pembayaran pembayaran)
        {
            this.NoNota = noNota;
            this.Tanggal = tanggal;
            this.Pegawai = pegawai;
            this.Customers = customers;
            this.ListNotaJualDetil = new List<NotaJualDetil>();
            this.Pembayaran = pembayaran;
        }

        public string NoNota { get => noNota; set => noNota = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public Pegawai Pegawai { get => pegawai; set => pegawai = value; }
        public Customers Customers { get => customers; set => customers = value; }
        public List<NotaJualDetil> ListNotaJualDetil { get => listNotaJualDetil; private set => listNotaJualDetil = value;}
        public Pembayaran Pembayaran { get => pembayaran; set => pembayaran = value; }

        #region GenerateNoNota
        public static string GenerateNoNota()
        {
            string sql = "select MAX(RIGHT(NoNota,3)) as NoUrutTransaksi" +
                         " from notaJual " +
                         " where Date(Tanggal) = Date(CURRENT_DATE)" +
                         " order by Tanggal DESC limit 1";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            string hasilNoNota = "";
            if (hasil.Read() == true)
            {
                if (hasil.GetValue(0).ToString() != "")
                {
                    int noUrutTrans = int.Parse(hasil.GetValue(0).ToString()) + 1;

                    hasilNoNota = DateTime.Now.Year +
                                  DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                  DateTime.Now.Day.ToString().PadLeft(2, '0') +
                                  noUrutTrans.ToString().PadLeft(3, '0');
                }
                else // JIKA belum ada transaksi penjualan hari ini
                {
                    hasilNoNota = DateTime.Now.Year +
                                      DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                      DateTime.Now.Day.ToString().PadLeft(2, '0') +
                                      "001";
                }
            }
            return hasilNoNota;
        }
        #endregion

        #region Methods
        public void TambahNotaJualDetil(Buku buku, int harga, int jumlah)
        {
            NotaJualDetil notaJualDetil = new NotaJualDetil(buku, harga, jumlah);

            this.ListNotaJualDetil.Add(notaJualDetil);
        }

        public static void TambahData(NotaJual nota)
        {
            using (TransactionScope transaksiScope = new TransactionScope())
            {
                try
                {
                    string sql1 = "insert into NotaJual(NoNota,Tanggal,IdPegawai,IdCustomer,IdPembayaran) values('" + nota.NoNota + "','" +
                                nota.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") + "','" + nota.Customers.IdCustomer + "','" + nota.Pegawai.IdPegawai + "','" + nota.Pembayaran.IdPembayaran + "')";

                    Koneksi.JalankanPerintahDML(sql1);

                    foreach (NotaJualDetil detilNota in nota.ListNotaJualDetil)
                    {
                        string sql2 = "insert into NotaJualDetil(NoNota,IdBuku,Harga,Jumlah) values ('" + nota.NoNota + "','" +
                                        detilNota.Buku.IdBuku + "','" + detilNota.Harga + "','" + detilNota.Jumlah + "')";

                        Koneksi.JalankanPerintahDML(sql2);

                        //untuk mengupdate stok barang yang sudah terjual, karena telah terjado proses penjualan maka stok haus diupdate atau dikurangi
                        //dan memanggil method updateStok
                        Buku.UpdateStok("penjualan", detilNota.Buku.IdBuku, detilNota.Jumlah);
                    }
                    transaksiScope.Complete(); // untuk menyimpan data secara permanen jika berhasil dilakukan
                }
                catch (Exception ex)
                {
                    transaksiScope.Dispose(); // untuk membatalkan semua perintah yang ada di dlm trynya
                    throw (new Exception("Penyimpanan transaksi penjualan gagal. Pesan Kesalahan" + ex.Message));
                }
            }
        }
        #endregion

        #region Method BacaData
        public static List<NotaJual> BacaData(string pKriteria, string pNilaiKriteria)
        {
            string sql1 = "";
            if (pKriteria == "")
            {
                sql1 = "SELECT N.NoNota, N.Tanggal, N.IdCustomer, c.Nama AS NamaPelanggan, c.Alamat AS AlamatPelanggan, N.IdPegawai, pe.Nama AS NamaPegawai, N.IdPembayaran, pem.JenisPembayaran as 'Jenis Pembayaran'  " +
                    "FROM notajual N INNER JOIN customers c on N.IdCustomer = c.IdCustomer " +
                    "INNER JOIN pegawai pe on N.IdPegawai = pe.IdPegawai " +
                    "INNER JOIN pembayaran pem on N.IdPembayaran = pem.IdPembayaran " +
                    "ORDER BY N.NoNota DESC";
            }
            else
            {
                sql1 = "SELECT N.NoNota, N.Tanggal, N.IdCustomer, c.Nama AS NamaPelanggan, c.Alamat AS AlamatPelanggan, N.IdPegawai, pe.Nama AS NamaPegawai, N.IdPembayaran, pem.JenisPembayaran as 'Jenis Pembayaran'  " +
                    "FROM notajual N INNER JOIN customers c on N.IdCustomer = c.IdCustomer " +
                    "INNER JOIN pegawai pe on N.IdPegawai = pe.IdPegawai " +
                    "INNER JOIN pembayaran pem on N.IdPembayaran = pem.IdPembayaran " +
                    "WHERE " + pKriteria + " LIKE '% " + pNilaiKriteria + "%' " +
                    "ORDER BY N.NoNota DESC";
            }

            MySqlDataReader hasilData1 = Koneksi.JalankanPerintahQuery(sql1);
            List<NotaJual> listHasilData = new List<NotaJual>();

            while (hasilData1.Read() == true)
            {
                string nomorNota = hasilData1.GetValue(0).ToString();

                DateTime tglNota = DateTime.Parse(hasilData1.GetValue(1).ToString());

                List<Customers> listPelanggan = Customers.BacaData("IdCustomer", hasilData1.GetValue(2).ToString());

                List<Pegawai> listPegawai = Pegawai.BacaData("IdPegawai", hasilData1.GetValue(5).ToString());

                List<Pembayaran> listPembayaran = Pembayaran.BacaData("IdPembayaran", hasilData1.GetValue(7).ToString());

                NotaJual nota = new NotaJual(nomorNota, tglNota, listPegawai[0], listPelanggan[0], listPembayaran[0]);

                string sql2 = "SELECT NJD.IdBuku, B.Judul, NJD.Harga, NJD.Jumlah " +
                                "FROM NotaJual N INNER JOIN NotaJualDetil NJD ON N.NoNota = NJD.NoNota " +
                                "INNER JOIN Buku B ON NJD.IdBuku = B.IdBuku " +
                                "WHERE N.NoNota = '" + nomorNota + "'";

                MySqlDataReader hasilData2 = Koneksi.JalankanPerintahQuery(sql2);

                while (hasilData2.Read() == true)
                {
                    List<Buku> listOfBook = Buku.BacaData("B.IdBuku", hasilData2.GetValue(0).ToString());

                    int hrgJual = int.Parse(hasilData2.GetValue(2).ToString());

                    int jumlahJual = int.Parse(hasilData2.GetValue(3).ToString());

                    NotaJualDetil detlNota = new NotaJualDetil(listOfBook[0], hrgJual, jumlahJual);

                    nota.TambahNotaJualDetil(listOfBook[0], hrgJual, jumlahJual);
                }
                listHasilData.Add(nota);
            }
            return listHasilData;
        }
        #endregion

        #region Method Cetak
        public static void CetakNota(string pKriteria, string pNilaiKriteria, string pNamaFile, Font pFont)
        {
            List<NotaJual> listNotaJual = new List<NotaJual>();

            listNotaJual = NotaJual.BacaData(pKriteria, pNilaiKriteria);

            StreamWriter file = new StreamWriter(pNamaFile);

            foreach (NotaJual nota in listNotaJual)
            {
                file.WriteLine("");
                file.WriteLine("TOKO MAJU MAKMUR UNTUNG SELALU");
                file.WriteLine("Jl. Raya Kalirungkut Surabaya");
                file.WriteLine("Telp. (031) - 12345678");
                file.WriteLine("=".PadRight(50, '='));

                file.WriteLine("No.Nota : " + nota.NoNota);
                file.WriteLine("Tanggal : " + nota.Tanggal);
                file.WriteLine("");
                file.WriteLine("Pelanggan : " + nota.Customers.Nama);
                file.WriteLine("Alamat : " + nota.Customers.Alamat);
                file.WriteLine("");
                file.WriteLine("Kasir : " + nota.Pegawai.Nama);
                file.WriteLine("");
                file.WriteLine("Jenis Pembayaran" + nota.Pembayaran.JenisPembayaran);
                file.WriteLine("=".PadRight(50, '='));

                int grandTotal = 0;
                foreach (NotaJualDetil njd in nota.ListNotaJualDetil)
                {
                    string nama = njd.Buku.Nama; // apabila nama terlalu panjang maka akan ditampilkan hanya sampai 30 karakter
                    if (nama.Length > 30)
                    {
                        nama = nama.Substring(0, 30);
                    }
                    int jumlah = njd.Jumlah;
                    int harga = njd.Harga;
                    int subTotal = jumlah * harga;
                    file.WriteLine(nama.PadRight(30, ' '));
                    file.WriteLine(jumlah.ToString().PadRight(3, ' '));
                    file.WriteLine(harga.ToString("#,###").PadRight(7, ' '));
                    file.WriteLine(subTotal.ToString("#,###").PadRight(10, ' '));
                    file.WriteLine("");
                    grandTotal = grandTotal + subTotal;
                }
                file.WriteLine("=".PadRight(50, '='));
                file.WriteLine("TOTAL : " + grandTotal.ToString("#,###"));
                file.WriteLine("=".PadRight(50, '='));
                file.WriteLine("Terima Kasih atas Kunjungan Anda");
                file.WriteLine("");
            }
            file.Close();

            ClassPrint c = new ClassPrint(pNamaFile, pFont, 20, 10, 10, 10);
            c.CetakPrinter("text");
        }
        #endregion
    }
}