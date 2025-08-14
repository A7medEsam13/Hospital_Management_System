using System.Collections.Generic;
namespace Hospital_Management_System.Repository
{
    public class EmergencyContactRepository : IEmergencyContactRepository
    {
        private readonly ApplicationDbContext _context;

        public EmergencyContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmergencyContact emergencyContact)
        {
            await _context.EmergencyContacts.AddAsync(emergencyContact);
        }

        public void Delete(EmergencyContact emergencyContact)
        {
            _context.EmergencyContacts.Remove(emergencyContact);
        }

        public void DeleteAllPatientEmergencyContacts(IEnumerable<EmergencyContact> emergencyContacts)
        {
            foreach (var Contact in emergencyContacts)
            {
                Delete(Contact);
            }
        }

        public IEnumerable<EmergencyContactDisplayDto> GetAllPatientEmergencyContacts(int patientId)
        {

            var emergencyContacts = _context.EmergencyContacts
                .AsNoTracking()
                .Where(ec => ec.PatientId == patientId)
                .Include(ec => ec.Patient)
                .Select(ec => new EmergencyContactDisplayDto
                {
                    Id = ec.Id,
                    FirstName = ec.FirstName,
                    LastName = ec.LastName,
                    PhoneNumber = ec.PhoneNumber,
                    Relation = ec.Relation,
                    PatientId = ec.PatientId,
                    PatientName = ec.Patient.FirstName + ' ' + ec.Patient.LastName
                });

            return emergencyContacts;
        }  

        public async Task<EmergencyContactDisplayDto> GetByIdAsync(int id)
        {
            return await _context.EmergencyContacts
                .Where(ec => ec.Id == id)
                .Include(ec => ec.Patient)
                .Select(ec=> new EmergencyContactDisplayDto
                {
                    Id = ec.Id,
                    FirstName = ec.FirstName,
                    LastName = ec.LastName,
                    PhoneNumber = ec.PhoneNumber,
                    Relation = ec.Relation,
                    PatientId = ec.PatientId,
                    PatientName = ec.Patient.FirstName+' '+ec.Patient.LastName
                })
                .FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
