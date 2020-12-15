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
    public partial class FormAddCustomers : Form
    {
        public List<Customers> listOfCustomers = new List<Customers>();

        public FormAddCustomers()
        {
            InitializeComponent();
        }

        private void FormAddCustomers_Load(object sender, EventArgs e)
        {
            string kodeTerbaru = Customers.GenerateKode();

            textBoxId.Text = kodeTerbaru;
            textBoxId.Enabled = false;
            textBoxNama.Focus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Customers c = new Customers(textBoxId.Text, textBoxNama.Text, textBoxAlamat.Text, textBoxTelepon.Text);

                Customers.TambahData(c);

                MessageBox.Show("Data Customers telah tersimpan", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Penympanan gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
            }
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
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxId.Text = "";
            textBoxNama.Text = "";
            textBoxAlamat.Text = "";
            textBoxTelepon.Text = "";
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
            label1.BackColor = HexToColor("#228B22");
            label1.ForeColor = HexToColor("#F0F0F0");
            panel1.BackColor = HexToColor("#228B22");
            panel2.BackColor = HexToColor("#32CD32");
            radioButtonNormal.BackColor = HexToColor("#32CD32");
            radioButtonDark.BackColor = HexToColor("#32CD32");
            radioButtonRandom.BackColor = HexToColor("#32CD32");


        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            label1.BackColor = HexToColor("#000000");
            label1.ForeColor = HexToColor("#FFFFFF");
            panel1.BackColor = HexToColor("#000000");
            panel2.BackColor = HexToColor("#000000");
            radioButtonNormal.BackColor = HexToColor("#000000");
            radioButtonDark.BackColor = HexToColor("#000000");
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
            radioButtonRandom.BackColor = HexToColor("#" + RandomString(6));

        }
    }
}
