namespace Hospital_Management_System.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public decimal RoomCost { get; set; } = 0;
        public decimal TestCost { get; set; } = 0;
        public decimal MedicineCost { get; set; } = 0;
        public decimal Total { get; set; }
        public int PatientId { get; set; } // Foreign key to Patient
        public int? RoomId { get; set; } // foreign key of room.
        public bool IsPaid { get; set; } = false;
        // Navigation properties
        public Patient Patient { get; set; } // Navigation property to Patient
        public Room? Room { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
