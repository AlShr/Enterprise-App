using System;
using System.Collections.Generic;
using ProjectBase.Utils;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class AuthorDto:BaseDto<long>
    {       
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private string firstName;
        private string lastName;

    }
}
