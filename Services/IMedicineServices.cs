namespace Hospital_Management_System.Services
{
    public interface IMedicineServices
    {
        public Task AddNewMedicine(MedicineCreationDto dto);
        public Task UpdateMedicineCost(int id, decimal newCost);
        public Task UpdateMedicineQuantity(int id, int newQuantity);
        public Task DeleteMedicine(int id);
        public Task<MedicineDisplayDTO> GetMedicineByID(int id);
        public Task<MedicineDisplayDTO> GetMedicineByName(string name);
        public IEnumerable<MedicineDisplayDTO> GetAllMedicines();
    }
}
