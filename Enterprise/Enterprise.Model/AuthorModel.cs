using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;


namespace Enterprise.Model
{
    public class AuthorModel : BaseModel<long>
    {
        public AuthorModel(){ }

        public AuthorModel(string firstName, string lastName)
        {
            Check.Require(!string.IsNullOrEmpty(firstName), "FirstName must be provided");
            Check.Require(!string.IsNullOrEmpty(lastname), "LastName must be provided");
            this.firstName = firstName;
            this.lastname = lastName;
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                this.SetProperty(ref firstName, value);
            }
        }

        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
                this.SetProperty(ref lastname, value);
            }
        }

        public int PosAuthList
        {
            get { return posauthList; }
            set 
            {
                posauthList = value;
                this.SetProperty(ref posauthList, value);
            }
        }
        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + FirstName + "|" + LastName).GetHashCode();
        }

        private int posauthList;
        private string firstName = "";
        private string lastname = "";
    }
}
