using BLL.DTO;
using DAL.Data;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ContactServices
    {
        DataBaseContext context = new DataBaseContext();

        /// <summary>
        /// Search and List of contacts
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public List<DTO.ContactListDTO> GetContactsList([Optional] string searchKey)
        {

            var ContactQuery = context.Contacts.AsQueryable();
            if (!string.IsNullOrEmpty(searchKey))
            {
                ContactQuery = ContactQuery.Where(i =>
                i.Name.Contains(searchKey)
                ||
                i.LastName.Contains(searchKey)
                ||
                i.PhoneNumber.Contains(searchKey)
                ||
                i.Company.Contains(searchKey)
                );
            }
            var contacts = ContactQuery.Select(p => new ContactListDTO
            {
                Id = p.Id,
                FullName = $"{p.Name} {p.LastName}",
                PhoneNumber = p.PhoneNumber
            }).ToList();

            return contacts;
        }
        /// <summary>
        /// Delete contacts
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>

        public DTO.ResultDTO DeleteContact(int Id)
        {
            var contact = context.Contacts.Find(Id);
            if (contact != null)
            {
                context.Remove(contact);
                context.SaveChanges();
                return new DTO.ResultDTO()
                {
                    IsSuccess = true,
                    Message = "Your Cantact deleted successfully!!"
                };
            }
            else
            {
                return new DTO.ResultDTO()
                {
                    IsSuccess = false,
                    Message = "Your Cantact does not exist!!"
                };
            }
        }

        /// <summary>
        /// Contacts Details
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>

        public DTO.ResultDTO<ContactDetailsDTO> ContactDetails(int Id)
        {

            var contact = context.Contacts.Find(Id);
            if (contact == null)
            {
                return new DTO.ResultDTO<ContactDetailsDTO>
                {
                    IsSuccess = false,
                    Message = "Your Cantact does not exist!!",
                    Data = null
                };
            }
            ContactDetailsDTO contactDetails = new ContactDetailsDTO();
            contactDetails.Id = contact.Id;
            contactDetails.PhoneNumber = contact.PhoneNumber;
            contactDetails.LastName = contact.LastName;
            contactDetails.Name = contact.Name;
            contactDetails.TimeCreated = contact.TimeCreated;
            contactDetails.Description = contact.Description;
            contactDetails.Company = contact.Company;

            return new DTO.ResultDTO<ContactDetailsDTO>
            {
                IsSuccess = true,
                Data = contactDetails
            };
        }

        /// <summary>
        /// Add Contact
        /// </summary>
        /// <param name="AddContactDTO"></param>
        /// <returns></returns>
        public ResultDTO AddContact(AddContactDTO addContactDTO)
        {
            if (string.IsNullOrEmpty(addContactDTO.PhoneNumber))
            {
                return (new ResultDTO
                {
                    IsSuccess = true,
                    Message = "Phone Number is a mandatory field!!!",
                });
            }

            context.Contacts.Add(new Contact
            {

                Company = addContactDTO.Company,
                Description = addContactDTO.Description,
                LastName = addContactDTO.LastName,
                Name = addContactDTO.Name,
                PhoneNumber = addContactDTO.PhoneNumber,
                TimeCreated = DateTime.Now
            });
            context.SaveChanges();
            return (new ResultDTO
            {
                IsSuccess = true,
                Message = $" {addContactDTO.Name} {addContactDTO.LastName} added to Database",
            });
        }
        /// <summary>
        /// Update Contact
        /// </summary>
        /// <param name="UpdateContactDTO"></param>
        /// <returns></returns>
        public ResultDTO UpdateContact(UpdateContactDTO updateContactDTO)
        {
            var contact = context.Contacts.Find(updateContactDTO.Id);
            if (contact == null)
            {
                return new ResultDTO
                {
                    IsSuccess = false,
                    Message = "This contact does not exist!!!"
                };
            }
            else
            {
                contact.Company= updateContactDTO.Company;
                contact.Description= updateContactDTO.Description;
                contact.LastName= updateContactDTO.LastName;
                contact.Name= updateContactDTO.Name;
                contact.PhoneNumber= updateContactDTO.PhoneNumber;
                context.SaveChanges();
                return new ResultDTO
                {
                    IsSuccess = true,
                    Message = "Update did successfully!!!"
                };
            }
        }
    }
}
