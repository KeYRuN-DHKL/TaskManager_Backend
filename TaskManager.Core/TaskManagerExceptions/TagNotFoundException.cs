using System.Net;

namespace TaskManager.Core.TaskManagerExceptions
{
    public class TagNotFoundException : AppException
    {
        public TagNotFoundException(string message) : base(message,HttpStatusCode.NotFound) { }
    }
}
