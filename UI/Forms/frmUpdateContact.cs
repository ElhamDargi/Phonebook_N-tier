using BLL.Services;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UI.Forms
{
    public partial class frmUpdateContact : Form
    {
        private readonly ContactServices contactServices;
        private readonly int Id;
        public frmUpdateContact(int id)
        {
            InitializeComponent();
            contactServices = new ContactServices();
            this.Id = id;
        }
        private void frmUpdateContact_Load(object sender, EventArgs e)
        {
            var contact = contactServices.ContactDetails(Id);
            if (!contact.IsSuccess)
            {
                MessageBox.Show(contact.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtCompany.Text = contact.Data.Company;
                txtDescription.Text = contact.Data.Description;
                txtLastName.Text = contact.Data.LastName;
                txtName.Text = contact.Data.Name;
                txtNumber.Text = contact.Data.PhoneNumber;
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var result = contactServices.UpdateContact(new UpdateContactDTO
            {
                Company = txtCompany.Text,
                Description = txtDescription.Text,
                LastName = txtLastName.Text,
                Name = txtName.Text,
                PhoneNumber = txtNumber.Text,
                Id = Id
            });
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(result.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }


    }
}
