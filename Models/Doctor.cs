namespace Hospital_Management_System.Models
{
    public class Doctor 
    {
        public int Id { get; set; }
        public string Qualification { get; set; }
        [ForeignKey("Staff")]
        public string StaffSSN { get; set; }
        public string Specialization { get; set; }

        // Navigation properties
        public Staff Staff { get; set; } 
    }
}
