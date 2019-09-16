using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NG.Entities
{
    public class NameType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Type { get; set; }

        public IEnumerable<Name> Names { get; set; } = new List<Name>();
    }
}
