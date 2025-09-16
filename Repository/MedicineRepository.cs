
namespace Hospital_Management_System.Repository
{
    public class MedicineRepository(ApplicationDbContext _context) : IMedicineRepository
    {
        public async Task AddNewMedicine(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
        }

        public async Task DeleteMedicine(int id)
        {
            await _context.Medicines
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
        }

        public List<Medicine> GetAllMedicines()
        {
            return _context.Medicines.AsNoTracking()
                .ToList();
        }

        public async Task<Medicine> GetByID(int id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        public async Task<Medicine> GetByName(string name)
        {
            return await _context.Medicines.FindAsync(name);
        }

        public async Task UpdateMedicineCost(int id, decimal newCost)
        {
            await _context.Medicines
                .Where(m => m.Id == id)
                .ExecuteUpdateAsync(m => m
                .SetProperty(p => p.Cost, n => newCost));
        }

        public async Task UpdateMedicineQuantity(int id, int newQuantity)
        {
            await _context.Medicines
                .Where(m => m.Id == id)
                .ExecuteUpdateAsync(m => m
                .SetProperty(p => p.Quantity, n => newQuantity));
        }
    }
}
