using System.Net;

namespace TaskManager.Core.TaskManagerExceptions
{
    public class TaskNotFoundException : AppException
    {
        public TaskNotFoundException(string message) : base(message,HttpStatusCode.NotFound) { }
    }
}
