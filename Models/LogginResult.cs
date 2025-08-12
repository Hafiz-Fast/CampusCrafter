namespace Student_Freelance_Backend.Models
{
    public class LogginResult
    {
        public int UserId { get; set; }
        public string UserType { get; set; }
        public bool Success { get; set; }
        public string message { get; set; }
    }
}
