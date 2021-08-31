using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AKTest.Data
{
    public class EntrantEntity
    {
        [Key]
        public int? id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

    }
}
