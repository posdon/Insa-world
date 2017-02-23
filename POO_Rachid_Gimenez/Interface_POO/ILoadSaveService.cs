using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace Interface_POO
{
    sealed class ILoadSaveService
    {
        
        public String OpenFile(string defaultExtension, string filter)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = defaultExtension;
            fd.Filter = filter;

            bool? result = fd.ShowDialog();

            return result.Value ? fd.FileName : null;
        }

        public String SaveFile(string defaultExtension, string filter)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.DefaultExt = defaultExtension;
            fd.Filter = filter;

            bool? result = fd.ShowDialog();

            return result.Value ? fd.FileName : null;
        }

    }
}
