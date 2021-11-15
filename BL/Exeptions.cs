using System;
using System.Runtime.Serialization;
namespace IBL.BO
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
}
