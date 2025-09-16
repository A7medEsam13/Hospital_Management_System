namespace Hospital_Management_System.Repository
{
    public interface IPayrollRepository
    {
        public Task UpdatePayroll(Payroll payroll);
        public Task Delete(int id);
        public Task CreatePayroll(Payroll payroll);
        public Task<List<DateTime>> GetAllDrawDates(string ssn);
        public Task<List<DateOnly>> GetAllUpdatedDates(string ssn);
        public Task<Payroll> GetBySSN(string ssn);
        public Task<Payroll> GetByID(int id);
    }
}
