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
    public partial class FormListCustomers : Form
    {
        List<Customers> listOfCustomers = new List<Customers>();

        public FormListCustomers()
        {
            InitializeComponent();
        }

        public void FormListCustomers_Load(object sender, EventArgs e)
        {
            listOfCustomers = Customers.BacaData("", "");
            comboBoxSearch.SelectedIndex = 0;

            if (listOfCustomers.Count > 0)
            {
                dataGridViewCustomers.DataSource = listOfCustomers;
            }
            else
            {
                dataGridViewCustomers.DataSource = null;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxSearch.Text == "Id Customer")
            {
                listOfCustomers = Customers.BacaData("IdCustomer", textBoxSearch.Text);
            }
            else if (comboBoxSearch.Text == "Nama Customer")
            {
                listOfCustomers = Customers.BacaData("Nama", textBoxSearch.Text);
            }
            else if (comboBoxSearch.Text == "Alamat")
            {
                listOfCustomers = Customers.BacaData("Alamat", textBoxSearch.Text);
            }
            else if (comboBoxSearch.Text == "No Telepon")
            {
                listOfCustomers = Customers.BacaData("Telepon", textBoxSearch.Text);
            }

            if (listOfCustomers.Count > 0)
            {
                dataGridViewCustomers.DataSource = listOfCustomers;
            }
            else
            {
                dataGridViewCustomers.DataSource = null;
            }
        }
    }
}
