 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommForAdolph.ThreadSafe
{
    /// <summary>
    /// 通用队列数据处理
    /// </summary>
    public class QueueProcessor<T>
    {
        Thread[] threads = null;
        public QueueProcessor()
        {
            this._queue = new SafeQueue<T>(1000);
            this.threads = new Thread[1];
            //this._queue.gr
        }
        public SafeQueue<T> Queue
        {
            get
            {
                return this._queue;
            }
        }
        public QueueProcessor(int capacity)
        {
            this._queue = new SafeQueue<T>(capacity);
        }
        public QueueProcessor(int capacity, int thread)
        {
            this._queue = new SafeQueue<T>(capacity);
            this.threads = new Thread[thread];
        }

        public event Action<T> OnProcess;
        public event Action<T, Exception> OnError;


        private SafeQueue<T> _queue = null;
        private bool _running = false;

        public int Count
        {
            get
            {
                return this._queue.Count;
            }
        }
        public bool Running
        {
            get
            {
                return this._running;
            }
        }
        public void AddItem(T item)
        {
            if (item == null) return;
            if (this._queue.Contains(item)) return;
            this._queue.Enqueue(item);
        }
        public void Clear()
        {
            this._queue.Clear();
        }
        public void StartProcess()
        {
            if (this._running) return;
            this._running = true;
            ThreadPool.QueueUserWorkItem(delegate
            {
                this.ProcessData();
            });
        }
        public void StopProcess()
        {
            if (this._running == false) return;
            this._running = false;
            this._queue.Clear();
        }
        private void ProcessData()
        {
            try
            {
                while (this._running)
                {
                    if (this._queue.Count == 0)
                    {
                        Thread.Sleep(10);
                    }
                    T item = this._queue.Dequeue();
                    if (item == null) continue;

                    //if (item is ISave)
                    //{
                    //    try
                    //    {
                    //        ISave saveObj = item as ISave;
                    //        saveObj.DoExcute();
                    //    }
                    //    catch (Exception exce)
                    //    {
                    //        Console.Write(exce.Message);
                    //    }
                      
                    //}
                    if (this.OnProcess != null)
                    {
                        try
                        {
                            this.OnProcess(item);
                        }
                        catch (ThreadInterruptedException)
                        {
                            return;
                        }
                        catch (ThreadAbortException)
                        {
                            return;
                        }
                        catch (Exception ex)
                        {
                            if (this.OnError != null)
                                this.OnError(item, ex);
                        }
                    }

                }
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
            catch (ThreadAbortException)
            {
                return;
            }

        }
    }
}
