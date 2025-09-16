
using Hospital_Management_System.UnitOfWork;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hospital_Management_System.Services
{
    public class PayrollServices(IUnitOfWork _unitOfWork, ILogger<PayrollServices> _logger, IMapper _mapper) : IPayrollServices
    {
        
        public async Task CreatePayroll(PayrollCreateDto dto)
        {
            if(dto is null)
            {
                _logger.LogError("Can not create a null payroll!");
                throw new ArgumentNullException();
            }

            _logger.LogInformation("Mappint the data transefere object to payroll entity");
            var payroll = _mapper.Map<Payroll>(dto);

            _logger.LogInformation("Create and save the new payroll to the database");
            await _unitOfWork.Payrolls.CreatePayroll(payroll);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation("Passing the id {id} to the delete method", id);
            await _unitOfWork.Payrolls.Delete(id);
        }

        public async Task<List<DateTime>> GetAllDrawDates(string ssn)
        {
            _logger.LogInformation("Getting all draw dates of stuff that has {ssn} SSN", ssn);

            var dates = await _unitOfWork.Payrolls.GetAllDrawDates(ssn);
            if(dates is null)
            {
                _logger.LogWarning("stuff {ssn} does not have any draws", ssn);
                throw new Exception("there is no draws dates");
            }

            return dates;
        }

        public async Task<List<DateOnly>> GetAllUpdatedDates(string ssn)
        {
            _logger.LogInformation("Getting the update dates of the stuff {ssn} payroll", ssn);

            var dates = await _unitOfWork.Payrolls.GetAllUpdatedDates(ssn);
            if(dates is null)
            {
                _logger.LogWarning("stuff {ssn} does not has any updates on his payroll", ssn);
                throw new Exception("no updates");
            }

            return dates;
        }

        public async Task<PayrollDisplayDto> GetByID(int id)
        {
            _logger.LogInformation("Getting the payroll from the database");
            var payroll = await _unitOfWork.Payrolls.GetByID(id);

            if(payroll is null)
            {
                _logger.LogWarning("there is no payroll exists in database with this ID {id}", id);
                throw new NullReferenceException("there is no payroll with this ID exists");
            }

            _logger.LogInformation("Mapping from the entity to the display dto");
            var dto = _mapper.Map<PayrollDisplayDto>(payroll);
            return dto;
        }

        public async Task<PayrollDisplayDto> GetBySSN(string ssn)
        {
            _logger.LogInformation("Getting the payroll of the stuff {ssn} from the database", ssn);
            var payroll = await _unitOfWork.Payrolls.GetBySSN(ssn);

            if(payroll is null)
            {
                _logger.LogWarning("the stuff {ssn} does not has payroll", ssn);
                throw new Exception($"the stuff {ssn} does not has payroll");
            }

            _logger.LogInformation("Mapping the entity to display dto");
            var dto = _mapper.Map<PayrollDisplayDto>(payroll);
            return dto;
        }

        public async Task UpdatePayroll(string ssn, PayrollUpdateDto dto)
        {
            var payroll = await _unitOfWork.Payrolls.GetBySSN(ssn);
            payroll.Salary = dto.Salary;

            payroll.UpdatedDate.Add(DateOnly.FromDateTime(DateTime.Now));


            _logger.LogInformation("Updating the payroll");
            await _unitOfWork.Payrolls.UpdatePayroll(payroll);
        }
    }
}
