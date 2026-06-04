namespace TaskManager.Core.TaskManagerExceptions
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(string message) : base(message) { }
    }
}
