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
    public partial class FormListBuku : Form
    {
        List<Buku> listOfBook = new List<Buku>();

        public FormListBuku()
        {
            InitializeComponent();
        }
        private void FormatDataGrid()
        {
            //menambah data di dataGridView
            dataGridViewBook.Columns.Add("IdBuku", "Id Buku");
            dataGridViewBook.Columns.Add("Judul", "Judul Buku");
            dataGridViewBook.Columns.Add("Harga", "Harga");
            dataGridViewBook.Columns.Add("Stok", "Stok");
            dataGridViewBook.Columns.Add("IdTipeBuku", "Id Tipe Buku");
            dataGridViewBook.Columns.Add("NamaTipeBuku", "Nama Tipe Buku");
            dataGridViewBook.Columns.Add("IdPenerbit", "ID Penerbit");
            dataGridViewBook.Columns.Add("NamaPenerbit", "Nama Penerbit");


            //mengatur allignmnent
            dataGridViewBook.Columns["Harga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBook.Columns["Stok"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //agar harga jual ditampilkan dengan pemisah ribuan
            dataGridViewBook.Columns["Harga"].DefaultCellStyle.Format = "#,###";

            //agar lebar kolom mengikuti text/isi otomatis
            dataGridViewBook.Columns["IdBuku"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBook.Columns["Judul"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBook.Columns["Harga"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBook.Columns["Stok"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBook.Columns["IdTipeBuku"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBook.Columns["IdPenerbit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void TampilDataGrid()
        {
            if (listOfBook.Count > 0)
            {
                dataGridViewBook.Rows.Clear();

                foreach (Buku b in listOfBook)
                {
                    dataGridViewBook.Rows.Add(b.IdBuku, b.Nama, b.Harga, b.Stok, b.TipeBuku.IdTipeBuku, b.TipeBuku.JenisBuku,b.Penerbit.IdPenerbit,b.Penerbit.NamaPenerbit);
                }
            }
            else
            {
                dataGridViewBook.DataSource = null;
            }
        }
        public void FormListBuku_Load(object sender, EventArgs e)
        {
            comboBoxSearch.SelectedIndex = 0;
            FormatDataGrid();
            listOfBook = Buku.BacaData("", "");
            TampilDataGrid();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";
            if (comboBoxSearch.Text == "Id Buku")
            {
                kriteria = "b.IdBuku";
            }
            else if (comboBoxSearch.Text == "Judul Buku")
            {
                kriteria = "b.Judul";
            }
            else if (comboBoxSearch.Text == "Harga")
            {
                kriteria = "b.Harga";
            }
            else if (comboBoxSearch.Text == "Stok")
            {
                kriteria = "b.Stok";
            }
            else if (comboBoxSearch.Text == "Id Tipe Buku")
            {
                kriteria = "b.IdTipeBuku";
            }
            else if (comboBoxSearch.Text == "Nama Tipe Buku")
            {
                kriteria = "t.JenisBuku";
            }
            else if(comboBoxSearch.Text == "Id Penerbit")
            {
                kriteria = "b.IdPenerbit";
            }
            else if(comboBoxSearch.Text == "Nama Penerbit")
            {
                kriteria = "p.NamaPenerbit";
            }
            listOfBook = Buku.BacaData(kriteria, textBoxSearch.Text);
            TampilDataGrid();
        }
    }
}
