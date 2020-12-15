using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using MySql.Data.MySqlClient;
namespace ClassLibraryBookStore
{
    public class Pegawai
    {
        private string idPegawai;
        private string nama;
        private DateTime tanggalLahir;
        private string alamat;
        private int gaji;
        private string username;
        private string password;
        private Jabatan jabatan;

        #region Constructors 
        public Pegawai(string idPegawai, string nama, DateTime tanggalLahir, string alamat, int gaji, string username, string password, Jabatan jabatan)
        {
            IdPegawai = idPegawai;
            Nama = nama;
            TanggalLahir = tanggalLahir;
            Alamat = alamat;
            Gaji = gaji;
            Username = username;
            Password = password;  
            Jabatan = jabatan;
        }
        #endregion

        #region Properties
        public string IdPegawai { get => idPegawai; set => idPegawai = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Alamat { get => alamat; set => alamat = value; }
        public int Gaji { get => gaji; set => gaji = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public Jabatan Jabatan { get => jabatan; set => jabatan = value; }
        public DateTime TanggalLahir { get => tanggalLahir; set => tanggalLahir = value; }
        #endregion

        public static void BuatUserBaru(Pegawai pPegawai, string pNamaServer)
        {
            string sql = "CREATE USER " + "'" + pPegawai.Username + "'" + "@" + "'" + pNamaServer + "'" + "IDENTIFIED BY " + "'" + pPegawai.Password + "'";
            Koneksi.JalankanPerintahDML(sql);
        }

        public static void BeriHakAkses(Pegawai pPegawai, string pNamaServer, string pNamaDatabase)
        {

            string sql = "GRANT ALL PRIVILEGES ON " + pNamaDatabase + ".* TO " + "'" + pPegawai.Username + "'" + "@" + "'" + pNamaServer + "'";
            Koneksi.JalankanPerintahDML(sql);
        }

        public static void ManajemenUser(Pegawai p)
        {
            string namaServer = Koneksi.GetNamaServer();
            string namaDatabase = Koneksi.GetNamaDatabase();

            Pegawai.BuatUserBaru(p, namaServer);
            Pegawai.BeriHakAkses(p, namaServer, namaDatabase);
        }
        public static void UbahPasswordUser(Pegawai pPegawai, string pNamaServer, string passwordBaru)
        {
            string sql = "SET PASSWORD FOR " + "'" + pPegawai.Username + "'" + "@" + "'" + pNamaServer + "'" + "=PASSWORD(" + "'" + passwordBaru + "')";
            Koneksi.JalankanPerintahDML(sql);
        }
        public static void UbahUser(Pegawai p)
        {
            string namaServer = Koneksi.GetNamaServer();

            Pegawai.UbahPasswordUser(p, namaServer, p.Password);
        }
        public static void HapusUser(Pegawai p, string pNamaServer)
        {
            string sql = "DROP USER " + "'" + p.Username + "'" + "@" + "'" + pNamaServer + "'";
            Koneksi.JalankanPerintahDML(sql);
        }
        public static void HapusAkunUser(Pegawai p)
        {
            string namaServer = Koneksi.GetNamaServer();

            Pegawai.HapusUser(p, namaServer);
        }
        #region method
        public static void TambahData(Pegawai p)
        {
            using (TransactionScope transaksiScope = new TransactionScope())
            {
                try
                {
                    string sql = "insert into pegawai(IdPegawai, Nama, TanggalLahir, Alamat, Gaji, Username, Password, IdJabatan) values('" + p.IdPegawai + "','" + p.Nama.Replace("'", "\\'") + "','" +
                                      p.TanggalLahir.ToString("yyyy-MM-dd") + "','" + p.Alamat.Replace("'", "\\'") + "','" + p.Gaji + "','" + p.Username + "','" + p.Password + "','" + p.Jabatan.IdJabatan + "')";

                    Koneksi.JalankanPerintahDML(sql);

                    //INSERT INTO `pegawai` (`KodePegawai` ,`Nama` ,`TglLahir` ,`Alamat` ,`Gaji` ,`Username` ,`Password` ,`IdJabatan`) VALUES('7', 'ria', '2020-06-08', 'Sidoarjo', '10000000', 'Ria2', '123', '1');

                    ManajemenUser(p);
                    transaksiScope.Complete();
                }
                catch (Exception ex)
                {
                    transaksiScope.Dispose(); // untuk membatalkan semua perintah yang ada di dlm trynya
                    throw (new Exception("Penambahan pegawai gagal. Pesan Kesalahan" + ex.Message));
                }
            }
        }

        public static void UbahData(Pegawai p)
        {
            string sql = "update pegawai set nama='" + p.Nama.Replace("'", "\\'") + "', TanggalLahir='" + p.TanggalLahir.ToString("yyyy-MM-dd") + "', Alamat='" + p.Alamat + "', Gaji='" + p.Gaji + "', Username='" + p.Username + "', Password='" + p.Password + "', IdJabatan='" + p.Jabatan.IdJabatan + "' where idPegawai='" + p.IdPegawai + "'";

            Koneksi.JalankanPerintahDML(sql);
            UbahUser(p);
        }

        public static void HapusData(Pegawai p)
        {
            string sql = "DELETE FROM pegawai WHERE idPegawai = '" + p.IdPegawai + "'";
            Koneksi.JalankanPerintahDML(sql);
            HapusAkunUser(p);
        }

        public static List<Pegawai> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";

            if (kriteria == "")
            {
                sql = "select p.idPegawai,p.Nama as 'Nama Pegawai',p.TanggalLahir,p.Alamat,p.Gaji,p.Username,p.Password,p.IdJabatan,j.NamaJabatan as 'Nama Jabatan' " +
                      "from pegawai p inner join jabatan j on p.IdJabatan = j.IdJabatan";
            }
            else
            {
                sql = "select p.idPegawai,p.Nama as 'Nama Pegawai',p.TanggalLahir,p.Alamat,p.Gaji,p.Username,p.Password,p.IdJabatan,j.NamaJabatan as 'Nama Jabatan' " +
                      "from pegawai p inner join jabatan j on p.IdJabatan = j.IdJabatan" +
                      " where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Pegawai> listOfPegawai = new List<Pegawai>();

            while (hasil.Read() == true)
            {


                Jabatan jabatan = new Jabatan(hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString());
                Pegawai p = new Pegawai(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString(), DateTime.Parse(hasil.GetValue(2).ToString()), 
                     hasil.GetValue(3).ToString(), hasil.GetInt32(4), hasil.GetValue(5).ToString(),
                    hasil.GetValue(6).ToString(), jabatan);
                listOfPegawai.Add(p);
            }
            return listOfPegawai;
        }

        public static string GenerateKode(Jabatan j)
        {
            string sql = "select max(right(IdPegawai,2)) from pegawai where IdJabatan ='" + j.IdJabatan + "'";

            string hasilKode = "";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            if (hasil.Read() == true)
            {
                if (hasil.GetValue(0).ToString() != "")
                {
                    int kodeTerbaru = int.Parse(hasil.GetValue(0).ToString()) + 1;
                    hasilKode = j.IdJabatan.ToString().PadLeft(2, '0') + kodeTerbaru.ToString().PadLeft(2, '0');
                }
                else
                {
                    hasilKode = j.IdJabatan.ToString().PadLeft(2, '0') + "01";
                }
            }
            return hasilKode;
        }
        #endregion

        #region BuatUserBaru
        //public static void BuatUserBaru(Pegawai pPegawai, string pNameServer)
        //{
        //    string sql = "Create user '" + pPegawai.Username + "'@'" + pNameServer + "' identified by '" + pPegawai.Password + "'";
        //    Koneksi.JalankanPerintahDML(sql);
        //}
        //public static void BeriHakAkses(Pegawai pPegawai, string pNamaServer, string pNamaDatabase)
        //{
        //    string sql = "grant all privileges on " + pNamaDatabase + ".* to '" + pPegawai.Username + "'@'" + pNamaServer + "'";

        //    Koneksi.JalankanPerintahDML(sql);
        //}
        //public static void ManajemenUser(Pegawai p)
        //{
        //    string namaServer = Koneksi.GetNamaServer();
        //    string namaDatabase = Koneksi.GetNamaDatabase();

        //    Pegawai.BuatUserBaru(p, namaServer);
        //    Pegawai.BeriHakAkses(p, namaServer, namaDatabase);
        //}
        //public static void UbahPasswordUser(Pegawai pPegawai)
        //{
        //    string namaServer = Koneksi.GetNamaServer();
        //    string sql = "SET PASSWORD FOR '" + pPegawai.Username + "'@'" + namaServer + "'=PASSWORD ('" + pPegawai.Password + "')";

        //    Koneksi.JalankanPerintahDML(sql);
        //}
        //public static void HapusPasswordUser(Pegawai pPegawai)
        //{
        //    string namaServer = Koneksi.GetNamaServer();
        //    string sql = "DROP USER '" + pPegawai.Username + "'@'" + namaServer + "'";

        //    Koneksi.JalankanPerintahDML(sql);
        //}
        #endregion
    }
}