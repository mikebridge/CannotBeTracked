using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo
{
    public class Message
    {
        [Key]
        public virtual Guid Id { get; set; }

        public virtual String Body { get; set; }

        public virtual ICollection<MessageTag> Tags { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public virtual DateTime DateCreated { get; set; }
    }
}
