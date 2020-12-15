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
    public partial class FormAddTypeOfBooks : Form
    {
        public FormAddTypeOfBooks()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                TipeBuku t = new TipeBuku(textBoxId.Text, textBoxJenisBuku.Text);

                TipeBuku.TambahData(t);

                MessageBox.Show("Data Tipe Buku telah tersimpan", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Penympanan gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxId.Text = "";
            textBoxJenisBuku.Text = "";
        }

        
        private void FormAddTypeOfBooks_Load(object sender, EventArgs e)
        {
            string kodeTerbaru = TipeBuku.GenerateKode();

            textBoxId.Text = kodeTerbaru;
            textBoxId.Enabled = false;
            textBoxJenisBuku.Focus();
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
            label1.BackColor = HexToColor("#FF8C00");
            label1.ForeColor = HexToColor("#F0F0F0");
            label2.BackColor = HexToColor("#F5DEB3");
            label2.ForeColor = HexToColor("#000000");
            label3.BackColor = HexToColor("#F5DEB3");
            label3.ForeColor = HexToColor("#000000");
            panel1.BackColor = HexToColor("#FF8C00");
            panel2.BackColor = HexToColor("#F5DEB3");
            radioButtonNormal.BackColor = HexToColor("#F5DEB3");
            radioButtonNormal.ForeColor = HexToColor("#000000");
            radioButtonRandom.BackColor = HexToColor("#F5DEB3");
            radioButtonRandom.ForeColor = HexToColor("#000000");
            radioButtonDark.BackColor = HexToColor("#F5DEB3");
            radioButtonDark.ForeColor = HexToColor("#000000");
            buttonClear.ForeColor = HexToColor("#000000");
        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#000000");
            label1.ForeColor = HexToColor("#F0F0F0");
            label2.BackColor = HexToColor("#000000");
            label2.ForeColor = HexToColor("#F0F0F0");
            label3.BackColor = HexToColor("#000000");
            label3.ForeColor = HexToColor("#F0F0F0");
            panel1.BackColor = HexToColor("#000000");
            panel2.BackColor = HexToColor("#000000");
            radioButtonNormal.BackColor = HexToColor("#000000");
            radioButtonNormal.ForeColor = HexToColor("#F0F0F0");
            radioButtonRandom.BackColor = HexToColor("#000000");
            radioButtonRandom.ForeColor = HexToColor("#F0F0F0");
            radioButtonDark.BackColor = HexToColor("#000000");
            radioButtonDark.ForeColor = HexToColor("#F0F0F0");
            buttonClear.ForeColor = HexToColor("#F0F0F0");
        }

        private void radioButtonRandom_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#" + RandomString(6));
            label1.ForeColor = HexToColor("#" + RandomString(6));
            label2.BackColor = HexToColor("#" + RandomString(6));
            label2.ForeColor = HexToColor("#" + RandomString(6));
            label3.BackColor = HexToColor("#" + RandomString(6));
            label3.ForeColor = HexToColor("#" + RandomString(6));
            panel1.BackColor = HexToColor("#" + RandomString(6));
            panel2.BackColor = HexToColor("#" + RandomString(6));
            radioButtonNormal.BackColor = HexToColor("#" + RandomString(6));
            radioButtonNormal.ForeColor = HexToColor("#" + RandomString(6));
            radioButtonDark.BackColor = HexToColor("#" + RandomString(6));
            radioButtonDark.ForeColor = HexToColor("#" + RandomString(6));
            buttonClear.ForeColor = HexToColor("#" + RandomString(6));
            radioButtonRandom.BackColor = HexToColor("#" + RandomString(6));

        }
    }
}
