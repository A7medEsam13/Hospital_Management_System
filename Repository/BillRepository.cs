
namespace Hospital_Management_System.Repository
{
    public class BillRepository(ApplicationDbContext context) : IBillRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task CreateNewBill(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
        }

        public async Task DeleteBill(int billID)
        {
            await _context.Bills
                .Where(b => b.Id == billID)
                .ExecuteDeleteAsync();
        }

        public async Task<List<Bill>> GetAllPatientBills(int patientID)
        {
            return await _context.Bills
                .AsNoTracking()
                .Where(b => b.PatientId == patientID)
                .Include(b => b.Patient)
                .ToListAsync();
        }

        public async Task<Bill> GetByID(int id)
        {
            return await _context.Bills
                .Include(b => b.Patient)
                .Include(b => b.Room)
                .Include(b=>b.Prescriptions)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateBill(Bill bill)
        {
            await _context.Bills
                .Where(b => b.Id == bill.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(o => o.RoomCost, n => bill.RoomCost)
                .SetProperty(o => o.TestCost, n => bill.TestCost)
                .SetProperty(o => o.MedicineCost, n => bill.MedicineCost)
                .SetProperty(o => o.Total, n => bill.Total));
        }
    }
}
