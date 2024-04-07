using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using static System.Net.WebRequestMethods;

namespace DataAccessLayer.Concrete
{
    public class FileWatcherDal : IFileWatcherDal
    {
       
        public event EventHandler<string> FileChanged;

        public void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            FileChanged?.Invoke(this, e.FullPath);

        }

        public void StartWatching(string folderPath)
        {
            FileSystemWatcher fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = folderPath;

            fileWatcher.Created += OnFileChanged;
            fileWatcher.Changed += OnFileChanged;
            fileWatcher.Deleted += OnFileChanged;
            fileWatcher.Renamed += OnFileChanged;

            fileWatcher.EnableRaisingEvents = true;

        }

        public void StopWatching()
        {
            FileSystemWatcher fileWatcher = new FileSystemWatcher();
            fileWatcher.EnableRaisingEvents = false;
            fileWatcher.Dispose();
        }

    }
}
