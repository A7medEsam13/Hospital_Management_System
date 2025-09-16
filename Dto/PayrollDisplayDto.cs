namespace Hospital_Management_System.Dto
{
    public class PayrollDisplayDto
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public string StuffName { set; get; }
        public string StaffSSN { get; set; }
        public string IBAN { get; set; }
        public List<DateOnly> UpdatedDate { set; get; } = new List<DateOnly>();
        public List<DateTime> DrawTimes { get; set; } = new List<DateTime>();
    }
}
