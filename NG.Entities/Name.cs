using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NG.Entities
{
    public class Name
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Record { get; set; }

        public decimal Popularity { get; set; }

        public int NameTypeId { get; set; }

        [Required]
        public NameType Type { get; set; }
    }
}
