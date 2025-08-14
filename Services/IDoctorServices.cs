namespace Hospital_Management_System.Services
{
    public interface IDoctorServices
    {
        public Task Add(DoctorCreateDto doctor); // done
        public Task<Doctor> GetById(string id);  // done
        public Task Update(DoctorUpdateDto doctor); // done
        public Task<IEnumerable<DoctorDisplayDto>> GetAll(); 
    }
}
