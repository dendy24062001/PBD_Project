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
    public partial class FormListSupplier : Form
    {
        public List<Supplier> listOfSupplier = new List<Supplier>();

        public FormListSupplier()
        {
            InitializeComponent();
        }

        public void FormListSupplier_Load(object sender, EventArgs e)
        {
            listOfSupplier = Supplier.BacaData("", "");
            comboBoxSearch.SelectedIndex = 0;
            if (listOfSupplier.Count > 0)
            {
                dataGridView1.DataSource = listOfSupplier;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxSearch.Text == "Id Supplier")
            {
                listOfSupplier = Supplier.BacaData("IdSupplier", textBoxSearch.Text);
            }
            else if (comboBoxSearch.Text == "Nama Supplier")
            {
                listOfSupplier = Supplier.BacaData("Nama", textBoxSearch.Text);
            }
            else if (comboBoxSearch.Text == "Alamat")
            {
                listOfSupplier = Supplier.BacaData("Alamat", textBoxSearch.Text);
            }

            if (listOfSupplier.Count > 0)
            {
                dataGridView1.DataSource = listOfSupplier;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }
    }
}
