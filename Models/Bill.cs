namespace Hospital_Management_System.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public decimal RoomCost { get; set; }
        public decimal TestCost { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal MedicineCost { get; set; }
        public decimal Total { get; set; }
        public int PatientId { get; set; } // Foreign key to Patient
        public int? RoomId { get; set; } // foreign key of room.
        public decimal RemainingBalance { get; set; }
        public int PolicyNumber { get; set; }

        // Navigation properties
        public Patient Patient { get; set; } // Navigation property to Patient
        public Insurance? Insurance { get; set; } // Navigation property to InsurancePolicy
        public Room? Room { get; set; }
        public ICollection<LaboratoryScreening>? LaboratoryScreenings { get; set; }
        public ICollection<Medicine>? Medicine { get; set; }
    }
}
