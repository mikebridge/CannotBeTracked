using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class AddOrUpdateMessageViewModel
    {
        public Guid? Id { get; set; }

        public String Body { get; set; }

        public String[] Tags { get; set; }
    }
}
