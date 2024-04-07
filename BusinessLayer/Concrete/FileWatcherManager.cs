using BusinessLayer.Abstract;
using Core.Constatnts;
using DataAccessLayer.Abstract;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace BusinessLayer.Concrete
{
    public class FileWatcherManager : IFileWatcherService
    {
        private readonly IFileWatcherDal _fileWatcherDal;
        private DispatcherTimer _timer;
        public string SelectedFolderPath { get; set; }

        public FileWatcherManager(IFileWatcherDal fileWatcherDal, DispatcherTimer timer)
        {
            _fileWatcherDal = fileWatcherDal;
            _fileWatcherDal.FileChanged += OnFileChanged;

            _timer = timer;
            _timer.Tick += CheckInputFolder;
            _timer.Interval = TimeSpan.FromMilliseconds(5000); 
            _timer.Start();

        }

        public void OnFileChanged(object? sender, string e)
        {
            try
            {
                Parallel.Invoke(() =>
                {
                        Console.WriteLine($"{Messages.FileChanged} {e}");

                       string fileContent = ReadFileContent(e);
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Messages.ErrorOccured} {ex.Message}");
            }
        }
        public void CheckInputFolder(object sender, EventArgs e)
        {
            
            Console.WriteLine( Messages.CheckingInputFolder);
        }

        public event EventHandler<string> FileChanged;

        public void StartWatching(string folderPath)
        {
            _fileWatcherDal.StartWatching(folderPath);
        }

        public void StopWatching()
        {
            _fileWatcherDal.StopWatching();
        }

        private string ReadFileContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            

            Console.WriteLine($"{Messages.FileChanged} {e.FullPath}");

            FileChanged?.Invoke(this, e.FullPath);
        }
        public void SelectFolder()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = Messages.SelectFolder,
                Filter = "Folders|*.dummy",
                FileName = "SelectFolder",
                CheckFileExists = false,
                CheckPathExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                SelectedFolderPath = Path.GetDirectoryName(openFileDialog.FileName);

                if (Directory.Exists(SelectedFolderPath))
                {
                    StartWatching(SelectedFolderPath);
                }
                else
                {
                    MessageBox.Show(Messages.SelectValidFolder, Messages.InvalidFolder, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
            }
        }
    }
}