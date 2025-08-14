namespace Hospital_Management_System.Repository
{
    public interface IDoctorRepository
    {
        public Task Add(Doctor doctor); // done
        public Task<Doctor> GetById(string id);  // done
        public void Update(DoctorUpdateDto doctor); // done
        public Task<IEnumerable<DoctorDisplayDto>> GetAll(); // done
        public Task SaveAsync();
    }
}
