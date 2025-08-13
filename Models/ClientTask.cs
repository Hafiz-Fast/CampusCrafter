namespace Student_Freelance_Backend.Models
{
    public class ClientTask
    {
        public string taskType { get; set; }
        public string taskTitle { get; set; }
        public string taskDescription { get; set; }
        public string taskRequirements { get; set; }
        public DateOnly taskDeadline { get; set; }
        public float taskBudget { get; set; }
    }
}
