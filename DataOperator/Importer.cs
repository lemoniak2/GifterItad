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
        public Importer(string path)
        {
            _path = path;
        }
        public string GetPersonById(int id)
        {
            string result = "";
            foreach (string line in File.ReadAllLines(_path)[id].Split(new char[] { ';' }))
            {
                result += line + "\n";
            }
            return result;
        }
        public int GetLenght()
        {
            return File.ReadAllLines(_path).Length;
        }
    }
}
