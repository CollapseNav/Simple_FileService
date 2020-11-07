using System;
using System.Collections.Generic;
using System.IO;

namespace Api.Common
{
    public class ReturnFileMapPath
    {
        public int Total { get; set; }
        public List<string> FileId { get; set; }
    }
    public class FileServConfig
    {
        private string FilePath;
        public bool? IsAuto { get; private set; }
        public bool UseRawName { get; set; }
        public bool UseUnknowFiles { get; set; }
        public bool UseDirectoryBrowser { get; set; }
        public bool CheckAuto()
        {
            if (!IsAuto.HasValue && !string.IsNullOrEmpty(FrontExt))
                IsAuto = false;
            else
                IsAuto = true;
            return IsAuto.Value;
        }
        public string FrontExt { get; set; }
        public string FileStore
        {
            get
            {
                if (!string.IsNullOrEmpty(FilePath))
                {
                    // 根据绝对路径和相对路径 定位到文件夹映射路径
                    var filePath = FilePath.StartsWith("./") ? Directory.GetCurrentDirectory() + "/" + FilePath.Replace("./", "") : FilePath;
                    var now = DateTime.Now;
                    if (IsAuto.HasValue && IsAuto.Value)
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
        private string fullPath;
        public string FullPath
        {
            get { return FileStore + FrontExt; }
            set { fullPath = value; }
        }
        /// <summary>
        /// 单次上传允许的最大size 单位为 k
        /// </summary>
        /// <value></value>
        public long MaxSize { get; set; }
        public string ServeMapPath { get; set; }
    }
}
