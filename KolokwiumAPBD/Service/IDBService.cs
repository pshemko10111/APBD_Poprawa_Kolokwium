using KolokwiumAPBD.DTOs;

namespace KolokwiumAPBD.Service
{
    public interface IDBService
    {
        public Task<IEnumerable<TaskDTO>> getTasksAsync(int? projectId);
        public Task addTaskAsync(AddTaskDTO AddTaskDTO);
    }
}
