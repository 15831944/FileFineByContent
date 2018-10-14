 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommForAdolph.ThreadSafe
{
    /// <summary>
    /// 线程安全的dictionary
    /// </summary>
    public class SafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISyncRoot
    {
        public SafeDictionary()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
        }
        public readonly object syncRoot = new object();
        private bool changed_key = true;
        private bool changed_value = true;

        #region 缓存数据 避免枚举时候加锁 影响性能
        private TValue[] _allvalues = null;
        private TKey[] _allkeys = null;

        public TKey[] AllKeys
        {
            get
            {
                if (this.changed_key)
                {
                    lock (this.syncRoot)
                    {
                        this._allkeys = base.Keys.ToArray();
                        this.changed_key = false;
                    }
                }
                return this._allkeys;
            }

        }
        public TValue[] AllValues
        {
            get
            {
                
                if (this.changed_value)
                {
                    lock (this.syncRoot)
                    {
                        this._allvalues = base.Values.ToArray();
                        this.changed_value = false;
                    }
                }
                return this._allvalues;
            }
        }
        #endregion


        public new void Add(TKey key,TValue value)
        {
            lock (syncRoot)
            {
                base.Add(key, value);
                this.changed_key = true;
                this.changed_value = true;
            }
        }
        public new bool Remove(TKey key)
        {
            lock (syncRoot)
            {
                this.changed_key = true;
                this.changed_value = true;
                return base.Remove(key);
            }
        }
        public new bool TryGetValue(TKey key, out TValue value)
        {
            lock (syncRoot)
            {
                return base.TryGetValue(key, out value);
            }
        }
        public new TValue this[TKey key]
        {
            get
            {
                TValue v = default(TValue);
                this.TryGetValue(key, out v);
                return v;
            }
            set
            {
                lock (syncRoot)
                {
                    base[key] = value;
                    this.changed_key = true;
                    this.changed_value = true;
                }
            }
        }
        public new void Clear()
        {
            lock (syncRoot)
            {
                base.Clear();

                this.changed_key = true;
                this.changed_value = true;
            }
        }
        public new int Count
        {
            get
            {
                lock (syncRoot)
                {
                    return base.Count;
                }
            }
        }

        public new bool ContainsKey(TKey key)
        {
            lock (syncRoot)
            {
                return base.ContainsKey(key);
            }
        }
        public new bool ContainsValue(TValue value)
        {
            lock (syncRoot)
            {
                return base.ContainsValue(value);
            }
        }

        #region ISyncRoot 成员

        object ISyncRoot.syncRoot
        {
            get { return this.syncRoot; }
        }

        #endregion
    }
}
