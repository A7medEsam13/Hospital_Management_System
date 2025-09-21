namespace Hospital_Management_System.Repository
{
    public interface IDoctorRepository
    {
        public Task Add(Doctor doctor); // done
        public Task<Doctor> GetById(string id);  // done
        public Task Update(Doctor doctor); // done
        public Task<IEnumerable<Doctor>> GetAll(); // done
    }
}
