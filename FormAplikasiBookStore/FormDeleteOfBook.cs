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
    public partial class FormDeleteOfBook : Form
    {
        private List<TipeBuku> listOfTipeBuku=new List<TipeBuku>();
        private List<Buku> listOfBuku = new List<Buku>();
        private List<Penerbit> listOfPenerbit = new List<Penerbit>();

        public FormDeleteOfBook()
        {
            InitializeComponent();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult konfirmasi = MessageBox.Show("Data Buku akan terhapus, Apakah anda yakin ?", "Konfirmasi", MessageBoxButtons.YesNo);
                if (konfirmasi == System.Windows.Forms.DialogResult.Yes)
                {
                    TipeBuku tipeDipilih = (TipeBuku)comboBoxTipeBuku.SelectedItem;
                    Penerbit penerbit = (Penerbit)comboBoxPenerbit.SelectedItem;
                    Buku b = new Buku(textBoxId.Text, textBoxJudul.Text, int.Parse(textBoxHarga.Text), int.Parse(textBoxStok.Text), tipeDipilih, penerbit);
                    
                    Buku.HapusData(b);
                    MessageBox.Show("Data Buku telah terhapus", "Info");
                }
                else
                {
                    MessageBox.Show("Data Buku tidak jadi di hapus", "Info");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Penghapusan gagal. Pesan Kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxHarga.Text = "";
            comboBoxTipeBuku.Text = "";
            textBoxStok.Text = "";
            textBoxId.Text = "";
            textBoxJudul.Text = "";
            comboBoxPenerbit.Text = "";
        }

       

        private void FormDeleteOfBook_Load(object sender, EventArgs e)
        {
            textBoxId.MaxLength = 5;
            textBoxJudul.MaxLength = 45;

            listOfTipeBuku = TipeBuku.BacaData("", "");
            comboBoxTipeBuku.DataSource = listOfTipeBuku;
            comboBoxTipeBuku.DisplayMember = "JenisBuku";
            comboBoxTipeBuku.DropDownStyle = ComboBoxStyle.DropDownList;

            listOfPenerbit = Penerbit.BacaData("", "");
            comboBoxPenerbit.DataSource = listOfPenerbit;
            comboBoxPenerbit.DisplayMember = "NamaPenerbit";
            comboBoxPenerbit.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBoxPenerbit.Enabled = false;
            comboBoxTipeBuku.Enabled = false;
            textBoxHarga.Enabled = false;
            textBoxHarga.Enabled = false;
            textBoxJudul.Enabled = false;
            textBoxStok.Enabled = false;

        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxId.Text.Length == textBoxId.MaxLength)
            {
                listOfBuku = Buku.BacaData("IdBuku", textBoxId.Text);
                if (listOfBuku.Count > 0)
                {
                    textBoxJudul.Text = listOfBuku[0].Nama;
                    comboBoxPenerbit.Text = (listOfBuku[0].Penerbit.NamaPenerbit).ToString();

                    textBoxHarga.Text = (listOfBuku[0].Harga).ToString();
                    textBoxStok.Text = (listOfBuku[0].Stok).ToString();
                    comboBoxTipeBuku.Text = (listOfBuku[0].TipeBuku.JenisBuku).ToString();
                    textBoxJudul.Focus();
                    comboBoxTipeBuku.Enabled = false;
                    textBoxJudul.Enabled = false;
                    textBoxHarga.Enabled = false;
                    textBoxStok.Enabled = false;
                    comboBoxPenerbit.Enabled = false;
                }
                else
                {
                    MessageBox.Show("kode Buku tidak ditemukan.", "Kesalahan");
                    textBoxHarga.Text = "";
                    comboBoxTipeBuku.Text = "";
                    textBoxStok.Text = "";
                    textBoxId.Text = "";
                    textBoxJudul.Text = "";
                }
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
            label9.BackColor = HexToColor("#CD5C5C");
            label9.ForeColor = HexToColor("#F0F0F0");
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
            label9.BackColor = HexToColor("#000000");
            label9.ForeColor = HexToColor("#F0F0F0");
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
            label9.BackColor = HexToColor("#" + RandomString(6));
            label9.ForeColor = HexToColor("#" + RandomString(6));
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
