namespace Hospital_Management_System.Repository
{
    public interface IStuffRepository
    {
        public Task Add(Stuff staff);
        public Task Remove(string ssn);
        public Task<Stuff> GetById(string ssn);
        public void Update(Stuff staff);
        public Task<IEnumerable<Stuff>> GetAll();
        public Task SaveAsync();
        public Task<bool> IsExists(string ssn);
        public Task<string> GetStuffSSN(string userID);
    }
}
