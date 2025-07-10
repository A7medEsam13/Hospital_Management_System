namespace Hospital_Management_System.Services
{
    public interface IStaffServices
    {
        public Task Add(Staff staff);
        public Task Remove(string ssn);
        public Task<Staff> GetById(string ssn);
        public void Update(Staff staff);
        public Task<IEnumerable<Staff>> GetAll();
        public Task SaveAsync();
    }
}
