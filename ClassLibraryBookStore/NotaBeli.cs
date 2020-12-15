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
    public class NotaBeli
    {
        private string noNota;
        private DateTime tanggal;
        private Pegawai pegawai;
        private Supplier supplier;
        private Pembayaran pembayaran;
       private List<NotaBeliDetil> listNotaBeliDetil;

        public NotaBeli(string noNota, DateTime tanggal, Pegawai pegawai, Supplier supplier, Pembayaran pembayaran)
        {
            this.NoNota = noNota;
            this.Tanggal = tanggal;
            this.Pegawai = pegawai;
            this.Supplier = supplier;
            this.ListNotaBeliDetil = new List<NotaBeliDetil>();
            this.pembayaran = pembayaran;
        }

        public string NoNota { get => noNota; set => noNota = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public Pegawai Pegawai { get => pegawai; set => pegawai = value; }
        public Supplier Supplier { get => supplier; set => supplier = value; }
        public List<NotaBeliDetil> ListNotaBeliDetil { get => listNotaBeliDetil; private set => listNotaBeliDetil = value; }
        public Pembayaran Pembayaran { get => pembayaran; set => pembayaran = value; }
       


        #region GenerateNoNota
        public static string GenerateNoNota()
        {
            string sql = "select MAX(RIGHT(NoNota,3)) as NoUrutTransaksi" +
                         " from notaBeli " +
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
        public void TambahNotaBeliDetil(Buku buku, int harga, int jumlah)
        {
            NotaBeliDetil notaBeliDetil = new NotaBeliDetil(buku, harga, jumlah);

            this.ListNotaBeliDetil.Add(notaBeliDetil);
        }

        public static void TambahData(NotaBeli nota)
        {
            using (TransactionScope transaksiScope = new TransactionScope())
            {
                try
                {
                    string sql1 = "insert into NotaBeli(NoNota,Tanggal,IdPegawai,IdSupplier,IdPembayaran) values('" + nota.NoNota + "','" +
                                nota.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") + "','" + nota.Pegawai.IdPegawai + "','" + nota.Supplier.IdSupplier + "','" + nota.Pembayaran.IdPembayaran + "')";

                    Koneksi.JalankanPerintahDML(sql1);

                    foreach (NotaBeliDetil detilNota in nota.ListNotaBeliDetil)
                    {
                        string sql2 = "insert into NotaBeliDetil(NoNota,IdBuku,Harga,Jumlah) values ('" + nota.NoNota + "','" +
                                        detilNota.Buku.IdBuku + "','" + detilNota.Harga + "','" + detilNota.Jumlah + "')";

                        Koneksi.JalankanPerintahDML(sql2);

                        //untuk mengupdate stok buku yang sudah terjual, karena telah terjado proses penjualan maka stok haus diupdate atau dikurangi
                        //dan memanggil method updateStok
                        Buku.UpdateStok("pembelian", detilNota.Buku.IdBuku, detilNota.Jumlah);
                    }
                    transaksiScope.Complete(); // untuk menyimpan data secara permanen jika berhasil dilakukan
                }
                catch (Exception ex)
                {
                    transaksiScope.Dispose(); // untuk membatalkan semua perintah yang ada di dlm trynya
                    throw (new Exception("Penyimpanan transaksi pembelian gagal. Pesan Kesalahan" + ex.Message));
                }
            }
        }
        #endregion

        #region Method BacaData
        public static List<NotaBeli> BacaData(string pKriteria, string pNilaiKriteria)
        {
            string sql1 = "";
            if (pKriteria == "")
            {
                sql1 = "SELECT N.NoNota, N.Tanggal,  N.IdPegawai, pe.Nama AS NamaPegawai, N.IdSupplier, s.Nama AS NamaSupplier, s.Alamat AS AlamatSupplier, N.IdPembayaran, p.JenisPembayaran as 'Jenis Pembayaran' " +
                    "FROM notaBeli N INNER JOIN supplier s on N.IdSupplier = s.IdSupplier " +
                    "INNER JOIN pegawai pe on N.IdPegawai = pe.IdPegawai " +
                    "INNER JOIN pembayaran p on N.IdPembayaran = p.IdPembayaran " +
                    "ORDER BY N.NoNota DESC";
                //SELECT N.NoNota, N.Tanggal, N.IdSupplier, s.Nama AS 'Nama Supplier', s.Alamat AS 'Alamat Supplier', N.IdPegawai, pe.Nama AS 'Nama Pegawai', N.IdPembayaran, p.JenisPembayaran as 'Jenis Pembayaran'
                //FROM notaBeli N INNER JOIN supplier s on N.IdSupplier = s.IdSupplier
                //INNER JOIN pegawai pe on N.IdPegawai = pe.IdPegawai
                //INNER JOIN pembayaran p on N.IdPembayaran = p.IdPembayaran
            }
            else
            {
                sql1 = "SELECT N.NoNota, N.Tanggal,  N.IdPegawai, pe.Nama AS NamaPegawai, N.IdSupplier, s.Nama AS NamaSupplier, s.Alamat AS AlamatSupplier, N.IdPembayaran, p.JenisPembayaran as 'Jenis Pembayaran' " +
                    "FROM notaBeli N INNER JOIN supplier s on N.IdSupplier = s.IdSupplier " +
                    "INNER JOIN pegawai pe on N.IdPegawai = pe.IdPegawai " +
                    "INNER JOIN pembayaran p on N.IdPembayaran = p.IdPembayaran " +
                    "WHERE " + pKriteria + " LIKE '% " + pNilaiKriteria + "%' " +
                    "ORDER BY N.NoNota DESC";
            }

            MySqlDataReader hasilData1 = Koneksi.JalankanPerintahQuery(sql1);
            List<NotaBeli> listHasilData = new List<NotaBeli>();

            while (hasilData1.Read() == true)
            {
                string nomorNota = hasilData1.GetValue(0).ToString();

                DateTime tglNota = DateTime.Parse(hasilData1.GetValue(1).ToString());

                List<Pegawai> listPegawai = Pegawai.BacaData("IdPegawai", hasilData1.GetValue(2).ToString()); //nilainya sesuai dengan urutan query yang diambil disql kriteria

                List<Supplier> listSupplier = Supplier.BacaData("IdSupplier", hasilData1.GetValue(4).ToString()); 

                List<Pembayaran> listPembayaran = Pembayaran.BacaData("IdPembayaran", hasilData1.GetValue(7).ToString()); 

                NotaBeli nota = new NotaBeli(nomorNota, tglNota, listPegawai[0], listSupplier[0], listPembayaran[0]);

                string sql2 = "SELECT NBD.IdBuku, B.Judul, NBD.Harga, NBD.Jumlah " +
                                "FROM NotaBeli N INNER JOIN NotaBeliDetil NBD ON N.NoNota = NBD.NoNota " +
                                "INNER JOIN Buku B ON NBD.IdBuku = B.IdBuku " +
                                "WHERE N.NoNota = '" + nomorNota + "'";

                MySqlDataReader hasilData2 = Koneksi.JalankanPerintahQuery(sql2);

                while (hasilData2.Read() == true)
                {
                    List<Buku> listBuku = Buku.BacaData("B.IdBuku", hasilData2.GetValue(0).ToString());

                    int hrgJual = int.Parse(hasilData2.GetValue(2).ToString());

                    int jumlahJual = int.Parse(hasilData2.GetValue(3).ToString());

                    NotaBeliDetil detlNota = new NotaBeliDetil(listBuku[0], hrgJual, jumlahJual);

                    nota.TambahNotaBeliDetil(listBuku[0], hrgJual, jumlahJual);
                }
                listHasilData.Add(nota);
            }
            return listHasilData;
        }
        #endregion

        #region Method Cetak
        public static void CetakNota(string pKriteria, string pNilaiKriteria, string pNamaFile, Font pFont)
        {
            List<NotaBeli> listNotaBeli = new List<NotaBeli>();

            listNotaBeli = NotaBeli.BacaData(pKriteria, pNilaiKriteria);

            StreamWriter file = new StreamWriter(pNamaFile);

            foreach (NotaBeli nota in listNotaBeli)
            {
                file.WriteLine("");
                file.WriteLine("TOKO MAJU MAKMUR UNTUNG SELALU");
                file.WriteLine("Jl. Raya Kalirungkut Surabaya");
                file.WriteLine("Telp. (031) - 12345678");
                file.WriteLine("=".PadRight(50, '='));

                file.WriteLine("No.Nota : " + nota.NoNota);
                file.WriteLine("Tanggal : " + nota.Tanggal);
                file.WriteLine("");
                file.WriteLine("Supplier : " + nota.Supplier.Nama);
                file.WriteLine("Alamat : " + nota.Supplier.Alamat);
                file.WriteLine("");
                file.WriteLine("Kasir : " + nota.Pegawai.Nama);
                file.WriteLine("");
                file.WriteLine("Jenis Pembayaran" + nota.Pembayaran.JenisPembayaran);
                file.WriteLine("=".PadRight(50, '='));

                int grandTotal = 0;
                foreach (NotaBeliDetil nbd in nota.ListNotaBeliDetil)
                {
                    string nama = nbd.Buku.Nama; // apabila nama terlalu panjang maka akan ditampilkan hanya sampai 30 karakter
                    if (nama.Length > 30)
                    {
                        nama = nama.Substring(0, 30);
                    }
                    int jumlah = nbd.Jumlah;
                    int harga = nbd.Harga;
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