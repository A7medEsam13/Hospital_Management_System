
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class BillServices(IMapper mapper,
        ILogger<BillServices> logger,
        IUnitOfWork unitOfWork
        ) : IBillServices
    {

        private readonly IMapper _mapper = mapper;
        private readonly ILogger<BillServices> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task CreateNewBill(BillCreationDto dto)
        {
            if(dto is null)
            {
                _logger.LogError("Invalid Model");
                throw new Exception("Invalid Model");
            }
            var patientBills = await _unitOfWork.Bills.GetAllPatientBills(dto.PatientID);
            if (patientBills.Any(b => !b.IsPaid))
            {
                _logger.LogWarning("Can not create a new bill while is not paied bill exists");
                throw new Exception("there is unpaid bill");
            }
            _logger.LogInformation("mapping the dto to entity");
            var billEntity = _mapper.Map<Bill>(dto);
            billEntity.Date = DateOnly.FromDateTime(DateTime.Now);

            var patient = await _unitOfWork.Patients.GetPatientById(billEntity.PatientId);
            if(!patient.IsRoomPaied)
            {

                _logger.LogInformation("Getting and adding room cost of patient {id}", billEntity.PatientId);
                var roomID = await _unitOfWork.Rooms.GetRoomIdByPatientId(billEntity.PatientId);
                billEntity.RoomId = roomID;
                billEntity.RoomCost = await _unitOfWork.Rooms.GetRoomCost(roomID);

            }

            _logger.LogInformation("Adding the bill entity to database");
            await _unitOfWork.Bills.CreateNewBill(billEntity);
            await _unitOfWork.Complete();

            //_logger.LogInformation("Getting and adding Screenigs cost of patient {id}", billEntity.PatientId);
            //var screenings = await _unitOfWork.LaboratoryScreenings.GetAllPatientScreenings(billEntity.PatientId);
            //var screeningsCost = screenings.Select(s => s.TestCost);
            //foreach(var cost in screeningsCost)
            //{
            //    billEntity.TestCost += cost;
            //}


            _logger.LogInformation("Getting and adding medicines cost of patient {id}", billEntity.PatientId);
            var UnPaidPrescriptions = await _unitOfWork.Prescriptions.GetUnPaidPrescriptions(billEntity.PatientId);
            foreach(var prescription in UnPaidPrescriptions)
            {
                var prescriptionMedicines = _unitOfWork.PrescriptionMedicines.GetMedicinesOfPrescription(prescription.Id);
                foreach(var presscriptionMedicine in prescriptionMedicines)
                {
                    billEntity.MedicineCost += presscriptionMedicine.Medicine.Cost;
                }
                var prescriptionScreenings = await _unitOfWork.Prescriptions.GetAllPrescriptionScreenings(prescription.Id);
                foreach(var screening in prescriptionScreenings)
                {
                    billEntity.TestCost += screening.LaboratoryScreening.TestCost;
                }

            }

            _logger.LogInformation("calculate the total amount");
            billEntity.Total = billEntity.RoomCost + billEntity.TestCost + billEntity.MedicineCost;

            billEntity.Prescriptions = UnPaidPrescriptions;
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation("Deleting bill with ID {id}", id);
            await _unitOfWork.Bills.DeleteBill(id);
        }

        public async Task<List<BillDisplayDto>> GetAllPatientBills(int patientID)
        {
            var bills = await _unitOfWork.Bills.GetAllPatientBills(patientID);

            if(bills is null || bills.Count == 0)
            {
                _logger.LogWarning("this patient {id} does not has any bills", patientID);
                throw new Exception("there is no bills to this patient");
            }

            _logger.LogInformation("Mappint the entities to dtos");
            var dtos = _mapper.Map<List<BillDisplayDto>>(bills);
            return dtos;
        }

        public async Task<BillDisplayDto> GetByID(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid ID");
                throw new Exception("Invalid ID");
            }

            var bill = await _unitOfWork.Bills.GetByID(id);
            if(bill is null)
            {
                _logger.LogWarning("no bill exists with this ID {id}", id);
                throw new Exception($"No bill exists with this ID {id}");
            }

            var dto = _mapper.Map<BillDisplayDto>(bill);
            return dto;
        }

        

        public async Task<bool> PayBill(int billID)
        {
            var bill = await _unitOfWork.Bills.GetByID(billID);
            bill.IsPaid = true;
            await _unitOfWork.Bills.Pay(billID);
            foreach(var prescription in bill.Prescriptions)
            {
                prescription.IsPaid = true;
                await _unitOfWork.Prescriptions.Pay(prescription);
            }
            var patient = await _unitOfWork.Patients.GetPatientById(bill.PatientId);
            patient.IsRoomPaied = true;
            await _unitOfWork.Complete();

            var paidBill = await _unitOfWork.Bills.GetByID(billID);
            return paidBill.IsPaid;
        }
    }
}
