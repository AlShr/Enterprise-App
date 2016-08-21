using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Model;
using System.Threading;
using LibraryClient.Common;


namespace LibraryClient.Managers
{
    public class CatalogManager<T> : ICatalogManager<T>
    {
        /// <summary>
        /// Raise event SettedPaginationChanged
        /// </summary>
        /// <param name="selector"></param>
        public void SimulateNewSetPagination(PageSelector selector)
        {
            NewSettedPaginationArgs args = new NewSettedPaginationArgs(selector);
            OnSettedPageSelectorChanged(args);
        }

        /// <summary>
        /// Raise event SearchedFilterChanged
        /// </summary>
        /// <param name="search"></param>
        public void SimulateNewCriteriaSearch(SearchModel search)
        {
            NewSearchedFilterArgs args = new NewSearchedFilterArgs(search);
            OnSearchedBookChanged(args);
        }

        /// <summary>
        /// Raise event NewRowObjectSelect 
        /// </summary>
        /// <param name="param"></param>
        public void SimulateNewSelectRow(T param)
        {
            NewRowObjectArgs<T> arg = new NewRowObjectArgs<T>(param);
            OnNewSelectRowObject(arg);
        }

        /// <summary>
        /// Raise event NewRowObjectEdit
        /// </summary>
        /// <param name="param"></param>
        public void SimulateEditRow(T param)
        {
            NewRowObjectArgs<T> arg = new NewRowObjectArgs<T>(param);
            OnNewEditRowObject(arg);
        }

        /// <summary>
        /// Raise NewRowObjectSubViewEdit
        /// </summary>
        /// <param name="param"></param>
        /// <param name="rowhandle"></param>
        public void SimulateEditRowSubView(T param, int rowhandle)
        {
            NewRowObjectArgs<T> arg = new NewRowObjectArgs<T>(param, rowhandle);
            OnNewEditRowObjectSubView(arg);
        }

        /// <summary>
        /// Raise NewRowObjectChanged
        /// </summary>
        /// <param name="param"></param>
        public void SimulateNewRowChange(T param)
        {
            NewRowObjectArgs<T> arg = new NewRowObjectArgs<T>(param);
            OnNewRowObjectChange(arg);
        }

        /// <summary>
        /// NewRowObjectCellEdit
        /// </summary>
        /// <param name="param"></param>
        /// <param name="rowhandle"></param>
        /// <param name="cellvalue"></param>
        public void SimulateNewRowCellEdit(T param, int rowhandle, long cellvalue)
        {
            NewRowObjectCellEditArgs<T, long> arg = new NewRowObjectCellEditArgs<T, long>
                          (param, rowhandle, cellvalue);
            OnNewEditCellRowObject(arg);
        }

        /// <summary>
        /// Raise NewRowObjectAdded
        /// </summary>
        /// <param name="e"></param>
        public void SimulateNewRowObjectAdd(T param)
        {
            NewRowObjectArgs<T> arg = new NewRowObjectArgs<T>(param);
            OnNewRowObjectAdd(arg);
        }

        protected virtual void OnSettedPageSelectorChanged(NewSettedPaginationArgs e)
        {
            EventHandler<NewSettedPaginationArgs> handler = Volatile.Read(ref SettedPaginationChanged);
            if (SettedPaginationChanged != null)
            {
                SettedPaginationChanged(this, e);
            }
        }

        protected virtual void OnSearchedBookChanged(NewSearchedFilterArgs e)
        {
            EventHandler<NewSearchedFilterArgs> handler = Volatile.Read(ref SearchedFilterChanged);
            if (SearchedFilterChanged != null)
            {
                SearchedFilterChanged(this, e);
            }
        }

        protected virtual void OnNewSelectRowObject(NewRowObjectArgs<T> e)
        {
            EventHandler<NewRowObjectArgs<T>> handler = Volatile.Read(ref NewRowObjectSelect);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewEditRowObject(NewRowObjectArgs<T> e)
        {
            EventHandler<NewRowObjectArgs<T>> handler = Volatile.Read(ref NewRowObjectEdit);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewEditRowObjectSubView(NewRowObjectArgs<T> e)
        {
            EventHandler<NewRowObjectArgs<T>> handler = Volatile.Read(ref NewRowObjectSubViewEdit);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewRowObjectChange(NewRowObjectArgs<T> e)
        {
            EventHandler<NewRowObjectArgs<T>> handler = Volatile.Read(ref NewRowObjectChanged);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewEditCellRowObject(NewRowObjectCellEditArgs<T, long> e)
        {
            EventHandler<NewRowObjectCellEditArgs<T, long>> handler = Volatile.Read(ref NewRowObjectCellEdit);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewRowObjectAdd(NewRowObjectArgs<T> e)
        {
            EventHandler<NewRowObjectArgs<T>> handler = Volatile.Read(ref NewRowObjectAdded);
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void RaiseSubViewEditedRowChanged()
        {
            EventHandler handler = Volatile.Read(ref EditedRowSubViewChanged);
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///Raise event  EditedRowSubViewChanged
        /// </summary>
        public virtual void OnSubViewEditedRowChanged()
        {
            RaiseSubViewEditedRowChanged();
        }

        public void RaiseEditedRowChanged()
        {
            EventHandler handler = Volatile.Read(ref EditedRowChanged);
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raise event EditedRowChanged
        /// </summary>
        public virtual void OnEditedRowChanged()
        {
            RaiseEditedRowChanged();
        }

        public void RaiseSelectedRowChanged()
        {
            EventHandler handler = Volatile.Read(ref SelectedRowChanged);
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///Raise event SelectedRowChanged 
        /// </summary>
        public virtual void OnSelectedRowChanged()
        {
            RaiseSelectedRowChanged();
        }

        public void RaiseSelectedRowObjectNewChildAdded()
        {
            EventHandler handler = Volatile.Read(ref EditedRowNewChildObjectAdded);
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raise event EditedRowNewChildObjectAdded
        /// </summary>
        public virtual void OnSelectedRowObjectNewChildAdded()
        {
            RaiseSelectedRowObjectNewChildAdded();
        }

        public void RaiseEnableToConnected()
        {
            EventHandler handler = Volatile.Read(ref EnableToConnected);
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public virtual void OnEnabledToConnect()
        {
            RaiseEnableToConnected();
        }

        public event EventHandler<NewSettedPaginationArgs> SettedPaginationChanged;
        public event EventHandler<NewSearchedFilterArgs> SearchedFilterChanged;
        public event EventHandler<NewRowObjectArgs<T>> NewRowObjectSelect;
        public event EventHandler<NewRowObjectArgs<T>> NewRowObjectEdit;
        public event EventHandler<NewRowObjectArgs<T>> NewRowObjectSubViewEdit;
        public event EventHandler<NewRowObjectArgs<T>> NewRowObjectChanged;
        public event EventHandler<NewRowObjectArgs<T>> NewRowObjectAdded;
        public event EventHandler<NewRowObjectCellEditArgs<T, long>> NewRowObjectCellEdit;       
        public event EventHandler SelectedRowChanged;
        public event EventHandler EditedRowChanged;
        public event EventHandler EditedRowSubViewChanged;
        public event EventHandler EditedRowNewChildObjectAdded;
        public event EventHandler EnableToConnected;
    }
}
