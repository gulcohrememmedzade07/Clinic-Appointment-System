using System.ComponentModel.DataAnnotations;

namespace Clinic_Appointment_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pasiyentin adı və soyadı daxil edilməlidir")]
        [StringLength(120)]
        public string PatientName { get; set; } = "";

        [Required(ErrorMessage = "Şikayət və ya qeyd daxil edilməlidir")]
        public string Notes { get; set; } = "";

        [Required(ErrorMessage = "Şöbə seçilməlidir")]
        public string Department { get; set; } = "";

        [Required(ErrorMessage = "Telefon nömrəsi daxil edilməlidir")]
        public string Phone { get; set; } = "";

        public string Status { get; set; } = "Gözləmədə";

        [Required(ErrorMessage = "Qəbul tarixi seçilməlidir")]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
    }
}