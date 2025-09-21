namespace Hospital_Management_System.Services
{
    public interface IBillServices
    {
        public Task CreateNewBill(BillCreationDto dto); //////////
        public Task Delete(int id);     //////////
        public Task<List<BillDisplayDto>> GetAllPatientBills(int patientID);    ////////////
        public Task<BillDisplayDto> GetByID(int id);   ////////////
        public Task<bool> PayBill(int billID);     ////////////
    }
}
