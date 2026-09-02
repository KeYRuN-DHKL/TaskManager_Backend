using System.Net;

namespace TaskManager.Core.TaskManagerExceptions
{
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException(string message) : base(message,HttpStatusCode.NotFound) { }
    }
}
