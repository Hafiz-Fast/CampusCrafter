namespace Student_Freelance_Backend.Models
{
    public class Education
    {
        public int freelancerId { get; set; }
        public string institute { get; set; }
        public string degree { get; set; }
        public string program { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
