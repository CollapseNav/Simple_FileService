using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api.Model
{
    public class BaseFile : BaseEntity
    {
        [MaxLength(100)]
        public string FileName { get; set; }
        public virtual string MapPath { get; set; }
        [MaxLength(20)]
        public string Sys { get; set; }
        [MaxLength(50)]
        public string Size { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsVisible { get; set; } = true;
    }
}
