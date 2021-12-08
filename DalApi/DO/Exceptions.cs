using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace DO
{
    [Serializable]
    public class ExistingException : Exception
    {
        public ExistingException() : base() { }
        public ExistingException(string Messege) : base(Messege) { }
        public ExistingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected ExistingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    public class NotExistingException : Exception
    {
        public NotExistingException() : base() { }
        public NotExistingException(string Messege) : base(Messege) { }
        public NotExistingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected NotExistingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}