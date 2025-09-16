namespace Hospital_Management_System.Repository
{
    public interface IMedicineRepository
    {
        public Task AddNewMedicine(Medicine medicine);
        public Task UpdateMedicineCost(int id, decimal newCost);
        public Task UpdateMedicineQuantity(int id, int newQuantity);
        public Task DeleteMedicine(int id);
        public Task<Medicine> GetByID(int id);
        public Task<Medicine> GetByName(string name);
        public List<Medicine> GetAllMedicines();
    }
}
