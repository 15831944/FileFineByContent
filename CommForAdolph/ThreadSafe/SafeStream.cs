 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CommForAdolph.ThreadSafe
{
    public class SafeStream
    {
        public object SyncRoot = new object();
        public SafeStream(Stream s)
        {
            if (s == null) throw new ArgumentNullException("数据流");
            this._stream = s;
        }
        private Stream _stream = null;

        public Stream Stream
        {
            get
            {
                return this._stream;
            }
        }
        public long Length
        {
            get
            {
                return this._stream.Length;
            }
        }

        public long Position
        {
            get
            {
                return this._stream.Position;
            }
            set
            {
                lock (this.SyncRoot)
                {
                    this._stream.Position = value;
                }
            }
        }

        public long Seek(long offset,SeekOrigin origin)
        {
            lock(this.SyncRoot)
            {
                return this._stream.Seek(offset, origin);
            }
        }
        public void Write(byte[] buffer,int offset,int count)
        {
            lock (this.SyncRoot)
            {
                this._stream.Write(buffer, offset, count);
            }
        }
        public int Read(byte[] buffer,int offset,int count)
        {
            lock (this.SyncRoot)
            {
                return this.Stream.Read(buffer, offset, count);
            }
        }
        public void Close()
        {
            lock (this.SyncRoot)
            {
                this.Stream.Close();
            }
        }
    }
}
