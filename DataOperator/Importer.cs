using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gifter.DataOperator
{
    public class Importer
    {
        private string _path;
        public Importer()
        {

        }
        public Importer(string path) : this()
        {
            _path = path;
        }
        public string GetPersonById(int id)
        {
            return String.Join(Environment.NewLine, File.ReadAllLines(_path)[id].Split(new char[] { ';' }));
        }
        public int GetLenght()
        {
            return File.ReadAllLines(_path).Length;
        }
        public string CopyFileName(string imageUrl,string currentProjectPath)
        {
            var tocopy = Path.Combine(currentProjectPath, Properties.Settings.Default.ImagesPath);
            var tmp = Path.GetFullPath(imageUrl);
            var tmp2 = Path.GetFullPath(tocopy) + Path.GetFileName(tmp);
            File.Copy(tmp, tmp2, true);
            return Properties.Settings.Default.ImagesPath + Path.GetFileName(tmp);
        }
    }
}
