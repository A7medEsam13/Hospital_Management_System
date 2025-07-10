namespace Hospital_Management_System.Dto
{
    public class DoctorCreateDto
    {
        // staff properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly JoinDate { get; set; }
        public DateOnly SeparationDate { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public int SSN { get; set; }

        // doctor properties
        public string Qualification { get; set; }
        public int StaffId { get; set; }
        public string Specialization { get; set; }
    }
}
