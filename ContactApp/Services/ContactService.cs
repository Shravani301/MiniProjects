using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Services
{
    internal class ContactService
    {
        private List<Contact> contacts = new List<Contact>();

        public void AddContact(User user, Contact contact)
        {
            user.AddContact(contact);
        }

        public void ModifyContact(Contact contact, Action<Contact> modifyAction)
        {
            modifyAction(contact);
        }

        public void DeleteContact(Contact contact)
        {
            contact.IsActive = false; 
        }

        public IEnumerable<Contact> GetAllContacts(User user) => user.Contacts.Where(c => c.IsActive);
    }
}
