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
using ClassLibraryBookStore;

namespace FormAplikasiBookStore
{
    public partial class FormListBookType : Form
    {

        public List<TipeBuku> listOfTipeBuku = new List<TipeBuku>();
        public FormListBookType()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void FormListBookType_Load(object sender, EventArgs e)
        {
            listOfTipeBuku = TipeBuku.BacaData("", "");
            comboBoxSearch.SelectedIndex = 0;
            if (listOfTipeBuku.Count > 0)
            {
                dataGridView1.DataSource = listOfTipeBuku;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxSearch.Text == "Id Tipe Buku")
            {
                listOfTipeBuku = TipeBuku.BacaData("idTipeBuku", textBoxSearch.Text);
            }
            else if (comboBoxSearch.Text == "Jenis Buku")
            {
                listOfTipeBuku = TipeBuku.BacaData("jenisBuku", textBoxSearch.Text);
            }

            if (listOfTipeBuku.Count > 0)
            {
                dataGridView1.DataSource = listOfTipeBuku;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
