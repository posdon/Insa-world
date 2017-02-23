using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Interface_POO
{
    public class ViewModelFileDialog : ViewModelBase
    {
        public ViewModelFileDialog()
        {
            this.SaveCommand = new RelayCommand(this.SaveFile);
            this.OpenCommand = new RelayCommand(this.OpenFile);
        }

        #region Properties


        public String SelectedPath
        {
            get;
            set;
        }

        public string Extension
        {
            get;
            set;
        }

        public string Filter
        {
            get;
            set;
        }
        public ICommand OpenCommand
        {
            get;
            set;
        }

        public ICommand SaveCommand
        {
            get;
            set;
        }

        #endregion

        private void OpenFile()
        {
            ILoadSaveService fileServices = new ILoadSaveService();
            this.SelectedPath = fileServices.OpenFile(this.Extension, this.Filter);
        }

        private void SaveFile()
        {
            ILoadSaveService fileServices = new ILoadSaveService();
            this.SelectedPath = fileServices.SaveFile(this.Extension, this.Filter);
        }
    }
}
