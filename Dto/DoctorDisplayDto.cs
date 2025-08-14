namespace Hospital_Management_System.Dto
{
    public class DoctorDisplayDto
    {
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly JoinDate { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
    }
}
