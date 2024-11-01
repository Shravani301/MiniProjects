using ContactApp.Exceptions;
using ContactApp.Models;
using ContactApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactApp.Controller
{
    internal class ContactController
    {
        private ContactService contactService = new ContactService();

        public void AddContact(int contactId, string firstName, string lastName, bool isActive, User user)
        {
            var contact = new Contact
            {
                ContactId = contactId,
                FirstName = firstName,
                LastName = lastName,
                IsActive = isActive
            };
            contactService.AddContact(user, contact);
        }

        public void ModifyContact(int contactId, User user, Action<Contact> modifyAction)
        {
            var contact = FindContactById(contactId, user);
            modifyAction(contact);
            contactService.ModifyContact(contact, modifyAction);
        }

        public void DeleteContact(int contactId, User user)
        {
            var contact = FindContactById(contactId, user);
            contactService.DeleteContact(contact);
        }

        public IEnumerable<Contact> GetAllContacts(User user)
        {
            return contactService.GetAllContacts(user);
        }

        public Contact GetContactById(int contactId, User user)
        {
            return FindContactById(contactId, user);
        }

        private Contact FindContactById(int contactId, User user)
        {
            var contact = user.Contacts.FirstOrDefault(c => c.ContactId == contactId && c.IsActive);
            if (contact == null) throw new InvalidUserActionException("Contact not found.");
            return contact;
        }
    }
}
