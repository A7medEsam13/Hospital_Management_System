namespace Hospital_Management_System.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Manager")]
        public string ManagerSSN { get; set; }
        public int StaffCount { get; set; }

        // Navigation properties
        public Staff Manager { get; set; } 
    }
}
