namespace Student_Freelance_Backend.Models
{
    public class Proposal
    {
        public int freelancerId { get; set; }
        public int taskId { get; set; }
        public string request { get; set; }
        public string[] links { get; set; }
        public float bidAmount { get; set; }
        public int flag { get; set; }
    }
}
