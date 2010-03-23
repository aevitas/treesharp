using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeSharp
{
    /// <summary>
    /// The base sequence class. This will execute each branch of logic, in order.
    /// If all branches succeed, this composite will return a successful run status.
    /// If any branch fails, this composite will return a failed run status.
    /// </summary>
    public class Sequence : Composite
    {
    }
}
