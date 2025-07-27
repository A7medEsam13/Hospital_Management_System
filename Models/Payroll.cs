namespace Hospital_Management_System.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        [ForeignKey("Staff")]
        public string StaffSSN { get; set; }
        public string IBAN { get; set; }

        // Navigation properties
        public Stuff Staff { get; set; }    
    }
}
