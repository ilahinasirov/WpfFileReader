using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IFileWatcherDal
    {
        event EventHandler<string> FileChanged;

        void StartWatching(string folderPath);
        void StopWatching();
        void OnFileChanged(object sender, FileSystemEventArgs e);
        
    }
}
