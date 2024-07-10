using Journey.Exception.ExceptionsBase;
using System.Net;

namespace Journey.Exception
{
    public class NotFoundException : JourneyException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages()
        {
            var error = new List<string>
                {
                    Message
                };
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
