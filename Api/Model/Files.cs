using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class File
    {
        [Key]
        public Guid? Id { get; set; }
        public string FileName { get; set; }
        public string MapPath { get; set; }
        public string Ext { get; set; }
        public string Sys { get; set; }
        public DateTime? AddTime { get; set; }
    }
}