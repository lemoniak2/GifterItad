using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace Gifter.DataOperator
{
    class Dialog
    {
        public string OpenDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV (.csv)|*.csv";
            string result = "";
            if (openFileDialog.ShowDialog() == true)
            {
                result = openFileDialog.FileName;
            }
            return result;
        }
        public string OpenDialog2()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Obrazki (.jpg)|*.jpg|Obrazki (.png)|*.png";
            string result = "";
            if (openFileDialog.ShowDialog() == true)
            {
                result = openFileDialog.FileName;
            }
            return result;
        }
        public string CopyFileName(string imageUrl)
        {
            string newString = Path.GetFileName(imageUrl); //Guid.NewGuid.ToString()
            File.Copy(imageUrl, String.Format(@".\Images\{0}", newString), true);
            return newString;
        }
    }
}
