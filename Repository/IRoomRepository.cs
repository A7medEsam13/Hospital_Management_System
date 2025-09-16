namespace Hospital_Management_System.Repository
{
    public interface IRoomRepository
    {
        public Task AddRoom(Room room);
        public List<Room> GetAllRooms();
        public List<Room> GetDepartmentRooms(string departmentName);
        public Task<int> GetRoomIdByPatientId(int patientId);
        public Task<decimal> GetRoomCost(int roomId);
        public Task<Room> GetRoomByID(int roomID);
        public Task UpdateNumberOfPatients(Room room);
    }
}
