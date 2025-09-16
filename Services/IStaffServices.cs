namespace Hospital_Management_System.Services
{
    public interface IStaffServices
    {
        public Task Create(StuffCreateDto stuff);
        public Task Terminate(string ssn);
        public Task<Stuff> GetById(string ssn);
        public Task Update(StuffUpdateDto staff);
        public Task<IEnumerable<StuffDisplayDto>> GetAll();
        public Task<StuffDisplayDto> GetByUserID(string userID);
    }
}
