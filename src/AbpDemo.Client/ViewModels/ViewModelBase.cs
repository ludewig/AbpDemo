using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbpDemo.Client
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Methods
        //文件选择对话框
        public string ShowFileDialog(string title, string filter = "docx|*.docx", string folder = "C:\\Users\\Administrator\\Desktop")
        {
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = title;
            dialog.Filter = filter;
            dialog.InitialDirectory = folder;
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }
            return filePath;

        }

        #endregion
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
