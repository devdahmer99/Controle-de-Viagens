using Journey.Exception.ExceptionsBase;
using System.Net;
using System.Net.Http.Headers;

namespace Journey.Exception
{
    public class NotFoundException(string message) : JourneyException(message)
    {
        public override IList<string> GetErrorMessages()
        {
              return [ Message ];
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
