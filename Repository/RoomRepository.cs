
namespace Hospital_Management_System.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       

        public async Task AddRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }


        public List<Room> GetAllRooms()
        {
            var rooms = _context.Rooms
                .AsNoTracking()
                .ToList();
            return rooms;
        }

        public List<Room> GetDepartmentRooms(string departmentName)
        {
            var rooms = _context.Rooms
                .AsNoTracking()
                .Where(r => r.DepartmentName == departmentName)
                .ToList();
            return rooms;
        }

        public async Task<Room> GetRoomByID(int roomID)
        {
            return await _context.Rooms
                .FindAsync(roomID);
        }

        public async Task<decimal> GetRoomCost(int roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            return room.Cost;

        }

        

        public async Task<int> GetRoomIdByPatientId(int patientId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            return (int)patient.RoomId;
        }

        

        public async Task UpdateNumberOfPatients(Room room)
        {
            await _context.Rooms
                .Where(r => r.Id == room.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(r => r.NumberOfPatients, r => room.NumberOfPatients));
        }
    }
}
