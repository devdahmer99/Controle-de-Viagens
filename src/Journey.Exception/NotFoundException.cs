using Journey.Exception.ExceptionsBase;
using System.Net;

namespace Journey.Exception
{
    public abstract class NotFoundException : JourneyException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
