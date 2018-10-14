
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommForAdolph.ThreadSafe
{
    /// <summary>
    /// 线程安全的List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SafeList<T> : List<T>, ISyncRoot<T>, ISyncRoot
    {
        public SafeList(IEnumerable<T> list)
        {
            this.AddRange(list);
        }
        public SafeList()
        {
        }
        public  readonly object sync = new object();

        public new void Add(T item)
        {
            lock (sync)
            {
                base.Add(item);
            }
        }
        

        public new void AddRange(IEnumerable<T> items)
        {
            lock (sync)
            {
                base.AddRange(items);
                
            }
        }
        public new void Clear()
        {
            lock (sync)
            {
                base.Clear();
            }
        }

        public new void Remove(T item)
        {
            lock (sync)
            {
                base.Remove(item);
            }
        }
        public new void RemoveAll(Predicate<T> pre)
        {
            lock (sync)
            {
                base.RemoveAll(pre);
            }
        }
        public new void RemoveAt(int index)
        {
            lock (sync)
            {
                base.RemoveAt(index);
            }
        }
        public new void RemoveRange(int index,int count)
        {
            lock (sync)
            {
                base.RemoveRange(index, count);
            }
        }
        public new void Reverse()
        {
            lock (sync)
            {
                base.Reverse();
            }
        }
        public new void Reverse(int index, int count)
        {
            lock (sync)
            {
                base.Reverse(index, count);
                
            }
        }
        public new void Sort()
        {
            lock (sync)
            {
                base.Sort();
            }
        }
        public new void Sort(Comparison<T> Comparison)
        {
            lock (sync)
            {
                base.Sort(Comparison);
            }
        }
        public new void Sort(IComparer<T> com)
        {
            lock (sync)
            {
                base.Sort(com);
            }
        }
        public new void Sort(int index,int count,IComparer<T> com)
        {
            lock (sync)
            {
                base.Sort(index,count,com);
                
            }
        }
        public new T[] ToArray()
        {
            lock (sync)
            {
                return base.ToArray();
            }
        }
        public new void TrimExcess()
        {
            lock (sync)
            {
                base.TrimExcess();
            }
        }
        public new bool TrueForAll(Predicate<T> p)
        {
            lock (sync)
            {
                return base.TrueForAll(p);
            }
        }

        #region ISyncRoot 成员

        object ISyncRoot<T>.syncRoot
        {
            get { return this.sync; }
        }

        #endregion

        #region ISyncRoot 成员

        object ISyncRoot.syncRoot
        {
            get { return this.sync; }
        }

        #endregion
    }
}
