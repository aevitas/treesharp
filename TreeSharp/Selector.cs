using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeSharp
{
    /// <summary>
    /// The base selector class. This will attempt to execute all branches of logic, until one succeeds. 
    /// This composite will fail only if all branches fail as well.
    /// </summary>
    public abstract class Selector : Composite
    {
    }

    /// <summary>
    /// Will execute each branch of logic by priority, until one succeeds. This composite
    /// will fail only if all branches fail as well.
    /// </summary>
    public class PrioritySelector : Selector
    {
        
    }

    /// <summary>
    /// Will execute random branches of logic, until one succeeds. This composite
    /// will fail only if all branches fail as well.
    /// </summary>
    public class ProbabilitySelector : Selector
    {
        
    }
}
