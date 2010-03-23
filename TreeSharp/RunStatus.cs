using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeSharp
{
    /// <summary>
    /// Values that can be returned from composites and the like.
    /// </summary>
    public enum RunStatus
    {
        Success,
        Failure,
        Running,
        Exception,
    }
}
