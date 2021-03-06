﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactUsService.Models
{
    public class CustomerMessage
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(2500)]
        public string Text { get; set; }

        [ForeignKey("CustomerEmail")]
        public string CustomerEmail { get { return Customer.Email; } }

        [Required]
        public DateTime ReceivedOn { get; set; }

        public virtual Customer Customer { get; set; }
    }
}