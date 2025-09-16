namespace Hospital_Management_System.Repository
{
    public interface IBillRepository
    {
        public Task CreateNewBill(Bill bill);
        public Task<List<Bill>> GetAllPatientBills(int patientID);
        public Task UpdateBill(Bill bill);
        public Task DeleteBill(int billID);
        public Task<Bill> GetByID(int id);
    }
}
