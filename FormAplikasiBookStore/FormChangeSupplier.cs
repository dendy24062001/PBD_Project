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
    public partial class FormChangeSupplier : Form
    {
        private List<Supplier> listOfSupplier = new List<Supplier>();

        public FormChangeSupplier()
        {
            InitializeComponent();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            try
            {
                Supplier s = new Supplier(textBoxId.Text, textBoxNama.Text, textBoxAlamat.Text);

                Supplier.UbahData(s);

                MessageBox.Show("Data Supplier berhasil diubah", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pengubahan gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxId.Text.Length == textBoxId.MaxLength)
            {
                listOfSupplier = Supplier.BacaData("IdSupplier", textBoxId.Text);
                if (listOfSupplier.Count > 0)
                {
                    textBoxAlamat.Text = listOfSupplier[0].Alamat;
                    textBoxNama.Text = listOfSupplier[0].Nama;
                    textBoxNama.Focus();
                }
                else
                {
                    MessageBox.Show("Kode kategori tidak ditemukan", "Kesalahan");
                    textBoxNama.Text = "";
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxAlamat.Text = "";
            textBoxNama.Text = "";
            textBoxId.Text = "";
        }

       
        private void FormChangeSupplier_Load(object sender, EventArgs e)
        {
            textBoxId.MaxLength = 1;
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
            label1.BackColor = HexToColor("#D2691E");
            label1.ForeColor = HexToColor("#F0F0F0");
            panel1.BackColor = HexToColor("#D2691E");
            panel2.BackColor = HexToColor("#F4A460");
            radioButtonNormal.BackColor = HexToColor("#F4A460");
            radioButtonNormal.ForeColor = HexToColor("#000000");
            radioButtonDark.BackColor = HexToColor("#F4A460");
            radioButtonDark.ForeColor = HexToColor("#000000");
            radioButtonRandom.BackColor = HexToColor("#F4A460");
            radioButtonRandom.ForeColor = HexToColor("#000000");
            label3.BackColor = HexToColor("#F4A460");
            label3.ForeColor = HexToColor("#000000");
            label4.BackColor = HexToColor("#F4A460");
            label4.ForeColor = HexToColor("#000000");
            labelNamaBarang.BackColor = HexToColor("#F4A460");
            labelNamaBarang.ForeColor = HexToColor("#000000");
        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#000000");
            label1.ForeColor = HexToColor("#F0F0F0");
            panel1.BackColor = HexToColor("#000000");
            panel2.BackColor = HexToColor("#000000");
            radioButtonNormal.BackColor = HexToColor("#000000");
            radioButtonNormal.ForeColor = HexToColor("#F0F0F0");
            radioButtonRandom.BackColor = HexToColor("#000000");
            radioButtonRandom.ForeColor = HexToColor("#F0F0F0");
            radioButtonDark.BackColor = HexToColor("#000000");
            radioButtonDark.ForeColor = HexToColor("#F0F0F0");
            label3.BackColor = HexToColor("#000000");
            label3.ForeColor = HexToColor("#F0F0F0");
            label4.BackColor = HexToColor("#000000");
            label4.ForeColor = HexToColor("#F0F0F0");
            labelNamaBarang.BackColor = HexToColor("#000000");
            labelNamaBarang.ForeColor = HexToColor("#F0F0F0");
        }

        private void radioButtonRandom_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#" + RandomString(6));
            label1.ForeColor = HexToColor("#" + RandomString(6));
            panel1.BackColor = HexToColor("#" + RandomString(6));
            panel2.BackColor = HexToColor("#" + RandomString(6));
            radioButtonNormal.BackColor = HexToColor("#" + RandomString(6));
            radioButtonNormal.ForeColor = HexToColor("#" + RandomString(6));
            radioButtonDark.BackColor = HexToColor("#" + RandomString(6));
            radioButtonDark.ForeColor = HexToColor("#" + RandomString(6));
            radioButtonRandom.BackColor = HexToColor("#" + RandomString(6));
            radioButtonRandom.ForeColor = HexToColor("#" + RandomString(6));
            label3.BackColor = HexToColor("#" + RandomString(6));
            label3.ForeColor = HexToColor("#" + RandomString(6));
            label4.BackColor = HexToColor("#" + RandomString(6));
            label4.ForeColor = HexToColor("#" + RandomString(6));
            labelNamaBarang.BackColor = HexToColor("#" + RandomString(6));
            labelNamaBarang.ForeColor = HexToColor("#" + RandomString(6));
        }
    }
}
