namespace Hospital_Management_System.Services
{
    public interface IRoomService
    {
        public Task AddRoom(RoomCreationDto room);
        public Task<bool> AddPatientToRoom(int roomId, int patientId);
        public Task DeletePatientFromRoom(int patientId);
        public IEnumerable<RoomDisplayDto> GetAllRooms();
        public IEnumerable<RoomDisplayDto> GetDepartmentRooms(string departmentName);
        public IEnumerable<PatientDisplayDto> GetRoomPatients(int roomId);
        public Task<int?> GetRoomIdByPatientId(int patientId);
        public Task<decimal> GetRoomCost(int roomId);
    }
}
