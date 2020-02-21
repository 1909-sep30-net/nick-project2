namespace KitchenRestService.Api.Models
{
    public class ApiUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool Admin { get; set; }
    }
}
