using KolokwiumAPBD.Context;
using KolokwiumAPBD.DTOs;
using Microsoft.EntityFrameworkCore;

namespace KolokwiumAPBD.Service
{
    public class DBService : IDBService
    {
        private readonly KolokwiumContext _context;

        public DBService(KolokwiumContext context)
        {
            _context = context;
        }

        public async Task addTaskAsync(AddTaskDTO addTaskDTO)
        {
            var project = await _context.Projects.FindAsync(addTaskDTO.idProject);
            if (project == null) throw new Exception("Project does not exist");

            var reporter = await _context.Users.SingleOrDefaultAsync(c => c.IdUser == addTaskDTO.idReporter);
            if (reporter == null) throw new Exception("Reporter does not exist");


            if (addTaskDTO.idAssignee != null)
            {
                if (_context.Users.FirstAsync(c => c.IdUser == addTaskDTO.idAssignee).Result == null) throw new Exception("Assignee does not exist");
            } else
            {
                var assignee = await _context.Projects.FirstAsync(c => c.IdProject == addTaskDTO.idProject);
                addTaskDTO.idAssignee = assignee.IdDefaultAssignee;
            }

            var task = new Task
            {
                Name = addTaskDTO.Name,
                Description = addTaskDTO.Description,
                IdProject = addTaskDTO.IdProject,
                IdReporter = addTaskDTO.IdReporter,
                IdAssignee = addTaskDTO.IdAssignee,
                CreatedAt = DateTime.UtcNow
            };
            

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
                    
        }

        public async Task<IEnumerable<TaskDTO>> getTasksAsync(int? projectId)
        {
            if (_context.Tasks.AnyAsync(t => t.ProjectId == projectId).Result)
            {
                return await _context.Tasks.Where(t => projectId == t.IdProject).Select(t => new TaskDTO
                {
                    idTask = t.IdTask,
                    name = t.Name,
                    description = t.Description,
                    createdAt = t.CreatedAt,
                    idProject = (int)projectId,
                    idReporter = t.IdReporter,
                    reporter = new UserDTO
                    {
                        firstName = t.FirstName,
                        lastName = t.LastName
                    },
                    idAssignee = t.IdAssignee,
                    assignee = new UserDTO
                    {
                        firstName = t.FirstName,
                        lastName = t.LastName
                    }
                }).ToListAsync();
            } else {
                return await _context.Tasks.Select(t => new TaskDTO
                {
                    idTask = t.IdTask,
                    name = t.Name,
                    description = t.Description,
                    createdAt = t.CreatedAt,
                    idProject = t.IdProject,
                    idReporter = t.IdReporter,
                    reporter = new UserDTO
                    {
                        firstName = t.FirstName,
                        lastName = t.LastName
                    },
                    idAssignee = t.IdAssignee,
                    assignee = new UserDTO
                    {
                        firstName = t.FirstName,
                        lastName = t.LastName
                    }
                }).ToListAsync();
            }
            
        }
    }
}
