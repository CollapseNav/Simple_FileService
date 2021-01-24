using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Api.Model
{
    public class File : BaseFile
    {
        [MaxLength(20)]
        public string Ext { get; set; }
        public Guid? TypeId { get; set; }
        public virtual FileType FileType { get; set; }
        public string ContentType { get; set; }
        [ForeignKey("ParentId"), JsonIgnore]
        public virtual Dir Parent { get; set; }
        public override string MapPath { get; set; }

        public File Init(IFormFile file)
        {
            ContentType = file.ContentType;
            Ext = Path.GetExtension(file.FileName);
            MapPath += "/" + file.FileName;
            Size = file.Length.ToString();
            FileName = file.FileName;

            base.Init();
            return this;
        }
    }
}
