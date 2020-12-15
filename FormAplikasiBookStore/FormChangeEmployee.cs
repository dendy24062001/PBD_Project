using ClassLibraryBookStore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormAplikasiBookStore
{
    public partial class FormChangeEmployee : Form
    {
        private List<Pegawai> listOfPegawai=new List<Pegawai>();

        public FormChangeEmployee()
        {
            InitializeComponent();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            try
            {
                Jabatan j = (Jabatan)comboBoxJabatan.SelectedItem;
                Pegawai p = new Pegawai(textBoxId.Text, textBoxNama.Text, dateTimePickerTanggal.Value, textBoxAlamat.Text, int.Parse(textBoxGaji.Text), textBoxUsername.Text, textBoxPass.Text, j);
                Pegawai.UbahData(p);

                MessageBox.Show("Data Pegawai Telah DiUbah", "Informasi");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Pengubahan Pegawai Gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxUsername.Text = "";
            textBoxPass.Text = "";
            textBoxNama.Text = "";
            textBoxId.Text = "";
            textBoxGaji.Text = "";
            textBoxAlamat.Text = "";

        }

       

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxId.TextLength == textBoxId.MaxLength)
            {
                listOfPegawai = Pegawai.BacaData("IdPegawai", textBoxId.Text);
                if (listOfPegawai.Count > 0)
                {
                    textBoxNama.Text = listOfPegawai[0].Nama;
                    textBoxAlamat.Text = listOfPegawai[0].Alamat;
                    textBoxGaji.Text = (listOfPegawai[0].Gaji).ToString();
                    textBoxPass.Text = listOfPegawai[0].Password.ToString();
                    textBoxUsername.Text = listOfPegawai[0].Username;
                    comboBoxJabatan.Text = (listOfPegawai[0].Jabatan.NamaJabatan).ToString();


                    comboBoxJabatan.Enabled = false;
                    textBoxNama.Focus();
                }
                else
                {
                    MessageBox.Show("Kode Pegawai tidak ditemukan.", "Kesalahan");
                    textBoxNama.Text = "";
                }
            }
        }

        private void FormChangeEmployee_Load(object sender, EventArgs e)
        {
            textBoxId.MaxLength = 4;
            List<Jabatan> listOfJabatan = Jabatan.BacaData("", "");
            comboBoxJabatan.DataSource = listOfJabatan;
            comboBoxJabatan.DisplayMember = "NamaJabatan";
            comboBoxJabatan.DropDownStyle = ComboBoxStyle.DropDownList;
            textBoxUsername.Text = "";
            textBoxPass.Text = "";
            textBoxNama.Text = "";
            textBoxId.Text = "";
            textBoxGaji.Text = "";
            textBoxAlamat.Text = "";
            comboBoxJabatan.Enabled = false;
        }
        public static Color HexToColor(String hexString)
        {
            Color actColor;
            int r, g, b;
            r = 0;
            g = 0;
            b = 0;
            if ((hexString.StartsWith("#")) && (hexString.Length == 7))
            {

                r = int.Parse(hexString.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                g = int.Parse(hexString.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                b = int.Parse(hexString.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                actColor = Color.FromArgb(r, g, b);
            }
            else
            {
                actColor = Color.White;
            }
            return actColor;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEF0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void radioButtonNormal_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#00008B");
            label1.ForeColor = HexToColor("#F0F0F0");
            panel1.BackColor = HexToColor("#00008B");
            panel2.BackColor = HexToColor("#6495ED");
            radioButtonNormal.BackColor = HexToColor("#6495ED");
            radioButtonDark.BackColor = HexToColor("#6495ED");
            label2.ForeColor = HexToColor("#000000");
            label3.ForeColor = HexToColor("#000000");
            label4.ForeColor = HexToColor("#000000");
            label6.ForeColor = HexToColor("#000000");
            label8.ForeColor = HexToColor("#000000");
            label7.ForeColor = HexToColor("#000000");
            labelNamaBarang.ForeColor = HexToColor("#000000");
            label5.ForeColor = HexToColor("#000000");
            radioButtonRandom.BackColor = HexToColor("#6495ED");

        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#000000");
            label1.ForeColor = HexToColor("#FFFFFF");
            panel1.BackColor = HexToColor("#000000");
            panel2.BackColor = HexToColor("#000000");
            radioButtonNormal.BackColor = HexToColor("#000000");
            radioButtonDark.BackColor = HexToColor("#000000");
            label2.ForeColor = HexToColor("#FFFFFF");
            label3.ForeColor = HexToColor("#FFFFFF");
            label4.ForeColor = HexToColor("#FFFFFF");
            label6.ForeColor = HexToColor("#FFFFFF");
            label8.ForeColor = HexToColor("#FFFFFF");
            label7.ForeColor = HexToColor("#FFFFFF");
            labelNamaBarang.ForeColor = HexToColor("#FFFFFF");
            label5.ForeColor = HexToColor("#FFFFFF");
            radioButtonRandom.BackColor = HexToColor("#000000");

        }

        private void radioButtonRandom_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#" + RandomString(6));
            label1.ForeColor = HexToColor("#" + RandomString(6));
            panel1.BackColor = HexToColor("#" + RandomString(6));
            panel2.BackColor = HexToColor("#" + RandomString(6));
            radioButtonNormal.BackColor = HexToColor("#" + RandomString(6));
            radioButtonDark.BackColor = HexToColor("#" + RandomString(6));
            label2.ForeColor = HexToColor("#" + RandomString(6));
            label3.ForeColor = HexToColor("#" + RandomString(6));
            label4.ForeColor = HexToColor("#" + RandomString(6));
            label6.ForeColor = HexToColor("#" + RandomString(6));
            label8.ForeColor = HexToColor("#" + RandomString(6));
            label7.ForeColor = HexToColor("#" + RandomString(6));
            labelNamaBarang.ForeColor = HexToColor("#" + RandomString(6));
            label5.ForeColor = HexToColor("#" + RandomString(6));
            radioButtonRandom.BackColor = HexToColor("#" + RandomString(6));

        }
    }
}
