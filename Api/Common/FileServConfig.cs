using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Api.Common
{
    public class ReturnFileMapPath
    {
        public int Total { get; set; }
        public List<string> FileId { get; set; }
    }
    public class FileServConfig
    {
        public bool UseRawName { get; set; }
        public bool UseUnknowFiles { get; set; }
        public bool UseDirectoryBrowser { get; set; }

        private string filestore;
        public string FileStore
        {
            get { return filestore; }
            set { filestore = Path.IsPathRooted(value) ? value : Directory.GetCurrentDirectory() + "/" + value; }
        }
        public string FullPath
        {
            get { return "/root"; }
            private set {; }
        }
        /// <summary>
        /// 单次上传允许的最大size 单位为 k
        /// </summary>
        /// <value></value>
        public string LimitSize { get; set; }
        public long MaxSize
        {
            get
            {
                var limit = LimitSize;
                limit.Replace(",", "");
                var tag = limit.ToLower().TakeLast(1).First().ToString();
                switch (tag)
                {
                    case "m":
                        {
                            limit.Replace(tag, "");
                            return long.Parse(limit) * 1024;
                        }
                    case "g":
                        {
                            limit.Replace(tag, "");
                            return long.Parse(limit) * 1024 * 1024;
                        }
                    default: { return long.Parse(limit); }
                }
            }
            private set {; }
        }
        public string ServeMapPath { get; set; }
    }
}
