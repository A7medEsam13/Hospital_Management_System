namespace Hospital_Management_System.Services
{
    public interface IPayrollServices
    {
        public Task UpdatePayroll(string ssn, PayrollUpdateDto dto); //
        public Task Delete(int id); //
        public Task CreatePayroll(PayrollCreateDto dto); //
        public Task<List<DateTime>> GetAllDrawDates(string ssn); //
        public Task<List<DateOnly>> GetAllUpdatedDates(string ssn); // 
        public Task<PayrollDisplayDto> GetByID(int id);
        public Task<PayrollDisplayDto> GetBySSN(string ssn);
    }
}
