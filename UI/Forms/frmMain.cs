using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Forms
{
    public partial class frmMain : Form
    {
        private readonly ContactServices contactServices;
        public frmMain()
        {
            InitializeComponent();
            contactServices = new ContactServices();
        }

        private void SettingGridview(List<ContactListDTO> contactList)
        {
            dataGridView1.DataSource = contactList;
            dataGridView1.Columns[0].HeaderText = "شناسه";
            dataGridView1.Columns[1].HeaderText = "نام";
            dataGridView1.Columns[2].HeaderText = "شماره تماس";
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Width = 200;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var contactList = contactServices.GetContactsList();
            SettingGridview(contactList);
            this.Cursor = Cursors.Default;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var contactList = contactServices.GetContactsList(searchKey.Text);
            SettingGridview(contactList);
            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var Id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var result = contactServices.DeleteContact(Id);
            if (result.IsSuccess)
            {
                MessageBox.Show(result.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMain_Load(null, null);
            }
            else
            {
                MessageBox.Show(result.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void ShowDetails()
        {
            var Id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            frmContactDetails frmContactDetails = new frmContactDetails(Id);
            frmContactDetails.ShowDialog();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            frmAddNewContact frmContact = new frmAddNewContact();
            frmContact.ShowDialog();
            frmMain_Load(null, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var Id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            frmUpdateContact frmUpdateContact = new frmUpdateContact(Id);
            frmUpdateContact.ShowDialog();
            frmMain_Load(null, null);
        }
    }
}
