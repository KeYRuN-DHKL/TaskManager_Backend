using System.Net;

namespace TaskManager.Core.TaskManagerExceptions
{
    public class ProjectNotFoundException : AppException
    {
        public ProjectNotFoundException(string message) : base(message,HttpStatusCode.NotFound) { }
    }
}
