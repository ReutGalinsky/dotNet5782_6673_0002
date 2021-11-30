using System;
using System.Runtime.Serialization;
namespace BO
{
    [Serializable]
    public class AddingProblemException : Exception
    {
        public AddingProblemException() : base() { }
        public AddingProblemException(string Messege) : base(Messege) { }
        public AddingProblemException(string Messege, Exception inner) : base(Messege, inner) { }
        protected AddingProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
    public class GettingProblemException : Exception
    {
        public GettingProblemException() : base() { }
        public GettingProblemException(string Messege) : base(Messege) { }
        public GettingProblemException(string Messege, Exception inner) : base(Messege, inner) { }
        protected GettingProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }


    public class UpdatingException : Exception
    {
        public UpdatingException() : base() { }
        public UpdatingException(string Messege) : base(Messege) { }
        public UpdatingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected UpdatingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
    public class ChargingException : Exception
    {
        public ChargingException() : base() { }
        public ChargingException(string Messege) : base(Messege) { }
        public ChargingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected ChargingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
    public class DeletingException : Exception
    {
        public DeletingException() : base() { }
        public DeletingException(string Messege) : base(Messege) { }
        public DeletingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected DeletingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
        public class ConnectionException : Exception
    {
        public ConnectionException() : base() { }
        public ConnectionException(string Messege) : base(Messege) { }
        public ConnectionException(string Messege, Exception inner) : base(Messege, inner) { }
        protected ConnectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
    }

}
