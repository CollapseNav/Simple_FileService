using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api.Model
{
    public class Dir : BaseFile
    {
        [ForeignKey("ParentId"), JsonIgnore]
        public virtual Dir Parent { get; set; }
        public virtual List<File> Files { get; set; }
        public virtual List<Dir> Dirs { get; set; }
    }
}
