using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly IRoomService _roomService;

        public RoomsController(ILogger<RoomsController> logger,
            IRoomService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpPost("New-Room")]
        public async Task<IActionResult> AddNewRoom(RoomCreationDto roomDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model State Is Not Valid!");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Adding the room");
            await _roomService.AddRoom(roomDTO);
            return Created();
        }


        [HttpPost]
        public async Task<IActionResult> AddPatientToRoom(int patientID, int roomID)
        {
            if (patientID <= 0 || roomID <= 0)
            {
                _logger.LogError("Patient ID or Room ID is wrong");
                return BadRequest("Patient ID or Room ID is wrong");
            }

            _logger.LogInformation("Adding Patient {patientid} to Room {roomId}", patientID, roomID);
            var IsAdded = await _roomService.AddPatientToRoom(roomID, patientID);

            if (IsAdded)
            {
                _logger.LogInformation("Patient Has Been Added Successfully");
                return Ok("Patient has been added successfully");
            }
            _logger.LogWarning("Room {id} is full", roomID);
            return BadRequest("Room Is Full");
        }

        [HttpPut]
        public async Task<IActionResult> DeletePatientFromRoom(int patientID)
        {
            if (patientID <= 0)
            {
                _logger.LogError("Patient ID is wrong");
                return BadRequest("Patient ID is wrong");
            }

            _logger.LogInformation("Deleting Patient {id} From his room", patientID);
            await _roomService.DeletePatientFromRoom(patientID);
            return Ok($"Patient {patientID} has been deleted successfully");
        }

        [HttpGet("All-Rooms")]
        public IActionResult GetAllRooms()
        {
            _logger.LogInformation("Getting all rooms from the service");
            var rooms = _roomService.GetAllRooms();
            return Ok(rooms);
        }

        [HttpGet("Department-Rooms")]
        public IActionResult GetDepartmentRooms(string departmentName)
        {
            if (departmentName.Length == 0)
            {
                _logger.LogError("Invalid Department Name");
                return BadRequest("Invalid Department Name");
            }

            _logger.LogInformation("Getting {deptName} Department Rooms", departmentName);
            var rooms = _roomService.GetDepartmentRooms(departmentName);
            return Ok(rooms);
        }

        [HttpGet("{roomId:int}")]
        public IActionResult GetRoomPatients(int roomID)
        {
            if (roomID <= 0)
            {
                _logger.LogError("Invalid Room ID");
                return BadRequest("Invalid Room ID");
            }

            _logger.LogInformation("Getting All Patients of Room {id}", roomID);
            var patients = _roomService.GetRoomPatients(roomID);
            return Ok(patients);
        }

        [HttpGet("Get-patient-room")]
        public async Task<IActionResult> GetPatientRoomID(int patientID)
        {
            if (patientID <= 0)
            {
                _logger.LogError("Invalid Patient ID");
                return BadRequest("Inavlid Patient ID");
            }

            _logger.LogInformation("Getting the Room ID of patient {id}", patientID);
            var roomId = await _roomService.GetRoomIdByPatientId(patientID);

            if (roomId == null)
            {
                _logger.LogWarning("Patient {id} Does not have Room", patientID);
                return Content($"Patient {patientID} Does Not have Room");
            }

            return Ok(roomId);
        }

        [HttpGet("cost")]
        public async Task<IActionResult> GetRoomCost(int roomID)
        {
            if (roomID <= 0)
            {
                _logger.LogError("Invalid Room ID");
                return BadRequest("Invalid Room ID");
            }

            _logger.LogInformation("Getting the Cost of the room {id}", roomID);
            var cost = await _roomService.GetRoomCost(roomID);
            return Ok(cost);
        }
    }
}
