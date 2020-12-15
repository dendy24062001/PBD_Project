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
    public partial class FormAddSellBill : Form
    {
        private List<Customers> listCustomers = new List<Customers>();
        private List<Buku> listBuku = new List<Buku>();
        private List<Pembayaran> listPembayaran = new List<Pembayaran>();
        NotaJual notaJual;
        FormMenu formMenu;
        public FormAddSellBill()
        {
            InitializeComponent();
        }
        #region Method
        private void FormatDataGrid()
        {
            dataGridViewNotaJual.Columns.Add("IdBuku", "Id Buku");
            dataGridViewNotaJual.Columns.Add("Judul", "Judul Buku");
            dataGridViewNotaJual.Columns.Add("HargaJual", "Harga Jual");
            dataGridViewNotaJual.Columns.Add("Jumlah", "Jumlah");
            dataGridViewNotaJual.Columns.Add("SubTotal", "SubTotal");

            dataGridViewNotaJual.Columns["IdBuku"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Judul"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["HargaJual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["SubTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewNotaJual.Columns["HargaJual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewNotaJual.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewNotaJual.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewNotaJual.Columns["HargaJual"].DefaultCellStyle.Format = "#,###";
            dataGridViewNotaJual.Columns["SubTotal"].DefaultCellStyle.Format = "#,###";

            dataGridViewNotaJual.AllowUserToAddRows = false;
            dataGridViewNotaJual.ReadOnly = true;
        }
        private int HitungGrandTotal()
        {
            int grandTotal = 0;
            for (int i = 0; i < dataGridViewNotaJual.Rows.Count; i++)
            {
                int subTotal = int.Parse(dataGridViewNotaJual.Rows[i].Cells["SubTotal"].Value.ToString());
                grandTotal = grandTotal + subTotal;
            }
            return grandTotal;
        }
        #endregion
        private void FormAddSellBill_Load(object sender, EventArgs e)
        {
            listCustomers = Customers.BacaData("", "");
            comboBoxPelanggan.DataSource = listCustomers;
            comboBoxPelanggan.DisplayMember = "Nama";
            comboBoxPelanggan.DropDownStyle = ComboBoxStyle.DropDownList;


            formMenu = (FormMenu)this.Owner.MdiParent;
            labelKodePegawai.Text = formMenu.pegawaiLogin.IdPegawai.ToString();
            labelNamaPegawai.Text = formMenu.pegawaiLogin.Nama;

            textBoxNoNota.Text = NotaJual.GenerateNoNota();

            textBoxBarcode.MaxLength = 13;

            FormatDataGrid();
            dateTimePickerDate.Value = DateTime.Now;
            textBoxNoNota.Enabled = false;
        }

        private void comboBoxPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Customers pelangganDipilih = (Customers)comboBoxPelanggan.SelectedItem;
            labelAlamat.Text = pelangganDipilih.Alamat;

            textBoxBarcode.Focus();
        }
        private void textBoxBarcode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxBarcode.Text.Length == textBoxBarcode.MaxLength)
            {
                listBuku = Buku.BacaData("Barcode", textBoxBarcode.Text);
                if (listBuku.Count > 0)
                {
                    labelKode.Text = listBuku[0].IdBuku.ToString();
                    labelNama.Text = listBuku[0].Nama.ToString();
                    labelHarga.Text = listBuku[0].Harga.ToString();
                    textBoxJumlah.Text = "1";
                    textBoxJumlah.Focus();
                }
                else
                {
                    MessageBox.Show("Buku Tidak Ditemukan");
                    textBoxBarcode.Text = "";
                }

            }
        }

        private void textBoxJumlah_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int subTotal = int.Parse(labelHarga.Text) * int.Parse(textBoxJumlah.Text);

                dataGridViewNotaJual.Rows.Add(labelKode, labelNama, labelHarga.Text, textBoxJumlah.Text, subTotal);

                labelGrandTotal.Text = HitungGrandTotal().ToString("#,###");

                textBoxBarcode.Text = "";
                labelHarga.Text = "";
                labelKode.Text = "";
                labelNama.Text = "";
                textBoxJumlah.Text = "";
                textBoxBarcode.Focus();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Customers pelangganDipilih = (Customers)comboBoxPelanggan.SelectedItem;
                Pembayaran pembayaranDipilih = (Pembayaran)comboBoxPayment.SelectedItem;
                notaJual = new NotaJual(textBoxNoNota.Text, dateTimePickerDate.Value,
                     formMenu.pegawaiLogin, pelangganDipilih, pembayaranDipilih);

                for (int i = 0; i < dataGridViewNotaJual.Rows.Count; i++)
                {
                    string idBuku = dataGridViewNotaJual.Rows[i].Cells["IdBuku"].Value.ToString();

                    listBuku = Buku.BacaData("B.IdBuku", idBuku);

                    int harga = int.Parse(dataGridViewNotaJual.Rows[i].Cells["HargaJual"].Value.ToString());
                    int jumlah = int.Parse(dataGridViewNotaJual.Rows[i].Cells["Jumlah"].Value.ToString());

                    notaJual.TambahNotaJualDetil(listBuku[0], harga, jumlah);
                }

                NotaJual.TambahData(notaJual);
                MessageBox.Show("Data Nota Jual Telah Tersimpan.", "Informasi");
                buttonPrint_Click(sender, e);// untuk memanggil event handler button cetak
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Menyimpan Nota. Pesan Kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                NotaJual.CetakNota("NoNota", textBoxNoNota.Text, "nota_jual.txt", new Font("Courier New", 12));

                MessageBox.Show("Nota Jual telah tercetak");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nota Jual Gagal dicetak. Pesan Kesalahan : " + ex.Message);
            }
        }
    }
}
