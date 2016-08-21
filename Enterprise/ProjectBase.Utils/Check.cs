using System;
using System.Diagnostics;


namespace ProjectBase.Utils
{
    /// <summary>
    /// Precondition Check
    /// Each method generates an exception or trace assertion statment if the contract is broken.
    /// </summary>
    public sealed class Check
    {

        /// <summary>
        /// Precondition Check
        /// </summary>
        public static void Require(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new PreconditionException("Precondition failed"); }
                    
            }
            else
            {
                Trace.Assert(assertion, "Precondition failed");
            }
        }

        /// <summary>
        /// Precondition Check
        /// </summary>
        public static void Require(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new PreconditionException(message); }
                    
            }
            else
            {
                Trace.Assert(assertion, "Precondition: " + message);
            }
        }

        /// <summary>
        /// Precondition Check
        /// </summary>
        public static void Require(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new PreconditionException(message, inner); }
            }
            else
            {
                Trace.Assert(assertion, "Precondition: " + message);
            }
        }

        /// <summary>
        /// Postcondition Check
        /// </summary>
        public static void Ensure(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new PostconditionException("Postcondition failed"); }
            }
            else
            {
                Trace.Assert(assertion, "PostCondition failed");
            }
        }

        /// <summary>
        /// Postcondition Check
        /// </summary>
        public static void Ensure(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new PostconditionException(message); }
            }
            else
            {
                Trace.Assert(assertion, "Postcondition: " + message);
            }
        }

        /// <summary>
        /// Postcondition Check
        /// </summary>
        public static void Ensure(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new PostconditionException(message, inner); }
            }
            else
            {
                Trace.Assert(assertion, "Postcondition: " + message);
            }
        }

        /// <summary>
        /// Invariant Check
        /// </summary>
        public static void Invariant(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new InvariantException("Assertion failed"); }
            }
            else
            {
                Trace.Assert(assertion, "Invariant failed");
            }
        }

        /// <summary>
        /// Invariant Check
        /// </summary>
        public static void Invariant(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new InvariantException(message); }
            }
            else
            {
                Trace.Assert(assertion, "Invariant: " + message);
            }
        }

        /// <summary>
        /// Invariant Check
        /// </summary>
        public static void Invariant(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new InvariantException(message, inner); }
            }
            else
            {
                Trace.Assert(assertion, "Invariant: " + message);
            }
        }

        /// <summary>
        /// Assertion Check
        /// </summary>
        public static void Assert(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new AssertionException("Assertion failed"); }
            }
            else
            {
                Trace.Assert(assertion, "Assert failed");
            }

        }

        /// <summary>
        /// Assertion Check
        /// </summary>
        public static void Assert(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new AssertionException(message); }
            }
            else
            {
                Trace.Assert(assertion, "Assert: " + message);
            }
        }

        /// <summary>
        /// Assertion Check
        /// </summary>
        public static void Assert(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) { throw new AssertionException(message, inner); }
            }
            else
            {
                Trace.Assert(assertion, "Assert: " + message);
            }
        }
        #region Implementation

        private Check() { }


        public static bool UseExceptions
        {
            get { return !useAssertions; }
        }

        public static bool UseAssertions
        {
            get { return useAssertions; }
            set { useAssertions = value; }
        }

        //Are trace assertion statements begin used
        //Dafault is to use exception handling.

        private static bool useAssertions = false;

        #endregion
    }

    #region Exception

    /// <summary>
    /// Design By Contract Checks.
    /// </summary>
    public class DesignByContract : ApplicationException
    {
        protected DesignByContract() { }
        protected DesignByContract(string message) : base(message) { }
        protected DesignByContract(string message, Exception inner) : base(message, inner) { }
       
    }
    /// <summary>
    /// Exception raised if precondition failed
    /// </summary>
    public class PreconditionException : DesignByContract
    {
        public PreconditionException() { }
        public PreconditionException(string message) : base(message) { }
        public PreconditionException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Exception raised if postcondition failed
    /// </summary>
    public class PostconditionException : DesignByContract
    {
        public PostconditionException() { }
        public PostconditionException(string message) : base(message) { }
        public PostconditionException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Exception raised if invariant failed
    /// </summary>
    public class InvariantException : DesignByContract
    {
        public InvariantException() { }
        public InvariantException(string message) : base(message) { }
        public InvariantException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Exception raised if assertion failed
    /// </summary>
    public class AssertionException : DesignByContract
    {
        public AssertionException() { }
        public AssertionException(string message) : base(message) { }
        public AssertionException(string message, Exception inner) : base(message, inner) { }
    }

    #endregion 
}
