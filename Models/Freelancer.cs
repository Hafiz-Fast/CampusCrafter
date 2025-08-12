namespace Student_Freelance_Backend.Models
{
    public class Freelancer
    {
        public int FreelancerId { get; set; }
        public string[] domain { get; set; }
        public string summary { get; set; }
        public string[] ProfficientSkills { get; set; }
        public string[] IntermediateSkills { get; set; }
        public string[] FamiliarSkills { get; set; }
    }
}
