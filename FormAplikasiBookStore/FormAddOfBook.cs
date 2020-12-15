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
    public partial class FormAddOfBook : Form
    {
        private List<TipeBuku> listOfTipeBuku = new List<TipeBuku>();
        private List<Penerbit> listOfPenerbit = new List<Penerbit>();
        public FormAddOfBook()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                TipeBuku tipeBuku = (TipeBuku)comboBoxTipe.SelectedItem;
                Penerbit penerbit = (Penerbit)comboBoxPenerbit.SelectedItem;
                Buku b = new Buku(textBoxId.Text, textBoxJudul.Text, int.Parse(textBoxHarga.Text), int.Parse(textBoxStok.Text), tipeBuku, penerbit);
                Buku.TambahData(b);
                MessageBox.Show("Data buku berhasil ditambahkan", "informasi");
                textBoxJudul.Text = "";
                textBoxId.Text = "";
                textBoxHarga.Text = "";
                textBoxStok.Text = "";
                comboBoxTipe.Text = "";
                comboBoxTipe.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah data. Pesan kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxJudul.Text = "";
            textBoxId.Text = "";
           
            textBoxHarga.Text = "";
            textBoxStok.Text = "";
           
            comboBoxTipe.Text = "";
            comboBoxTipe.Focus();
            comboBoxPenerbit.Text = "";
        }

        private void FormAddOfBook_Load(object sender, EventArgs e)
        {
            textBoxId.MaxLength = 5;

            textBoxJudul.MaxLength = 45;

            listOfTipeBuku = TipeBuku.BacaData("", "");
            comboBoxTipe.DataSource = listOfTipeBuku;
            comboBoxTipe.DisplayMember = "JenisBuku";

            listOfPenerbit = Penerbit.BacaData("", "");
            comboBoxPenerbit.DataSource = listOfPenerbit;
            comboBoxPenerbit.DisplayMember = "NamaPenerbit";

            comboBoxTipe.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPenerbit.DropDownStyle = ComboBoxStyle.DropDownList;

            textBoxJudul.Text = "";
            textBoxHarga.Text = "";
            textBoxStok.Text = "";

            textBoxHarga.TextAlign = HorizontalAlignment.Right;
            textBoxStok.TextAlign = HorizontalAlignment.Right;
        }

        private void comboBoxTipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipeBuku tipedipilih = (TipeBuku)comboBoxTipe.SelectedItem;

            string kodeBuku = Buku.GenerateKode(tipedipilih);
            textBoxId.Text = kodeBuku;
            textBoxId.Enabled = false;
            textBoxJudul.Focus();
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

            label1.BackColor = HexToColor("#800000");
            label1.ForeColor = HexToColor("#F0F0F0");
            label2.BackColor = HexToColor("#CD5C5C");
            label2.ForeColor = HexToColor("#F0F0F0");
            label5.BackColor = HexToColor("#CD5C5C");
            label5.ForeColor = HexToColor("#F0F0F0");
            label3.BackColor = HexToColor("#CD5C5C");
            label3.ForeColor = HexToColor("#F0F0F0");
            label4.BackColor = HexToColor("#CD5C5C");
            label4.ForeColor = HexToColor("#F0F0F0");
            label8.BackColor = HexToColor("#CD5C5C");
            label8.ForeColor = HexToColor("#F0F0F0");
            label7.BackColor = HexToColor("#CD5C5C");
            label7.ForeColor = HexToColor("#F0F0F0");
            panel1.BackColor = HexToColor("#800000");
            panel2.BackColor = HexToColor("#CD5C5C");
            radioButtonNormal.BackColor = HexToColor("#CD5C5C");
            radioButtonDark.BackColor = HexToColor("#CD5C5C");
            radioButtonRandom.BackColor = HexToColor("#CD5C5C");
        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {

            label1.BackColor = HexToColor("#000000");
            label1.ForeColor = HexToColor("#F0F0F0");
            label2.BackColor = HexToColor("#000000");
            label2.ForeColor = HexToColor("#F0F0F0");
            label5.BackColor = HexToColor("#000000");
            label5.ForeColor = HexToColor("#F0F0F0");
            label3.BackColor = HexToColor("#000000");
            label3.ForeColor = HexToColor("#F0F0F0");
            label4.BackColor = HexToColor("#000000");
            label4.ForeColor = HexToColor("#F0F0F0");
            label8.BackColor = HexToColor("#000000");
            label8.ForeColor = HexToColor("#F0F0F0");
            label7.BackColor = HexToColor("#000000");
            label7.ForeColor = HexToColor("#F0F0F0");
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
            label2.BackColor = HexToColor("#" + RandomString(6));
            label2.ForeColor = HexToColor("#" + RandomString(6));
            label5.BackColor = HexToColor("#" + RandomString(6));
            label5.ForeColor = HexToColor("#" + RandomString(6));
            label3.BackColor = HexToColor("#" + RandomString(6));
            label3.ForeColor = HexToColor("#" + RandomString(6));
            label4.BackColor = HexToColor("#" + RandomString(6));
            label4.ForeColor = HexToColor("#" + RandomString(6));
            label8.BackColor = HexToColor("#" + RandomString(6));
            label8.ForeColor = HexToColor("#" + RandomString(6));
            label7.BackColor = HexToColor("#" + RandomString(6));
            label7.ForeColor = HexToColor("#" + RandomString(6));
            panel1.BackColor = HexToColor("#" + RandomString(6));
            panel2.BackColor = HexToColor("#" + RandomString(6));
            radioButtonNormal.BackColor = HexToColor("#" + RandomString(6));
            radioButtonDark.BackColor = HexToColor("#" + RandomString(6));
            radioButtonRandom.BackColor = HexToColor("#" + RandomString(6));
        }
    }
}
