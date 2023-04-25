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
    public partial class frmAddNewContact : Form
    {
        private readonly ContactServices contactServices;
        public frmAddNewContact()
        {
            InitializeComponent();
            contactServices = new ContactServices();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddContactDTO addContact = new AddContactDTO();
            addContact.LastName = txtLastName.Text;
            addContact.PhoneNumber = txtNumber.Text;
            addContact.Name = txtName.Text;
            addContact.Company = txtCompany.Text;
            addContact.Description = txtDescription.Text;
            var result = contactServices.AddContact(addContact);
            if (result.IsSuccess)
            {
                MessageBox.Show(result.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
