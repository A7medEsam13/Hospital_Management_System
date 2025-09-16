namespace Hospital_Management_System.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        [ForeignKey("Staff")]
        public string StaffSSN { get; set; }
        public string IBAN { get; set; }
        public List<DateOnly> UpdatedDate { set; get; } = new List<DateOnly>();
        public List<DateTime> DrawTimes { get; set; } = new List<DateTime>();

        // Navigation properties
        public Stuff Staff { get; set; }    
    }
}
