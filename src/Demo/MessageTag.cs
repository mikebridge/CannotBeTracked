using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo
{
    public class MessageTag
    {
        public Guid MessageId { get; set; }

        public Message Message { get; set; }

        public String Tag { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateCreated { get; set; }
    }
}
