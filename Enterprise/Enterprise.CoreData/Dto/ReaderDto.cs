using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.CoreData.Dto;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class ReaderDto:BaseDto<long>
    {      
        public EmailDto EmailIdentity
        {
            get { return emaiIdentity; }
            set { emaiIdentity = value; }
        }

        private EmailDto emaiIdentity;
    }
}
