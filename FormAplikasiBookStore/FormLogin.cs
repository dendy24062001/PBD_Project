using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibraryBookStore;

namespace FormAplikasiBookStore
{
    public partial class FormLogin : Form
    {
        List<Pegawai> listPegawai = new List<Pegawai>();
        FormMenu formMenu = new FormMenu();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            Height = 50 + panelLogin.Height;
           
            textBoxServer.Text = "localhost";
            textBoxDatabase.Text = "book_store";
        }

        private void linkLabelOption_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Height = 50 + panelLogin.Height + panelOption.Height;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxServer.Text != "" && textBoxDatabase.Text != "")
            {
                Height = 50 + panelLogin.Height;
            }
            else
            {
                MessageBox.Show("Nama server dan nama database tidak boleh kosong", "Kesalahan");
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBoxUsername.Text != "")
                {
                    Koneksi koneksi = new Koneksi(textBoxServer.Text, textBoxDatabase.Text, textBoxUsername.Text, textBoxPassword.Text);

                    Koneksi koneksi2 = new Koneksi();

                    koneksi.Connect();
                    koneksi2.Connect();

                    MessageBox.Show("Koneksi Berhasil. Selamat menggunakan aplikasi", "Informasi");
                    this.Owner.WindowState = FormWindowState.Normal;

                    this.Owner.Enabled = true;
                    listPegawai = Pegawai.BacaData("username", textBoxUsername.Text);
                    if (listPegawai.Count > 0)
                    {
                        FormMenu formMenu = (FormMenu)this.Owner;
                        formMenu.labelKodePegawai.Text = listPegawai[0].IdPegawai.ToString();
                        formMenu.labelNamaPegawai.Text = listPegawai[0].Nama;
                        formMenu.labelJabatanPegawai.Text = listPegawai[0].Jabatan.NamaJabatan;

                        formMenu.PengaturanHakAkses(listPegawai[0].Jabatan);

                        formMenu.pegawaiLogin = listPegawai[0];

                        this.Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Username tidak ditemukan");
                    }
                    this.Close();
                }
               
                else
                {
                    MessageBox.Show("Username dan Password tidak boleh kosong", "Kesalahan");
                    labelUsername.Visible = false;
                    textBoxUsername.Focus();
                    if (textBoxPassword.Text == "")
                    {
                        labelPassword.Visible = true;
                    }
                    else
                    {
                        labelPassword.Visible = false;
                    }
                    if (textBoxServer.Text == "")
                    {
                        labelServer.Visible = true;
                    }
                    else
                    {
                        labelServer.Visible = false;
                    }
                    if (textBoxDatabase.Text == "")
                    {
                        labelDatabase.Visible = true;
                    }
                    else
                    {
                        labelDatabase.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi Gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
                labelUsername.Visible = false;
                textBoxUsername.Focus();
                if (textBoxPassword.Text == "")
                {
                    labelPassword.Visible = true;
                }
                else
                {
                    labelPassword.Visible = false;
                }
                if (textBoxServer.Text == "")
                {
                    labelServer.Visible = true;
                }
                else
                {
                    labelServer.Visible = false;
                }
                if (textBoxDatabase.Text == "")
                {
                    labelDatabase.Visible = true;
                }
                else
                {
                    labelDatabase.Visible = false;
                }
            }
        }
        private void textBoxUsername_Click(object sender, EventArgs e)
        {
            labelUsername.Visible = false;
            if (textBoxPassword.Text == "")
            {
                labelPassword.Visible = true;
            }
            else
            {
                labelPassword.Visible = false;
            }
            if (textBoxServer.Text == "")
            {
                labelServer.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxDatabase.Text == "")
            {
                labelDatabase.Visible = true;
            }
            else
            {
                labelDatabase.Visible = false;
            }
        }

        private void textBoxPassword_Click(object sender, EventArgs e)
        {
            labelPassword.Visible = false;
            if (textBoxUsername.Text == "")
            {
                labelUsername.Visible = true;
            }
            else
            {
                labelUsername.Visible = false;
            }
            if (textBoxServer.Text == "")
            {
                labelServer.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxDatabase.Text == "")
            {
                labelDatabase.Visible = true;
            }
            else
            {
                labelDatabase.Visible = false;
            }
        }

        private void textBoxServer_Click(object sender, EventArgs e)
        {
            labelServer.Visible = false;
            if (textBoxUsername.Text == "")
            {
                labelUsername.Visible = true;
            }
            else
            {
                labelUsername.Visible = false;
            }
            if (textBoxDatabase.Text == "")
            {
                labelDatabase.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxPassword.Text == "")
            {
                labelPassword.Visible = true;
            }
            else
            {
                labelPassword.Visible = false;
            }
        }

        private void textBoxDatabase_Click(object sender, EventArgs e)
        {
            labelDatabase.Visible = false;
            if (textBoxUsername.Text == "")
            {
                labelUsername.Visible = true;
            }
            else
            {
                labelUsername.Visible = false;
            }
            if (textBoxServer.Text == "")
            {
                labelServer.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxPassword.Text == "")
            {
                labelPassword.Visible = true;
            }
            else
            {
                labelPassword.Visible = false;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            labelServer.Visible = false;
            textBoxServer.Focus();
            if (textBoxUsername.Text == "")
            {
                labelUsername.Visible = true;
            }
            else
            {
                labelUsername.Visible = false;
            }
            if (textBoxDatabase.Text == "")
            {
                labelDatabase.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxPassword.Text == "")
            {
                labelPassword.Visible = true;
            }
            else
            {
                labelPassword.Visible = false;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            labelDatabase.Visible = false;
            textBoxDatabase.Focus();
            if (textBoxUsername.Text == "")
            {
                labelUsername.Visible = true;
            }
            else
            {
                labelUsername.Visible = false;
            }
            if (textBoxServer.Text == "")
            {
                labelServer.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxPassword.Text == "")
            {
                labelPassword.Visible = true;
            }
            else
            {
                labelPassword.Visible = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            labelPassword.Visible = false;
            textBoxPassword.Focus();
            if (textBoxUsername.Text == "")
            {
                labelUsername.Visible = true;
            }
            else
            {
                labelUsername.Visible = false;
            }
            if (textBoxServer.Text == "")
            {
                labelServer.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxDatabase.Text == "")
            {
                labelDatabase.Visible = true;
            }
            else
            {
                labelDatabase.Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            labelUsername.Visible = false;
            textBoxUsername.Focus();
            if (textBoxPassword.Text == "")
            {
                labelPassword.Visible = true;
            }
            else
            {
                labelPassword.Visible = false;
            }
            if (textBoxServer.Text == "")
            {
                labelServer.Visible = true;
            }
            else
            {
                labelServer.Visible = false;
            }
            if (textBoxDatabase.Text == "")
            {
                labelDatabase.Visible = true;
            }
            else
            {
                labelDatabase.Visible = false;
            }
        }

        

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                try
                {
                    if (textBoxUsername.Text != "")
                    {
                        Koneksi koneksi = new Koneksi(textBoxServer.Text, textBoxDatabase.Text, textBoxUsername.Text, textBoxPassword.Text);

                        Koneksi koneksi2 = new Koneksi();

                        koneksi.Connect();
                        koneksi2.Connect();

                        MessageBox.Show("Koneksi Berhasil. Selamat menggunakan aplikasi", "Informasi");
                        this.Owner.WindowState = FormWindowState.Normal;
                        this.Owner.Enabled = true;
                        listPegawai = Pegawai.BacaData("username", textBoxUsername.Text);
                        if (listPegawai.Count > 0)
                        {
                            FormMenu formMenu = (FormMenu)this.Owner;
                            formMenu.labelKodePegawai.Text = listPegawai[0].IdPegawai.ToString();
                            formMenu.labelNamaPegawai.Text = listPegawai[0].Nama;
                            formMenu.labelJabatanPegawai.Text = listPegawai[0].Jabatan.NamaJabatan;

                            formMenu.PengaturanHakAkses(listPegawai[0].Jabatan);

                            formMenu.pegawaiLogin = listPegawai[0];

                            this.Close();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Username dan Password tidak boleh kosong", "Kesalahan");
                        if (textBoxUsername.Text == "")
                        {
                            labelUsername.Visible = true;
                        }
                        else
                        {
                            labelUsername.Visible = false;
                        }
                        labelUsername.Visible = false;
                        textBoxUsername.Focus();
                        if (textBoxPassword.Text == "")
                        {
                            labelPassword.Visible = true;
                        }
                        else
                        {
                            labelPassword.Visible = false;
                        }
                        if (textBoxServer.Text == "")
                        {
                            labelServer.Visible = true;
                        }
                        else
                        {
                            labelServer.Visible = false;
                        }
                        if (textBoxDatabase.Text == "")
                        {
                            labelDatabase.Visible = true;
                        }
                        else
                        {
                            labelDatabase.Visible = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi Gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
                    labelUsername.Visible = false;
                    textBoxUsername.Focus();
                    if (textBoxPassword.Text == "")
                    {
                        labelPassword.Visible = true;
                    }
                    else
                    {
                        labelPassword.Visible = false;
                    }
                    if (textBoxServer.Text == "")
                    {
                        labelServer.Visible = true;
                    }
                    else
                    {
                        labelServer.Visible = false;
                    }
                    if (textBoxDatabase.Text == "")
                    {
                        labelDatabase.Visible = true;
                    }
                    else
                    {
                        labelDatabase.Visible = false;
                    }
                }
            }
        }

        private void textBoxUsername_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                labelPassword.Visible = false;
                textBoxPassword.Focus();
                labelPassword.Visible = false;
                if (textBoxUsername.Text == "")
                {
                    labelUsername.Visible = true;
                }
                else
                {
                    labelUsername.Visible = false;
                }
                if (textBoxServer.Text == "")
                {
                    labelServer.Visible = true;
                }
                else
                {
                    labelServer.Visible = false;
                }
                if (textBoxDatabase.Text == "")
                {
                    labelDatabase.Visible = true;
                }
                else
                {
                    labelDatabase.Visible = false;
                }
                e.IsInputKey = true;
            }
            if (e.KeyData == (Keys.Tab | Keys.Shift))
            {
                if (textBoxUsername.Text == "")
                {
                    labelUsername.Visible = true;
                }
                else
                {
                    labelUsername.Visible = false;
                }
                Height = 50 + panelLogin.Height + panelOption.Height;
                labelServer.Visible = false;
                textBoxServer.Focus();
                e.IsInputKey = true;
            }
            else if (e.KeyData == (Keys.Tab | Keys.Control))
            {
                if (textBoxUsername.Text == "")
                {
                    labelUsername.Visible = true;
                }
                else
                {
                    labelUsername.Visible = false;
                }
                Height = 50 + panelLogin.Height;
                labelUsername.Visible = false;
                textBoxUsername.Focus();
                e.IsInputKey = true;
            }
        }

        private void textBoxPassword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                labelUsername.Visible = false;
                textBoxUsername.Focus();
                if (textBoxPassword.Text == "")
                {
                    labelPassword.Visible = true;
                }
                else
                {
                    labelPassword.Visible = false;
                }
                if (textBoxServer.Text == "")
                {
                    labelServer.Visible = true;
                }
                else
                {
                    labelServer.Visible = false;
                }
                if (textBoxDatabase.Text == "")
                {
                    labelDatabase.Visible = true;
                }
                else
                {
                    labelDatabase.Visible = false;
                }
                e.IsInputKey = true;
            }
            if (e.KeyData == (Keys.Tab | Keys.Shift))
            {
                if (textBoxPassword.Text == "")
                {
                    labelPassword.Visible = true;
                }
                else
                {
                    labelPassword.Visible = false;
                }
                Height = 50 + panelLogin.Height + panelOption.Height;
                labelServer.Visible = false;
                textBoxServer.Focus();
                e.IsInputKey = true;
            }
            else if (e.KeyData == (Keys.Tab | Keys.Control))
            {
                if (textBoxPassword.Text == "")
                {
                    labelPassword.Visible = true;
                }
                else
                {
                    labelPassword.Visible = false;
                }
                Height = 50 + panelLogin.Height;
                labelUsername.Visible = false;
                textBoxUsername.Focus();
                e.IsInputKey = true;
            }


        }

        private void textBoxServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (textBoxServer.Text != "" && textBoxDatabase.Text != "")
                {
                    Height = 50 + panelLogin.Height;
                    textBoxUsername.Focus();
                }
                else
                {
                    MessageBox.Show("Nama server dan nama database tidak boleh kosong", "Kesalahan");
                    labelServer.Visible = false;
                    textBoxServer.Focus();
                    if (textBoxUsername.Text == "")
                    {
                        labelUsername.Visible = true;
                    }
                    else
                    {
                        labelUsername.Visible = false;
                    }
                    if (textBoxDatabase.Text == "")
                    {
                        labelDatabase.Visible = true;
                    }
                    else
                    {
                        labelServer.Visible = false;
                    }
                    if (textBoxPassword.Text == "")
                    {
                        labelPassword.Visible = true;
                    }
                    else
                    {
                        labelPassword.Visible = false;
                    }
                }
                
            }
        }

        private void textBoxDatabase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (textBoxServer.Text != "" && textBoxDatabase.Text != "")
                {
                    Height = 50 + panelLogin.Height;
                    textBoxUsername.Focus();
                }
                else
                {
                    MessageBox.Show("Nama server dan nama database tidak boleh kosong", "Kesalahan");
                    labelServer.Visible = false;
                    textBoxServer.Focus();
                    if (textBoxUsername.Text == "")
                    {
                        labelUsername.Visible = true;
                    }
                    else
                    {
                        labelUsername.Visible = false;
                    }
                    if (textBoxDatabase.Text == "")
                    {
                        labelDatabase.Visible = true;
                    }
                    else
                    {
                        labelServer.Visible = false;
                    }
                    if (textBoxPassword.Text == "")
                    {
                        labelPassword.Visible = true;
                    }
                    else
                    {
                        labelPassword.Visible = false;
                    }
                }
            }
        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                try
                {
                    if (textBoxUsername.Text != "")
                    {
                        Koneksi koneksi = new Koneksi(textBoxServer.Text, textBoxDatabase.Text, textBoxUsername.Text, textBoxPassword.Text);

                        Koneksi koneksi2 = new Koneksi();

                        koneksi.Connect();
                        koneksi2.Connect();

                        MessageBox.Show("Koneksi Berhasil. Selamat menggunakan aplikasi", "Informasi");
                        this.Owner.WindowState = FormWindowState.Normal;
                        this.Owner.Enabled = true;
                        this.Close();

                        listPegawai = Pegawai.BacaData("username", textBoxUsername.Text);
                        if (listPegawai.Count > 0)
                        {
                            FormMenu formMenu = (FormMenu)this.Owner;
                            formMenu.labelKodePegawai.Text = listPegawai[0].IdPegawai.ToString();
                            formMenu.labelNamaPegawai.Text = listPegawai[0].Nama;
                            formMenu.labelJabatanPegawai.Text = listPegawai[0].Jabatan.NamaJabatan;

                            formMenu.PengaturanHakAkses(listPegawai[0].Jabatan);

                            formMenu.pegawaiLogin = listPegawai[0];

                            this.Close();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Username dan Password tidak boleh kosong", "Kesalahan");
                        labelUsername.Visible = false;
                        textBoxUsername.Focus();
                        if (textBoxPassword.Text == "")
                        {
                            labelPassword.Visible = true;
                        }
                        else
                        {
                            labelPassword.Visible = false;
                        }
                        if (textBoxServer.Text == "")
                        {
                            labelServer.Visible = true;
                        }
                        else
                        {
                            labelServer.Visible = false;
                        }
                        if (textBoxDatabase.Text == "")
                        {
                            labelDatabase.Visible = true;
                        }
                        else
                        {
                            labelDatabase.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi Gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
                    labelUsername.Visible = false;
                    textBoxUsername.Focus();
                    if (textBoxPassword.Text == "")
                    {
                        labelPassword.Visible = true;
                    }
                    else
                    {
                        labelPassword.Visible = false;
                    }
                    if (textBoxServer.Text == "")
                    {
                        labelServer.Visible = true;
                    }
                    else
                    {
                        labelServer.Visible = false;
                    }
                    if (textBoxDatabase.Text == "")
                    {
                        labelDatabase.Visible = true;
                    }
                    else
                    {
                        labelDatabase.Visible = false;
                    }
                }
            }
        }

        private void textBoxServer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                labelDatabase.Visible = false;
                textBoxDatabase.Focus();
                if (textBoxUsername.Text == "")
                {
                    labelUsername.Visible = true;
                }
                else
                {
                    labelUsername.Visible = false;
                }
                if (textBoxServer.Text == "")
                {
                    labelServer.Visible = true;
                }
                else
                {
                    labelServer.Visible = false;
                }
                if (textBoxPassword.Text == "")
                {
                    labelPassword.Visible = true;
                }
                else
                {
                    labelPassword.Visible = false;
                }
                e.IsInputKey = true;
            }
            if (e.KeyData == (Keys.Tab | Keys.Shift))
            {
                Height = 50 + panelLogin.Height + panelOption.Height;
                labelServer.Visible = false;
                textBoxServer.Focus();
                e.IsInputKey = true;
            }
            else if (e.KeyData == (Keys.Tab | Keys.Control))
            {
                Height = 50 + panelLogin.Height;
                labelUsername.Visible = false;
                textBoxUsername.Focus();
                e.IsInputKey = true;
            }
        }

        private void textBoxDatabase_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                labelServer.Visible = false;
                textBoxServer.Focus();
                if (textBoxUsername.Text == "")
                {
                    labelUsername.Visible = true;
                }
                else
                {
                    labelUsername.Visible = false;
                }
                if (textBoxDatabase.Text == "")
                {
                    labelDatabase.Visible = true;
                }
                else
                {
                    labelServer.Visible = false;
                }
                if (textBoxPassword.Text == "")
                {
                    labelPassword.Visible = true;
                }
                else
                {
                    labelPassword.Visible = false;
                }
                e.IsInputKey = true;
            }
            if (e.KeyData == (Keys.Tab | Keys.Shift))
            {
                Height = 50 + panelLogin.Height + panelOption.Height;
                labelServer.Visible = false;
                textBoxServer.Focus();
                e.IsInputKey = true;
            }
            else if (e.KeyData == (Keys.Tab | Keys.Control))
            {
                Height = 50 + panelLogin.Height;
                labelUsername.Visible = false;
                textBoxUsername.Focus();
                e.IsInputKey = true;
            }
        }

       
    }
}
