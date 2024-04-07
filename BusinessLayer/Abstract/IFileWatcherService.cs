using System;
using System.IO;

namespace BusinessLayer.Abstract
{
    public interface IFileWatcherService
    {
        event EventHandler<string> FileChanged;
        public string SelectedFolderPath { get;  set; }
        void StartWatching(string folderPath);
        void StopWatching();
        void OnFileChanged(object sender, FileSystemEventArgs e);
        void SelectFolder();
        void CheckInputFolder(object sender, EventArgs e);
    }
}