namespace MvcCrud.Models
{
    public class EditEmployeeViewmodel
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string lName { get; set; }
        public string? Email { get; set; }
        public long? Phone { get; set; }
        public DateTime DOB { get; set; }

    }
}
