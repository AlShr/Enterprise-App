using System;
using System.Collections.Generic;
using ProjectBase.Data;


namespace Enterprise.Model
{
    public class ReaderModel:BaseModel<long>
    {
        public EmailModel EmailIdentity
        {
            get { return emailIdentity; }
            set 
            {
                emailIdentity = value;
                this.SetProperty(ref emailIdentity, value);
            }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + EmailIdentity.EmailAddress).GetHashCode();
        }

        private EmailModel emailIdentity;       
    }
    public class EmailModel
    {
        public string EmailAddress
        {
            get { return emailAddress; }
            set 
            {
                emailAddress = value;
            }
        }

        private string emailAddress = "";
    }

}
