namespace TaskManager.Core.TaskManagerExceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(string message) : base(message) { }
    }
}
