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

        public string CreateDialog(string Filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Filter;
            string result = "";
            if (openFileDialog.ShowDialog() == true)
            {
                result = openFileDialog.FileName;
            }
            return result;
        }

        public string OpenCSV()
        {
            return CreateDialog(Properties.Settings.Default.CSVFilter);
        }

        public string OpenImage()
        {
            return CreateDialog(Properties.Settings.Default.ImageFilter);
        }
        
    }
}
