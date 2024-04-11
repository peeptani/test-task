namespace test_task.Models
{
    public class HelpdeskRequest
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsResolved { get; set; } = false;
    }
}
