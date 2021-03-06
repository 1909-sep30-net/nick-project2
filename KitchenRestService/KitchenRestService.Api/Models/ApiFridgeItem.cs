﻿using System;
using System.ComponentModel.DataAnnotations;

namespace KitchenRestService.Api.Models
{
    public class ApiFridgeItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Expiration { get; set; }
        public int OwnerId { get; set; }
    }
}
