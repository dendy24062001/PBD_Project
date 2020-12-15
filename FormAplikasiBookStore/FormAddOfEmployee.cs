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
    public partial class FormAddOfEmployee : Form
    {
        private List<Jabatan> listOfJabatan = new List<Jabatan>();

        public FormAddOfEmployee()
        {
            InitializeComponent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxUsername.Text = "";
            textBoxPass.Text = "";
            textBoxNama.Text = "";
            textBoxId.Text = "";
            textBoxGaji.Text = "";
            textBoxAlamat.Text = "";
            comboBoxJabatan.SelectedIndex = 0;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Jabatan j = (Jabatan)comboBoxJabatan.SelectedItem;
                Pegawai p = new Pegawai(textBoxId.Text, textBoxNama.Text, dateTimePickerTanggalLahir.Value, textBoxAlamat.Text, int.Parse(textBoxGaji.Text), textBoxUsername.Text, textBoxPass.Text, j);
                Pegawai.TambahData(p);

                MessageBox.Show("Data Pegawai Telah DiTambahkan", "Informasi");
                buttonClear_Click(buttonSave, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Penambahan Pegawai Gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void comboBoxJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Jabatan jabatanDipilih = (Jabatan)comboBoxJabatan.SelectedItem;
            string kodeBaru = Pegawai.GenerateKode(jabatanDipilih);
            textBoxId.Text = kodeBaru;
            textBoxId.Enabled = false;
            textBoxNama.Focus();
        }

        private void FormAddOfEmployee_Load(object sender, EventArgs e)
        {
            textBoxId.Enabled = false;
            textBoxNama.MaxLength = 45;

            listOfJabatan = Jabatan.BacaData("", "");
            comboBoxJabatan.DataSource = listOfJabatan;
            comboBoxJabatan.DisplayMember = "NamaJabatan";

            comboBoxJabatan.DropDownStyle = ComboBoxStyle.DropDownList;
            textBoxPass.MaxLength = 8;
            textBoxGaji.Text = "";
            textBoxNama.Text = "";
            textBoxAlamat.Text = "";
            textBoxUsername.Text = "";
            textBoxPass.Text = "";
           
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
            labelAlamat.ForeColor = HexToColor("#000000");
            labelTanggal.ForeColor = HexToColor("#000000");
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
            labelAlamat.ForeColor = HexToColor("#FFFFFF");
            labelTanggal.ForeColor = HexToColor("#FFFFFF");
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
            labelAlamat.ForeColor = HexToColor("#" + RandomString(6));
            labelTanggal.ForeColor = HexToColor("#" + RandomString(6));
            radioButtonRandom.BackColor = HexToColor("#" + RandomString(6));

        }

        private void textBoxPass_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPass.TextLength > textBoxPass.MaxLength)
            {
                MessageBox.Show("Maksimal password adalah 8 digit");
            }
        }
    }
}
