using System;
using System.Collections.Generic;
using System.IO;

namespace Api.Common
{
    public class ReturnFileMapPath
    {
        public int Total { get; set; }
        public List<Guid> FileId { get; set; }
    }
    public class FileServConfig
    {
        private string FilePath;
        public string FrontExt { get; set; }
        public string FileStore
        {
            get
            {
                if (!string.IsNullOrEmpty(FilePath))
                {
                    var filePath = FilePath.StartsWith("./") ? Directory.GetCurrentDirectory() + "/" + FilePath.Replace("./", "") : FilePath;
                    var now = DateTime.Now;
                    FrontExt = "/" + now.Year.ToString() + now.Month + now.Day;
                    if (!Directory.Exists(filePath + FrontExt) && !filePath.EndsWith(FrontExt))
                    {
                        Directory.CreateDirectory(filePath + FrontExt);
                    }
                    return filePath;
                }
                else return "";
            }
            set { FilePath = value; }
        }
        public string FullPath { get { return FileStore + FrontExt; } private set { } }
        public long MaxSize { get; set; }
        public string FileServPath { get; set; }
    }
}
