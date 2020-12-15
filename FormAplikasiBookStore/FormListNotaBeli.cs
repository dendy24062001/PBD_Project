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
    public partial class FormListNotaBeli : Form
    {
        string kriteria = "";
        private List<NotaBeli> listNotaBeli = new List<NotaBeli>();
        public FormListNotaBeli()
        {
            InitializeComponent();
        }

        private void FormListNotaBeli_Load(object sender, EventArgs e)
        {
            FormatDataGrid();
            listNotaBeli = NotaBeli.BacaData("", "");
            TampilDataGrid();
        }
        private void FormatDataGrid()
        {
            dataGridViewNotaBeli.Columns.Add("NoNota", "No Nota");
            dataGridViewNotaBeli.Columns.Add("Tanggal", "Tanggal");
            dataGridViewNotaBeli.Columns.Add("IdPegawai", "Id Employee");
            dataGridViewNotaBeli.Columns.Add("Nama", "Employee Name");
            dataGridViewNotaBeli.Columns.Add("IdSupplier", "Id Supplier");
            dataGridViewNotaBeli.Columns.Add("Nama", "Nama Supplier");
            dataGridViewNotaBeli.Columns.Add("Alamat", "Alamat Supplier");
            dataGridViewNotaBeli.Columns.Add("IdBuku", "Id Buku");
            dataGridViewNotaBeli.Columns.Add("Judul", "Judul Buku");
            dataGridViewNotaBeli.Columns.Add("Harga", "Harga");
            dataGridViewNotaBeli.Columns.Add("Jumlah", "Jumlah");
            dataGridViewNotaBeli.Columns.Add("IdPembayaran", "ID Pembayaran");
            dataGridViewNotaBeli.Columns.Add("JenisPembayaran", "Jenis Pembayaran");

            dataGridViewNotaBeli.Columns["NoNota"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["IdPegawai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["IdSupplier"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Alamat"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["IdBuku"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Judul"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Harga"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["IdPembayaran"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNotaBeli.Columns["JenisPembayaran"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewNotaBeli.Columns["Harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewNotaBeli.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewNotaBeli.Columns["Harga"].DefaultCellStyle.Format = "#,###";

            dataGridViewNotaBeli.AllowUserToAddRows = false;
            dataGridViewNotaBeli.ReadOnly = true;
        }
        private void TampilDataGrid()
        {
            dataGridViewNotaBeli.Rows.Clear();

            if (listNotaBeli.Count > 0)
            {
                foreach (NotaBeli n in listNotaBeli)
                {
                    foreach (NotaBeliDetil nbd in n.ListNotaBeliDetil)
                    {
                        dataGridViewNotaBeli.Rows.Add(n.NoNota, n.Tanggal.ToShortDateString(), n.Pegawai.IdPegawai, n.Pegawai.Nama, n.Supplier.IdSupplier, n.Supplier.Nama, n.Supplier.Alamat, nbd.Buku.IdBuku,nbd.Buku.Nama, nbd.Harga, nbd.Jumlah, n.Pembayaran.IdPembayaran ,n.Pembayaran.JenisPembayaran);
                    }
                }
            }
            else
            {
                dataGridViewNotaBeli.DataSource = null;
            }
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            
            if (comboBoxSearch.Text=="No Nota")
            {
                kriteria = "N.NoNota";
            }
            else if(comboBoxSearch.Text == "Tanggal")
            {
                kriteria = "N.Tanggal";
            }
            else if(comboBoxSearch.Text == "Id Pegawai")
            {
                kriteria = "N.IdPegawai";
            }
            else if(comboBoxSearch.Text == "Id Supplier")
            {
                kriteria = "N.IdSupplier";
            }
            else if(comboBoxSearch.Text == "Jenis Pembayaran")
            {
                kriteria = "p.JenisPembayaran";
            }
            listNotaBeli = NotaBeli.BacaData(kriteria, textBoxSearch.Text);
            TampilDataGrid();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            NotaBeli.CetakNota(kriteria, textBoxSearch.Text, "List_Of_Purchase_Bill.txt", new Font("Courier New", 12));
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAddPurchaseBill formAddPurchaseBill = new FormAddPurchaseBill();
            formAddPurchaseBill.Owner = this;
            formAddPurchaseBill.ShowDialog();
        }
    }
}
