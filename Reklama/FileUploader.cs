using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Reklama
{
    public class FileUploader
    {
        private readonly HttpPostedFileBase _file;
        public string Name { get; private set; }
        public string UniqueName { get; private set; }
        public string ContentType { get; private set; }
        public int ContentLength { get; private set; }

        public FileUploader(HttpPostedFileBase file)
        {
             _file = file;
            Name = file.FileName;
            ContentType = file.ContentType;
            ContentLength = file.ContentLength;
            GenerateUniqueFilename();
        }


        protected void GenerateUniqueFilename()
        {
            string[] extensions = Name.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string extension = extensions.ElementAt(extensions.Count() - 1);
            UniqueName = Guid.NewGuid().ToString() + '.' + extension;
        }


        public virtual void Save(string keypath)
        {
            var path = ProjectConfiguration.Get.FilePaths["base"] + ProjectConfiguration.Get.FilePaths[keypath];
            _file.SaveAs(Path.Combine(path, UniqueName));
        }


        /// <summary>
        /// Удаление файла из ФС
        /// </summary>
        /// <param name="keypath">ключ словаря каталога</param>
        /// <param name="filename">имя удаляемого файла</param>
        public static void Delete(string keypath, string filename)
        {
            if (filename != string.Empty)
            {
                var path = Path.Combine(ProjectConfiguration.Get.FilePaths["base"], ProjectConfiguration.Get.FilePaths[keypath],
                                    filename);
                File.Delete(path);
            }
        }
    }
}