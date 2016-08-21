using System;
using ProjectBase.Utils;

namespace Enterprise.CoreData.Domain
{
    public class Email
    {
        public Email() { }

        public Email(string emailAddress) 
        {
            Check.Require(!string.IsNullOrEmpty(emailAddress), "EmailAddress must be provided");
            this.emailAddress = emailAddress;
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set 
            {
                Check.Require(!string.IsNullOrEmpty(value), "EmailAddress must be provided");
                emailAddress = value;
            }
        }

        private string emailAddress = "";
    }
}
