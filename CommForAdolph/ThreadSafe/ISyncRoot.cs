 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CommForAdolph.ThreadSafe
{
    public interface ISyncRoot<T>:IList<T>
    {
        object syncRoot
        {
            get;
        }
    }
    public interface ISyncRoot:IEnumerable
    {
        object syncRoot
        {
            get;
        }
    }
}
