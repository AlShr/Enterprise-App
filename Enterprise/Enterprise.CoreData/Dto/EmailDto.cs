using System;
using ProjectBase.Utils;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class EmailDto
    {
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        private string emailAddress = "";
    }
}
