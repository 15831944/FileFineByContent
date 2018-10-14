 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommForAdolph.ThreadSafe
{
    /// <summary>
    /// 线程安全的Queue，继承自Queue
    /// </summary>
    public class SafeQueue<T> : Queue<T>
    {
        public readonly object SyncRoot = new object();
        public SafeQueue(int cap):base(cap)
        {

        }
        #region Queue
        
        public new bool Contains(T item)
        {
            return base.Contains(item);
        }
        public new void CopyTo(T[] array, int arrayIndex)
        {
            lock (this.SyncRoot)
            {
                base.CopyTo(array, arrayIndex);
            }
        }
        public new void Clear()
        {
            lock (this.SyncRoot)
            {
                base.Clear();
            }
        }
        public new void TrimExcess()
        {
            lock (this.SyncRoot)
            {
                base.TrimExcess();
            }
        }
        public new T[] ToArray()
        {
            lock (this.SyncRoot)
            {
                return base.ToArray();
            }
        }
        public new void Enqueue(T item)
        {
            lock (this.SyncRoot)
            {
                base.Enqueue(item);
            }
        }
        public new T Dequeue()
        {
            lock (this.SyncRoot)
            {
                if (base.Count > 0)
                    return base.Dequeue();
            }
            return default(T);
        }
        public new T Peek()
        {
            lock (this.SyncRoot)
            {
                return base.Peek();

            }
        }
    }
        #endregion
}
