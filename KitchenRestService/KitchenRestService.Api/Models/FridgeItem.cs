using System;
using System.ComponentModel.DataAnnotations;

namespace KitchenRestService.Api.Models
{
    public class FridgeItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Expiration { get; set; }
    }
}
