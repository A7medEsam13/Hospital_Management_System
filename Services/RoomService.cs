
using Hospital_Management_System.Repository;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<RoomService> _logger;

        public RoomService(IRoomRepository roomRepository,
            ILogger<RoomService> logger,
            IPatientRepository patientRepository)
        {
            _roomRepository = roomRepository;
            _logger = logger;
            _patientRepository = patientRepository;
        }

        public async Task<bool> AddPatientToRoom(int roomId, int patientId)
        {
            if(await IsFull(roomId))
            {
                _logger.LogWarning("Room With ID {id} is Full", roomId);
                return false;
            }

            _logger.LogInformation("Getting the patient from database with ID {pID}", patientId);
            var patient = await _patientRepository.GetPatientById(patientId);
            if(patient == null)
            {
                _logger.LogError("patient with ID {id} is not exist", patientId);
                throw new NullReferenceException($"patient with ID {patientId} is not exist");
            }


            _logger.LogInformation("setting the room Id to {Id}", roomId);
            patient.RoomId = roomId;
            _logger.LogInformation("updating and saving the patient details in the database");
            _patientRepository.Updatepatient(patient);


            _logger.LogInformation("Updating the Number of patients of room {id}", roomId);
            var room = await _roomRepository.GetRoomByID(roomId);
            room.NumberOfPatients++;
            await _roomRepository.UpdateNumberOfPatients(room);
            _logger.LogInformation("Saving changes to database");
            await _roomRepository.SaveAsync();

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
            await _roomRepository.AddRoom(room);
            await _roomRepository.SaveAsync();
        }

        public async Task DeletePatientFromRoom(int patientId)
        {
            _logger.LogInformation("Getting the patient from database");
            var patient = await _patientRepository.GetPatientById(patientId);
            if (patient == null)
            {
                _logger.LogError("Patient with ID {id} is not exist", patientId);
                throw new NullReferenceException("Patient is not exist");
            }
            patient.RoomId = 0;
            _logger.LogInformation("updating and saving the patient data");
            _patientRepository.Updatepatient(patient);
            await _roomRepository.SaveAsync();
        }

        public IEnumerable<RoomDisplayDto> GetAllRooms()
        {
            _logger.LogInformation("Getting all rooms from the database");
            var rooms = _roomRepository.GetAllRooms()
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
            var rooms = _roomRepository.GetDepartmentRooms(departmentName)
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
            return await _roomRepository.GetRoomCost(roomId);
        }

        public async Task<int?> GetRoomIdByPatientId(int patientId)
        {
            _logger.LogInformation("getting the room ID of patient {id}", patientId);
            var roomID = await _roomRepository.GetRoomIdByPatientId(patientId);
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
            var patients = _patientRepository.GetAllPatients();
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
            var room = await _roomRepository.GetRoomByID(roomID);
            if (room.NumberOfPatients < room.Capacity)
                return false;
            return true;
        }

        
    }
}
