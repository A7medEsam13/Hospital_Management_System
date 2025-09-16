
using System.Threading.Tasks;

namespace Hospital_Management_System.Repository
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePayroll(Payroll payroll)
        {
            await _context.Payrolls.AddAsync(payroll);
        }

        public async Task Delete(int id)
        {
            await _context.Payrolls
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<DateTime>> GetAllDrawDates(string ssn)
        {
            var payroll = await GetBySSN(ssn);
            return payroll.DrawTimes;
        }

        public async Task<List<DateOnly>> GetAllUpdatedDates(string ssn)
        {
            var payroll = await GetBySSN(ssn);
            return payroll.UpdatedDate;
        }

        public async Task<Payroll> GetByID(int id)
        {
            return await _context.Payrolls.FindAsync(id);
        }

        public async Task<Payroll> GetBySSN(string ssn)
        {
            return await _context.Payrolls.FirstOrDefaultAsync(p => p.StaffSSN == ssn);
        }

        

        public async Task UpdatePayroll(Payroll payroll)
        {
            await _context.Payrolls.ExecuteUpdateAsync(s => s
            .SetProperty(p => p.Salary, n => payroll.Salary)
            .SetProperty(p => p.UpdatedDate, n => payroll.UpdatedDate)
            .SetProperty(p => p.DrawTimes, n => payroll.DrawTimes));
        }
    }
}
