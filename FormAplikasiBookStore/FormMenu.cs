using ClassLibraryBookStore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace FormAplikasiBookStore
{
    public partial class FormMenu : Form
    {
        string resourcePath = Application.StartupPath + "\\Resources\\";
        WindowsMediaPlayer loopSound = new WindowsMediaPlayer();
        WindowsMediaPlayer normalSound = new WindowsMediaPlayer();
        public Pegawai pegawaiLogin;
        public FormMenu()
        {
            InitializeComponent();
            Customize();
        }
        private void Customize()
        {
            panelBook.Visible = false;
            panelBookType.Visible = false;
            panelCustomer.Visible = false;
            panelEmployee.Visible = false;
            panelPurchase.Visible = false;
            panelSales.Visible = false;
            panelSupplier.Visible = false;


        }
        private void HideSubMenu()
        {
            if (panelBook.Visible == true)
            {
                panelBook.Visible = false;
            }
            else if(panelBookType.Visible == true)
            {
                panelBookType.Visible = false;
            }
            else if(panelSupplier.Visible == true)
            {
                panelSupplier.Visible = false;
            }
            else if (panelEmployee.Visible == true)
            {
                panelEmployee.Visible = false;
            }
            else if (panelCustomer.Visible == true)
            {
                panelCustomer.Visible = false;
            }
            else if (panelPurchase.Visible == true)
            {
                panelPurchase.Visible = false;
            }
            else
            {
                panelSales.Visible = false;
            }



        }
        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null) 
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void buttonAddType_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAddTypeOfBooks());


        }

        private void buttonAddBook_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAddOfBook());
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            loopSound.URL = resourcePath + "The Wind and The Star Traveler Genshin Impact.mp3";
            loopSound.settings.setMode("loop", true);

            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;
            this.Enabled = false;
            

            //menampilkan form login
            FormLogin formLogin = new FormLogin();
            formLogin.Owner = this;
            formLogin.Show();

            
            
        }



        private void buttonAddSupplier_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAddSupplier());

           
        }

        private void buttonAddCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAddCustomers());

        }

     

        private void buttonChangeEmployee_Click(object sender, EventArgs e)
        {
            openChildForm(new FormChangeEmployee());

        }


        private void buttonAddPurchase_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAddPurchaseBill());
        }

        private void buttonPrintPurchase_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonBook_Click(object sender, EventArgs e)
        {
            openChildForm(new FormListBuku());
            ShowSubMenu(panelBook);
        }

        private void buttonBookType_Click(object sender, EventArgs e)
        {
            openChildForm(new FormListBookType());
            ShowSubMenu(panelBookType);
        }

        private void buttonEmployee_Click(object sender, EventArgs e)
        {
            openChildForm(new FormListPegawai());
            ShowSubMenu(panelEmployee);
        }

        private void buttonPurchase_Click(object sender, EventArgs e)
        {
            openChildForm(new FormListNotaBeli());
            ShowSubMenu(panelPurchase);
        }

        private void buttonSupplier_Click(object sender, EventArgs e)
        {
            openChildForm(new FormListSupplier());
            ShowSubMenu(panelSupplier);
        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new FormListCustomers());
            ShowSubMenu(panelCustomer);
        }

        private void buttonAddEmployee_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAddOfEmployee());
        }

        private void buttonAddSales_Click(object sender, EventArgs e)
        {
            openChildForm(new FormAddSellBill());
        }

        private void buttonChangeBook_Click(object sender, EventArgs e)
        {
            openChildForm(new FormChangeBook());
        }

        private void buttonChangeSupplier_Click_1(object sender, EventArgs e)
        {
            openChildForm(new FormChangeSupplier());
        }

        private void buttonSales_Click(object sender, EventArgs e)
        {
            openChildForm(new FormListNotaJual());
            ShowSubMenu(panelSales);
        }

        private void buttonDeleteSupplier_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDeleteOfSupplier());
        }

        private void buttonDeleteEmployee_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDeleteOfEmployee());
        }

        private void buttonChangeCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new FormChangeCustomers());
        }

        private void buttonDeleteBook_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDeleteOfBook());
        }
        
        private void buttonChangeType_Click(object sender, EventArgs e)
        {
            openChildForm(new FormChangeTypeOfBook());
        }

        private void buttonDeleteCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDeleteOfCustomers());
        }

        public void PengaturanHakAkses(Jabatan j)
        {
            if (j.IdJabatan == "1") //CEO
            {

            }
            else if (j.IdJabatan == "2") //PENATA
            {
                 
            }
            else if (j.IdJabatan == "3") //KASIR
            {
                buttonBook.Visible = false;
                buttonAddBook.Visible = false;
                buttonChangeBook.Visible = false;
                buttonDeleteBook.Visible = false;
            }
        }

        public void radioButtonEnable_CheckedChanged(object sender, EventArgs e)
        {
            loopSound.URL = resourcePath + "The Wind and The Star Traveler Genshin Impact.mp3";
            loopSound.settings.setMode("loop", true);
        }

        public void radioButtonDisable_CheckedChanged(object sender, EventArgs e)
        {
            loopSound.URL = "";
            loopSound.settings.setMode("loop", true);
        }
    }
}
