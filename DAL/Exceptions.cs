﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace IDAL.DO
{
    [Serializable]
    class ExistingException:Exception//exception for item that already exist in the data base
    {
        public ExistingException() : base() { }
        public ExistingException(string Messege) : base(Messege) { }
        public ExistingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected ExistingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    class NotExistingException : Exception//exception for item that not exist in the data base
    {
        public NotExistingException() : base() { }
        public NotExistingException(string Messege) : base(Messege) { }
        public NotExistingException(string Messege, Exception inner) : base(Messege, inner) { }
        protected NotExistingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
