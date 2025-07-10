namespace Hospital_Management_System.Services
{
    public interface IDoctorServices
    {
        public Task Add(Doctor doctor); // done
        public Doctor GetById(int id);  // done
        public void Update(Doctor doctor); // done
        public Task<IEnumerable<Doctor>> GetAll(); // done
        public Task SaveAsync();
    }
}
