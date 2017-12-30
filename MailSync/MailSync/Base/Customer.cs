using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSync.Base
{
    public class CustomerDetails
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? DOB { get; set; }

        public bool IsUnSubscribed {get; set;}
    }

    public class ListDetails
    {
        public int ListID { get; set; }

        public string ListName { get; set; }

        public string CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastChange { get; set; }

        public int CustomerCount { get; set; }

        public int OpenCount { get; set; }
    }

    public class CustomerListDetails :CustomerDetails
    {
        public int? ListID { get; set; }
        public DateTime DateAdded { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class CustomersMappedList : ListDetails
    {
        public List<CustomerListDetails> CustomerMappedListDetails { get; set; }

    }
}