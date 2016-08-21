
using System;
using Enterprise.Model;

namespace LibraryClient.Common
{
    public interface ICatalogManager<T>
    {
        event EventHandler SelectedRowChanged;
        event EventHandler EditedRowChanged;
        event EventHandler EditedRowSubViewChanged;
        event EventHandler EditedRowNewChildObjectAdded;
        event EventHandler EnableToConnected;
        event EventHandler<NewRowObjectArgs<T>> NewRowObjectSelect;
        event EventHandler<NewRowObjectArgs<T>> NewRowObjectEdit;
        event EventHandler<NewRowObjectArgs<T>> NewRowObjectSubViewEdit;
        event EventHandler<NewRowObjectArgs<T>> NewRowObjectChanged;
        event EventHandler<NewSearchedFilterArgs> SearchedFilterChanged;
        event EventHandler<NewSettedPaginationArgs> SettedPaginationChanged;
        event EventHandler<NewRowObjectArgs<T>> NewRowObjectAdded;
        event EventHandler<NewRowObjectCellEditArgs<T, long>> NewRowObjectCellEdit;
        void OnSubViewEditedRowChanged();
        void OnEditedRowChanged();
        void OnSelectedRowObjectNewChildAdded();
        void OnSelectedRowChanged();
        void OnEnabledToConnect();
        void SimulateNewSelectRow(T param);
        void SimulateEditRow(T param);
        void SimulateEditRowSubView(T param, int rowhandle);
        void SimulateNewRowChange(T param);
        void SimulateNewRowCellEdit(T param, int rowhandle, long cellvalue);
        void SimulateNewRowObjectAdd(T param);
        void SimulateNewCriteriaSearch(SearchModel search);
        void SimulateNewSetPagination(PageSelector selector);
    }
    public class NewRowObjectArgs<T> : EventArgs
    {
        public NewRowObjectArgs(T rowObject)
        {
            this.rowObject = rowObject;
        }
        public NewRowObjectArgs(T rowObject, int rowObjectHandle)
        {
            this.rowObject = rowObject;
            this.rowObjectHandle = rowObjectHandle;
        }
        public T RowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }

        public int RowObjectHandle
        {
            get { return rowObjectHandle; }
            set { rowObjectHandle = value; }
        }
        private int rowObjectHandle;
        private T rowObject;
    }

    public class NewRowObjectCellEditArgs<T, Tid> : NewRowObjectArgs<T>
    {
        public NewRowObjectCellEditArgs(T param, int rowObjectHandle, Tid cellEdit)
            : base(param, rowObjectHandle)
        {
            this.cellEdit = cellEdit;
        }
        public Tid CellEdit
        {
            get { return cellEdit; }
            set { cellEdit = value; }
        }
        private Tid cellEdit;
    }

    public class NewSearchedFilterArgs : EventArgs
    {
        public NewSearchedFilterArgs(SearchModel searchModel)
        {
            this.searchModel = searchModel;
        }
        private SearchModel searchModel;
        public SearchModel SearchModel { get { return searchModel; } }
    }

    public class NewSettedPaginationArgs : EventArgs
    {
        public NewSettedPaginationArgs(PageSelector pageSelector)
        {
            this.pageSelector = pageSelector;
        }
        public PageSelector PageSelector { get { return pageSelector; } }
        private PageSelector pageSelector;
    }
}
