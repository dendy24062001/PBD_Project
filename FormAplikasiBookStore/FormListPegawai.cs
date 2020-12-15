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
    public partial class FormListPegawai : Form
    {
        public List<Pegawai> listOfPegawai = new List<Pegawai>();

        public FormListPegawai()
        {
            InitializeComponent();
        }
        private void FormatDataGrid()
        {
            dataGridViewPegawai.Columns.Clear();

            dataGridViewPegawai.Columns.Add("IdPegawai", "Id Pegawai");
            dataGridViewPegawai.Columns.Add("Nama", "Nama Pegawai");
            dataGridViewPegawai.Columns.Add("Alamat", "Alamat");
            dataGridViewPegawai.Columns.Add("Gaji", "Gaji Pegawai");
            dataGridViewPegawai.Columns.Add("Username", "Username");
            dataGridViewPegawai.Columns.Add("IdJabatan", "Id Jabatan");
            dataGridViewPegawai.Columns.Add("NamaJabatan", "Nama Jababtan");

            dataGridViewPegawai.Columns["IdPegawai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewPegawai.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewPegawai.Columns["Alamat"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewPegawai.Columns["Gaji"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewPegawai.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewPegawai.Columns["IdJabatan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewPegawai.Columns["NamaJabatan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            dataGridViewPegawai.Columns["Gaji"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewPegawai.Columns["Gaji"].DefaultCellStyle.Format = "#,###";
        }

        private void TampilDataGrid()
        {
            dataGridViewPegawai.Rows.Clear();

            if (listOfPegawai.Count > 0)
            {
                foreach (Pegawai p in listOfPegawai)
                {

                    dataGridViewPegawai.Rows.Add(p.IdPegawai, p.Nama, p.Alamat, p.Gaji, p.Username, p.Jabatan.IdJabatan, p.Jabatan.NamaJabatan);
                }
            }
            else
            {
                dataGridViewPegawai.DataSource = null;
            }
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";
            if (comboBoxSearch.Text == "Id Pegawai")
            {
                kriteria = "p.IdPegawai";
            }
            else if (comboBoxSearch.Text == "Nama")
            {
                kriteria = "p.Nama";
            }
            else if (comboBoxSearch.Text == "Tanggal Lahir")
            {
                kriteria = "p.TanggalLahir";
            }
            else if (comboBoxSearch.Text == "Alamat")
            {
                kriteria = "p.Alamat";
            }
            else if (comboBoxSearch.Text == "Gaji")
            {
                kriteria = "p.Gaji";
            }
            else if (comboBoxSearch.Text == "Username")
            {
                kriteria = "p.Username";
            }
            else if (comboBoxSearch.Text == "Id Jabatan")
            {
                kriteria = "p.IdJabatan";
            }
            else if (comboBoxSearch.Text == "Nama Jabatan")
            {
                kriteria = "j.NamaJabatan";
            }

            listOfPegawai = Pegawai.BacaData(kriteria, textBoxSearch.Text);
            TampilDataGrid();
        }

        public void FormListPegawai_Load(object sender, EventArgs e)
        {

            comboBoxSearch.SelectedIndex = 0;

            FormatDataGrid();

            listOfPegawai = Pegawai.BacaData("", "");

            TampilDataGrid();
        }

        
    }
}
