using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Models
{
    internal class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public List<ContactDetails> ContactDetails { get; set; }
        
        public void AddContactDetail(ContactDetails details)
        {
            ContactDetails.Add(details);
        }

    }
}
