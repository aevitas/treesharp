using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeSharp
{
    /// <summary>
    /// The base Action class. A simple, easy to use, way to execute actions, and return their status of execution.
    /// These are normally considered 'atoms' in that they are executed in their entirety.
    /// </summary>
    public abstract class Action
    {
        /// <summary>
        /// Runs this action, and returns a <see cref="RunStatus"/> describing it's current state of execution.
        /// </summary>
        /// <returns></returns>
        protected abstract RunStatus Run();
    }
}
