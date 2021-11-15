using System;
using System.Runtime.Serialization;
namespace IBL.BO
{
    [Serializable]
    public class AddingProblemException:Exception
    {
    public AddingProblemException() : base() { }
    public AddingProblemException(string Messege) : base(Messege) { }
    public AddingProblemException(string Messege, Exception inner) : base(Messege, inner) { }
    protected AddingProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
