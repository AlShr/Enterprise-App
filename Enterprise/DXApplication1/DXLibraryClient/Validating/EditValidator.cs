using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;

namespace DXLibraryClient.Validating
{

    public class BookEditValidatorEventArgs<T> : BaseContainerValidateEditorEventArgs
    {
        public BookEditValidatorEventArgs(T rowObject, object fValue)
            : base(fValue)
        {
            this.rowObject = rowObject;
        }

        public T RowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }

        private T rowObject;
    }
}
