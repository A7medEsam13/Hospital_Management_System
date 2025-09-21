namespace Hospital_Management_System.Dto
{
    public class BillDisplayDto
    {
        public int ID { get; set; }
        public int PatientId { get; set; } 
        public DateOnly Date { get; set; }
        public decimal RoomCost { get; set; }
        public decimal TestsCost { get; set; }
        public decimal MedicineCost { get; set; }
        public decimal Total { get; set; }
    }
}
