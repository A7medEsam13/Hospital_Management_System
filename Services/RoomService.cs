
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RoomService> _logger;

        public RoomService(ILogger<RoomService> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddPatientToRoom(int roomId, int patientId)
        {
            if(await IsFull(roomId))
            {
                _logger.LogWarning("Room With ID {id} is Full", roomId);
                return false;
            }

            _logger.LogInformation("Getting the patient from database with ID {pID}", patientId);
            var patient = await _unitOfWork.Patients.GetPatientById(patientId);
            if(patient == null)
            {
                _logger.LogError("patient with ID {id} is not exist", patientId);
                throw new NullReferenceException($"patient with ID {patientId} is not exist");
            }


            _logger.LogInformation("setting the room Id to {Id}", roomId);
            patient.RoomId = roomId;
            _logger.LogInformation("updating and saving the patient details in the database");
            _unitOfWork.Patients.Updatepatient(patient);


            _logger.LogInformation("Updating the Number of patients of room {id}", roomId);
            var room = await _unitOfWork.Rooms.GetRoomByID(roomId);
            room.NumberOfPatients++;
            await _unitOfWork.Rooms.UpdateNumberOfPatients(room);
            _logger.LogInformation("Saving changes to database");
            await _unitOfWork.Complete();

            return true;
            
        }

        public async Task AddRoom(RoomCreationDto roomDTO)
        {
            _logger.LogInformation("mapping the room creation dto to entity");
            var room = new Room()
            {
                Type = roomDTO.Type,
                Cost = roomDTO.Cost,
                DepartmentName = roomDTO.DepartmentName,
                Capacity = roomDTO.Capacity
            };

            _logger.LogInformation("Adding and saving the room to database");
            await _unitOfWork.Rooms.AddRoom(room);
            await _unitOfWork.Complete();
        }

        public async Task DeletePatientFromRoom(int patientId)
        {
            _logger.LogInformation("Getting the patient from database");
            var patient = await _unitOfWork.Patients.GetPatientById(patientId);
            if (patient == null)
            {
                _logger.LogError("Patient with ID {id} is not exist", patientId);
                throw new NullReferenceException("Patient is not exist");
            }
            patient.RoomId = 0;
            _logger.LogInformation("updating and saving the patient data");
            _unitOfWork.Patients.Updatepatient(patient);
            await _unitOfWork.Complete();
        }

        public IEnumerable<RoomDisplayDto> GetAllRooms()
        {
            _logger.LogInformation("Getting all rooms from the database");
            var rooms = _unitOfWork.Rooms.GetAllRooms()
                .Select(r => new RoomDisplayDto()
                {
                    Id = r.Id,
                    Cost = r.Cost,
                    Type = r.Type,
                    DepartmentName = r.DepartmentName
                });
            if (!rooms.Any())
            {
                _logger.LogWarning("no rooms exist");
                return Enumerable.Empty<RoomDisplayDto>();
            }
            return rooms;
        }

        public IEnumerable<RoomDisplayDto> GetDepartmentRooms(string departmentName)
        {
            _logger.LogInformation("getting all {dep} department room", departmentName);
            var rooms = _unitOfWork.Rooms.GetDepartmentRooms(departmentName)
                .Select(r => new RoomDisplayDto()
                {
                    Id = r.Id,
                    Cost = r.Cost,
                    Type = r.Type,
                    DepartmentName = r.DepartmentName
                });

            if (!rooms.Any())
            {
                _logger.LogWarning("{dept} department does not have any rooms", departmentName);
                return Enumerable.Empty<RoomDisplayDto>();
            }
            _logger.LogInformation("mapping and returning the Room Display Dto");
            return rooms.Select(r => new RoomDisplayDto()
            {
                Id = r.Id,
                Cost = r.Cost,
                Type = r.Type,
                DepartmentName = r.DepartmentName
            });
        }

        public async Task<decimal> GetRoomCost(int roomId)
        {
            _logger.LogInformation("getting the room {id} cost from database", roomId);
            return await _unitOfWork.Rooms.GetRoomCost(roomId);
        }

        public async Task<int?> GetRoomIdByPatientId(int patientId)
        {
            _logger.LogInformation("getting the room ID of patient {id}", patientId);
            var roomID = await _unitOfWork.Rooms.GetRoomIdByPatientId(patientId);
            if (roomID == null)
            {
                _logger.LogWarning("patient {id} does not have any room", patientId);
                return roomID;
            }
            _logger.LogInformation("patient {id} has a room", patientId);
            return roomID;
        }

        public  IEnumerable<PatientDisplayDto> GetRoomPatients(int roomId)
        {
            _logger.LogInformation("getting all patients of room {id}", roomId);
            var patients = _unitOfWork.Patients.GetAllPatients();
            var roomPatients = patients.Where(p => p.RoomId == roomId)
                .Select(p => new PatientDisplayDto()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                    BloodType = p.BloodType,
                    Email = p.Email,
                    Gender = p.Gender,
                    SSN = p.SSN,
                    AdmissionDate = p.AdmissionDate,
                    RoomId = p.RoomId
                }).AsEnumerable();

            if (roomPatients == null || !roomPatients.Any())
            {
                _logger.LogWarning("room {id} is empty, does not have any patients", roomId);
                return Enumerable.Empty<PatientDisplayDto>();
            }
            _logger.LogInformation("returning patients of room {id}", roomId);
            return roomPatients;
        }

        private async Task<bool> IsFull(int roomID)
        {
            var room = await _unitOfWork.Rooms.GetRoomByID(roomID);
            if (room.NumberOfPatients < room.Capacity)
                return false;
            return true;
        }

        
    }
}
