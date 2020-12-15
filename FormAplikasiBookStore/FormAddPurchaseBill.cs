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
    public partial class FormAddPurchaseBill : Form
    {
        FormMenu formMenu;
        NotaBeli notaBeli;
        public List<Buku> listBuku = new List<Buku>();
        public List<Supplier> listSupplier = new List<Supplier>();
        private List<Pembayaran> listPembayaran = new List<Pembayaran>();
        public FormAddPurchaseBill()
        {
            InitializeComponent();
        }

        private void FormAddPurchaseBill_Load(object sender, EventArgs e)
        {
            listSupplier = Supplier.BacaData("", "");
            comboBoxSupplier.DataSource = listSupplier;
            comboBoxSupplier.DisplayMember = "Nama";
            comboBoxSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

            listPembayaran = Pembayaran.BacaData("", "");
            comboBoxPayment.DataSource = listPembayaran;
            comboBoxPayment.DisplayMember = "JenisPembayaran";
            comboBoxPayment.DropDownStyle = ComboBoxStyle.DropDownList;

            textBoxNoNota.Text = NotaBeli.GenerateNoNota();

            textBoxBarcode.MaxLength = 13;

            FormatDataGrid();
            dateTimePickerDate.Value = DateTime.Now;
        }
        private void FormatDataGrid()
        {
            dataGridViewNotaBeli.Columns.Clear();

            dataGridViewNotaBeli.Columns.Add("IdBuku", "Id Buku");
            dataGridViewNotaBeli.Columns.Add("Judul", "Judul Buku");
            dataGridViewNotaBeli.Columns.Add(textBoxHarga.Text.ToString(), "Harga Beli");
            dataGridViewNotaBeli.Columns.Add("Jumlah", "Jumlah");
            dataGridViewNotaBeli.Columns.Add("SubTotal", "SubTotal");

            dataGridViewNotaBeli.Columns["IdBuku"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Judul"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns[textBoxHarga.Text.ToString()].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["SubTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewNotaBeli.Columns[textBoxHarga.Text.ToString()].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewNotaBeli.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewNotaBeli.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewNotaBeli.Columns[textBoxHarga.Text.ToString()].DefaultCellStyle.Format = "#,###";
            dataGridViewNotaBeli.Columns["SubTotal"].DefaultCellStyle.Format = "#,###";

            dataGridViewNotaBeli.AllowUserToAddRows = false;
            dataGridViewNotaBeli.ReadOnly = true;
        }
        private int HitungGrandTotal()
        {
            int grandTotal = 0;
            for (int i = 0; i < dataGridViewNotaBeli.Rows.Count; i++)
            {
                int subTotal = int.Parse(dataGridViewNotaBeli.Rows[i].Cells["SubTotal"].Value.ToString());
                grandTotal = grandTotal + subTotal;
            }
            return grandTotal;
        }
        private void comboBoxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Supplier supplierDipilih = (Supplier)comboBoxSupplier.SelectedItem;
            labelAlamat.Text = supplierDipilih.Alamat;

            textBoxBarcode.Focus();
        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int subTotal = int.Parse(textBoxHarga.Text) * int.Parse(textBoxJumlah.Text);

                dataGridViewNotaBeli.Rows.Add(labelKode, labelNama, textBoxHarga.Text, textBoxJumlah.Text, subTotal);

                labelGrandTotal.Text = HitungGrandTotal().ToString("#,###");

                textBoxBarcode.Text = "";
                textBoxJumlah.Text = "";
                labelNama.Text = "";
                labelKode.Text = "";
                textBoxJumlah.Text = "";
                textBoxBarcode.Focus();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Supplier supplierDipilih = (Supplier)comboBoxSupplier.SelectedItem;
                Pembayaran pembayaranDipilih = (Pembayaran)comboBoxPayment.SelectedItem;
                notaBeli = new NotaBeli(textBoxNoNota.Text, dateTimePickerDate.Value,formMenu.pegawaiLogin,
                    supplierDipilih, pembayaranDipilih);

                for (int i = 0; i < dataGridViewNotaBeli.Rows.Count; i++)
                {
                    string IdBuku = dataGridViewNotaBeli.Rows[i].Cells["IdBuku"].Value.ToString();

                    listBuku = Buku.BacaData("B.IdBuku", IdBuku);

                    int harga = int.Parse(dataGridViewNotaBeli.Rows[i].Cells[textBoxHarga.Text].Value.ToString());
                    int jumlah = int.Parse(dataGridViewNotaBeli.Rows[i].Cells["Jumlah"].Value.ToString());

                    notaBeli.TambahNotaBeliDetil(listBuku[0], harga, jumlah);
                }

                NotaBeli.TambahData(notaBeli);
                MessageBox.Show("Data Nota Pembelian Telah Tersimpan.", "Informasi");
                
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
                NotaBeli.CetakNota("NoNota", textBoxNoNota.Text, "Purchase_bill.txt", new Font("Courier New", 12));

                MessageBox.Show("Nota Beli telah tercetak");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nota Beli Gagal dicetak. Pesan Kesalahan : " + ex.Message);
            }
        }
    }
}
