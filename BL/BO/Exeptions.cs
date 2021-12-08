using System;
using System.Runtime.Serialization;
namespace BO
{
    /// <summary>
    /// our exeptions: including exeptions for AGUCDC-adding, getting, updating, charging, deleting and connecting  
    /// </summary>
    [Serializable]
    #region AddingProblemException
    public class AddingProblemException : Exception
    {
        /// <summary>
        /// adding exeption: exeption in case that there is any adding problem.
        /// </summary>
        public AddingProblemException() : base() { }
        public AddingProblemException(string Messege) : base(Messege) { }
        public AddingProblemException(string Messege, Exception inner) : base(Messege, inner) { }
        protected AddingProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    #endregion
    #region GettingProblemException
    public class GettingProblemException : Exception
    {
        /// <summary>
        /// getting exeption: exeption in case that there is any getting problem.
        /// </summary>
        public GettingProblemException() : base() { }
        public GettingProblemException(string Messege) : base(Messege) { }
        public GettingProblemException(string Messege, Exception inner) : base(Messege, inner) { }
        protected GettingProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    #endregion
    #region UpdatingException
    public class UpdatingException : Exception
    {
        /// <summary>
        /// updating exeption: exeption in case that there is any updating problem.
        /// </summary>
        public UpdatingException() : base() { }
        public UpdatingException(string Messege) : base(Messege) { }
        public UpdatingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected UpdatingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    #endregion
    #region ChargingException
    public class ChargingException : Exception
    {
        /// <summary>
        /// charging exeption: exeption in case that there is any charging problem.
        /// </summary>
        public ChargingException() : base() { }
        public ChargingException(string Messege) : base(Messege) { }
        public ChargingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected ChargingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    #endregion
    #region DeletingException
    public class DeletingException : Exception
    {
        /// <summary>
        /// deleting exeption: exeption in case that there is any deleting problem.
        /// </summary>
        public DeletingException() : base() { }
        public DeletingException(string Messege) : base(Messege) { }
        public DeletingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected DeletingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    #endregion
    #region ConnectionException
    public class ConnectionException : Exception
    {
        /// <summary>
        /// connecting exeption: exeption in case that there is any connecting problem.
        /// </summary>
        public ConnectionException() : base() { }
        public ConnectionException(string Messege) : base(Messege) { }
        public ConnectionException(string Messege, Exception inner) : base(Messege, inner) { }
        protected ConnectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    #endregion
}