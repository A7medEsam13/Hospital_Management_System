
using Hospital_Management_System.UnitOfWork;

namespace Hospital_Management_System.Services
{
    public class MedicineServices(IUnitOfWork _unitOfWork, 
        ILogger<MedicineServices> _logger,
        IMapper _mapper) : IMedicineServices
    {
        public async Task AddNewMedicine(MedicineCreationDto dto)
        {
            _logger.LogInformation("Mapping the dto to entity");
            var entity = _mapper.Map<Medicine>(dto);

            await _unitOfWork.Medicines.AddNewMedicine(entity);
            await _unitOfWork.Complete();
        }

        public async Task DeleteMedicine(int id)
        {
            var medicine = await _unitOfWork.Medicines.GetByID(id);

            if(medicine is null)
            {
                _logger.LogWarning("There is no medicine with this ID {id} exists in the database", id);
                throw new KeyNotFoundException();
            }

            await _unitOfWork.Medicines.DeleteMedicine(id);
        }

        public IEnumerable<MedicineDisplayDTO> GetAllMedicines()
        {
            var medicines = _unitOfWork.Medicines.GetAllMedicines();

            if(medicines is null || !medicines.Any())
            {
                _logger.LogWarning("There is no medicines exist");
                throw new Exception("no medicines in database");
            }

            var dtos = _mapper.Map<IEnumerable<MedicineDisplayDTO>>(medicines);

            return dtos;
        }

        public async Task<MedicineDisplayDTO> GetMedicineByID(int id)
        {
            _logger.LogInformation("Getting the entity from database with ID {id}", id);
            var medicine = await _unitOfWork.Medicines.GetByID(id);

            if(medicine is null)
            {
                _logger.LogWarning("There is no medicine with this ID {id} exists in the database", id);
                throw new KeyNotFoundException();
            }

            var dto = _mapper.Map<MedicineDisplayDTO>(medicine);
            return dto;
        }

        public async Task<MedicineDisplayDTO> GetMedicineByName(string name)
        {
            _logger.LogInformation("Getting the medicine with name {name} from database", name);
            var medicine = await _unitOfWork.Medicines.GetByName(name);

            if(medicine is null)
            {
                _logger.LogWarning("There is no medicine exists with this name {name}", name);
                throw new KeyNotFoundException();
            }

            _logger.LogInformation("Mapping the entity to dto");
            var dto = _mapper.Map<MedicineDisplayDTO>(medicine);
            return dto;
        }

        public async Task UpdateMedicineCost(int id, decimal newCost)
        {
            if (newCost <= 0)
            {
                _logger.LogError("Invalid Cost");
                throw new Exception("Invalid Cost");
            }

            _logger.LogInformation("Updating the cost of medicine {id}", id);
            await _unitOfWork.Medicines.UpdateMedicineCost(id, newCost);
        }

        public async Task UpdateMedicineQuantity(int id, int newQuantity)
        {
            _logger.LogInformation("Updating the quantity of medicine {id}", id);
            await _unitOfWork.Medicines.UpdateMedicineQuantity(id, newQuantity);
        }
    }
}
