using Enterprise.CoreData.ConverterFactory;
using Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Enterprise.Services.Validators
{
    public class EditValidator
    {
        public EditValidator()
        {
            validateStrategy = new Dictionary<string, IValidateStrategy>();
            validateStrategy.Add("Description", new BookValidatingStrategy());

        }

        public void CheckOut(string fieldType, BaseModel<long> model, object param)
        {
            validateStrategy[fieldType].Validate(model, param);

        }

        IDictionary<string, IValidateStrategy> validateStrategy;
    }

    
    public interface IValidateStrategy
    {
        bool Validate(BaseModel<long> model, object param);
    }

    public class BookValidatingStrategy : IValidateStrategy
    {
        public bool Validate(BaseModel<long> model, object param)
        {
            string cellvalue = param as string;
            return string.IsNullOrEmpty(cellvalue);
        }
    }
}
