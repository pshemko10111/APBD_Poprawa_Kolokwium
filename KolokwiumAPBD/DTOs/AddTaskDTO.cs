namespace KolokwiumAPBD.DTOs
{
    public class AddTaskDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public int idProject { get; set; }
        public int idReporter { get; set; }
        public int? idAssignee { get; set; }
    }
}
