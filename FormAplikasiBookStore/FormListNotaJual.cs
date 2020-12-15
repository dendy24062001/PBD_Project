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
    public partial class FormListNotaJual : Form
    {
        List<NotaJual> listNotaJual = new List<NotaJual>();
        string kriteria = "";
        public FormListNotaJual()
        {
            InitializeComponent();
        }
        #region Method
        public void FormatDataGrid()
        {
            dataGridViewNotaJual.Columns.Add("NoNota", "No Nota");
            dataGridViewNotaJual.Columns.Add("Tanggal", "Tanggal");
            dataGridViewNotaJual.Columns.Add("IdCustomer", "Id Customers");
            dataGridViewNotaJual.Columns.Add("Nama", "Customer Name");
            dataGridViewNotaJual.Columns.Add("Alamat", "Customer Address");
            dataGridViewNotaJual.Columns.Add("IdPegawai", "Employee Id");
            dataGridViewNotaJual.Columns.Add("Nama", "Employee Name");
            dataGridViewNotaJual.Columns.Add("IdBuku", "Book Id");
            dataGridViewNotaJual.Columns.Add("Judul", "Title Of Book");
            dataGridViewNotaJual.Columns.Add("Harga", "Price");
            dataGridViewNotaJual.Columns.Add("Jumlah", "Amount");
            dataGridViewNotaJual.Columns.Add("JenisPembayaran", "Payment");

            dataGridViewNotaJual.Columns["NoNota"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["IdCustomer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Alamat"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["IdPegawai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["IdBuku"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Judul"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Harga"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaJual.Columns["JenisPembayaran"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewNotaJual.Columns["Harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewNotaJual.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewNotaJual.Columns["Harga"].DefaultCellStyle.Format = "#,###";

            dataGridViewNotaJual.AllowUserToAddRows = false;
            dataGridViewNotaJual.ReadOnly = true;
        }
        private void TampilDataGrid()
        {
            dataGridViewNotaJual.Rows.Clear();

            if (listNotaJual.Count > 0)
            {
                foreach (NotaJual n in listNotaJual)
                {
                    foreach (NotaJualDetil njd in n.ListNotaJualDetil)
                    {
                        dataGridViewNotaJual.Rows.Add(n.NoNota, n.Tanggal.ToShortDateString(), n.Customers.IdCustomer, n.Customers.Nama, n.Customers.Alamat,
                            n.Pegawai.IdPegawai, n.Pegawai.Nama, njd.Buku.IdBuku, njd.Buku.Nama, njd.Harga, njd.Jumlah, n.Pembayaran.JenisPembayaran);
                    }
                }
            }
            else
            {
                dataGridViewNotaJual.DataSource = null;
            }
        }
        #endregion
        private void FormListNotaJual_Load(object sender, EventArgs e)
        {
            FormatDataGrid();

            listNotaJual = NotaJual.BacaData("", "");

            TampilDataGrid();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAddSellBill formAddSellBill = new FormAddSellBill();
            formAddSellBill.Owner = this;
            formAddSellBill.Show();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            NotaJual.CetakNota(kriteria, textBoxSearch.Text, "daftar_nota_jual.txt", new Font("Courier New", 12));
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxSearch.Text == "No Nota")
            {
                kriteria = "N.NoNota";
            }
            else if (comboBoxSearch.Text == "Tanggal")
            {
                kriteria = "N.Tanggal";
            }
            else if (comboBoxSearch.Text == "Id Customers")
            {
                kriteria = "N.IdCustomer";
            }
            else if (comboBoxSearch.Text == "Id Pegawai")
            {
                kriteria = "N.IdPegawai";
            }
            else if (comboBoxSearch.Text == "Jenis Pembayaran")
            {
                kriteria = "p.JenisPembayaran";
            }
            listNotaJual = NotaJual.BacaData(kriteria, textBoxSearch.Text);
            TampilDataGrid();
        }
    }
}
