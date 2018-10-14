 
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

namespace CommForAdolph.ThreadSafe
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class SafeBindingList<T> : BindingList<T>, ISyncRoot<T>, ISyncRoot
    {
        public SafeBindingList()
            : base()
        {
        }
        public SafeBindingList(IList<T> list)
            : base(list)
        {
        }

        public object syncRoot = new object();
        //public override void CancelNew(int itemIndex)
        //{
        //    lock (syncRoot)
        //    {
        //        base.CancelNew(itemIndex);
                
        //    }
        //}
        //public override void EndNew(int itemIndex)
        //{
        //    lock (syncRoot)
        //    {
        //        base.EndNew(itemIndex);
        //    }
        //}
        public void Add(ISynchronizeInvoke invoker, T item)
        {
            if (invoker.InvokeRequired)
            {
                Action<T> action = new Action<T>(this.Add);
                invoker.Invoke(action, new object[] { item });
            }
            else
            {
                this.Add(item);
            }
            
        }
        public void RemoveAt(ISynchronizeInvoke invoker, int pos)
        {
            if (invoker.InvokeRequired)
            {
                Action<int> action = new Action<int>(this.RemoveAt);
                invoker.Invoke(action, new object[] { pos });
            }
            else
            {
                this.RemoveAt(pos);
            }
            
        }
        public void Insert(ISynchronizeInvoke invoker,int pos, T item)
        {
            if (invoker.InvokeRequired)
            {
                Action<int, T> action = new Action<int, T>(this.Insert);
                invoker.Invoke(action, new object[] { pos,item });
            }
            else
            {
                this.Insert(pos,item);
            }

        }
 
        protected override object AddNewCore()
        {
            lock (syncRoot)
            {
                return base.AddNewCore();
            }
        }
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            lock (this.syncRoot)
            {
                base.ApplySortCore(prop, direction);
            }
        }

        public void Clear(ISynchronizeInvoke invoker)
        {

            if (invoker.InvokeRequired)
            {
                Action action = new Action(this.Clear);
                invoker.Invoke(action,null);
            }
            else
            {
                this.Clear();
            }

        }
        //public new void Clear()
        //{
        //    lock (this.syncRoot)
        //    {
        //        base.Clear();
        //    }
        //}
        public new bool Contains(T item)
        {
            lock (this.syncRoot)
            {
                return base.Contains(item);
                
            }
        }
        public new int IndexOf(T item)
        {
            lock (this.syncRoot)
            {
                return base.IndexOf(item);
            }
        }
        //public new void Insert(int index,T item)
        //{
        //    lock (this.syncRoot)
        //    {
        //        base.Insert(index, item);
        //    }
        //}

        protected override void InsertItem(int index, T item)
        {
            lock (this.syncRoot)
            {
                base.InsertItem(index, item);
            }
        }
        protected override void ClearItems()
        {
            lock (syncRoot)
            {
                base.ClearItems();
            }
        }
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            lock (this.syncRoot)
            {
                return base.FindCore(prop, key);
            }
        }
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            lock (syncRoot)
            {
                base.OnAddingNew(e);
            }
        }
        //public new bool Remove(T item)
        //{
        //    lock (this.syncRoot)
        //    {
        //        return base.Remove(item);
        //    }
        //}
        public bool Remove(ISynchronizeInvoke invoker, T item)
        {
            if (invoker.InvokeRequired)
            {
                Func<T, bool> action = new Func<T, bool>(this.Remove);
                //lock (this.syncRoot)
                //{
                    return (bool)invoker.Invoke(action, new object[] { item });
                //}
            }
            else
            {
                return this.Remove(item);
            }
        }
        //protected override void OnListChanged(ListChangedEventArgs e)
        //{
        //    lock (this.syncRoot)
        //    {
        //        base.OnListChanged(e);
        //    }
        //}
        protected override void RemoveSortCore()
        {
            lock (syncRoot)
            {
                base.RemoveSortCore();
            }
        }
        protected override void SetItem(int index, T item)
        {
            lock (syncRoot)
            {
                base.SetItem(index, item);
            }
        }
        protected override void RemoveItem(int index)
        {
            lock (syncRoot)
            {
                base.RemoveItem(index);
            }
        }

        #region ISyncRoot 成员

        object ISyncRoot<T>.syncRoot
        {
            get { return this.syncRoot; }
        }

        #endregion

        #region ISyncRoot 成员

        object ISyncRoot.syncRoot
        {
            get { return this.syncRoot; }
        }

        #endregion
    }

    public static class BindingListHelper
    {
        public static void RemoveRange<T>(this BindingList<T> list, int startindex, int count)
        {
            if (startindex < 0 || count <= 0) return;
            int endindex = startindex + count;
            if (endindex >= list.Count) endindex = list.Count;
            for (int i = endindex - 1; i >= startindex; i--)
            {
                list.RemoveAt(i);
            }
        }


        //public static void AddRange<T>(this BindingList<T> list, IEnumerable<T> collection)
        //{
        //    if (collection == null) return;
        //    collection.Foreach(a => list.Add(a));
        //}
        //public static void InsertRange<T>(this BindingList<T> list, int startindex, IEnumerable<T> collection)
        //{
        //    if (startindex < 0 || collection == null) return;
        //    if (startindex > list.Count) startindex = list.Count;
        //    collection.Foreach(a => list.Insert(startindex, a));
        //}


    }
}
