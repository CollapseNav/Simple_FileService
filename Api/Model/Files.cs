using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class File
    {
        [Key]
        public Guid? Id { get; set; }
        [MaxLength(100)]
        public string FileName { get; set; }
        public string MapPath { get; set; }
        [MaxLength(20)]
        public string Ext { get; set; }
        [MaxLength(20)]
        public string Sys { get; set; }
        [MaxLength(50)]
        public string Size { get; set; }
        public DateTime? AddTime { get; set; }
    }
}