using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeSharp
{
    /// <summary>
    /// This composite will perform a 'switch' statement to execute a specific branch of logic.
    /// This is useful for selecting specific branches, for different types of agents. (e.g. rogue, mage, and warrior branches)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Switch<T> : Composite
    {
        /// <summary>
        /// The statement assigned to this Switch that will determine which logical branch to take.
        /// </summary>
        protected Func<T> Statement { get; set; }

        /// <summary>
        /// The switch arguments.
        /// </summary>
        protected SwitchArgument<T>[] Arguments { get; set; }

        /// <summary>
        /// The 'default' argument to be carried out if no other switch conditions are met.
        /// </summary>
        protected Composite Default { get; set; }

        public Switch(Func<T> statement, params SwitchArgument<T>[] args)
        {
            Statement = statement;
            Arguments = args;
        }

        public Switch(Func<T> statement, Composite defaultArgument, params SwitchArgument<T>[] args):this(statement, args)
        {
            Default = defaultArgument;
        }

        protected RunStatus RunSwitch()
        {
            if (Arguments == null && Default == null)
            {
                LastException = new Exception("Switch statement has no arguments, and no default statement. Can not run.");
                return RunStatus.Exception;
            }

            // Run the statement, and get the value for our switch.
            T value = Statement();

            // Since we can't do an *actual* switch statement,
            // this is the best we can do. It works in the same way,
            // except that it's slower, and may cause severe performance
            // hits if there are a large number of switch cases.
            if (Arguments != null)
            {
                // Make sure we don't do this query twice.
                var arg = Arguments.First(a => a.RequiredValue.Equals(value));
                if (arg != null)
                    return arg.Branch.Run();
            }

            if (Default != null)
            {
                return Default.Run();
            }

            // Fails if no possible switches are found!
            return RunStatus.Failure;
        }
    }

    public class SwitchArgument<T>
    {
        /// <summary>
        /// A branch of logic that will be executed if this argument is the correct switch.
        /// </summary>
        public Composite Branch { get; set; }

        /// <summary>
        /// The value required for this logic branch to be executed.
        /// </summary>
        public T RequiredValue { get; set; }


        public SwitchArgument(Composite branch, T requiredValue = default(T))
        {
            Branch = branch;
            RequiredValue = requiredValue;
        }
    }
}
