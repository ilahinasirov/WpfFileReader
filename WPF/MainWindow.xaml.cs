using BusinessLayer.Abstract;
using Core.Constatnts;
using Core.Entities;
using Core.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private readonly IFileWatcherService _fileWatcherService;
        private readonly IDataImporterService _dataImporterService;

        public MainWindow(IFileWatcherService fileWatcherService, IDataImporterService dataImporterService)
        {
            InitializeComponent();
            _fileWatcherService = fileWatcherService;
            _dataImporterService = dataImporterService;
            

            _fileWatcherService.FileChanged += OnFileChanged;

            _fileWatcherService.SelectFolder();


        }

        public MainWindow()
        {
                InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            await Task.Run(() =>
            {
                string filePath= _fileWatcherService.SelectedFolderPath;


                Dispatcher.Invoke(() => OnFileChanged(this, filePath));
            });
        }



        private async void OnFileChanged(object sender, string e)
        {
            FileType fileType = _dataImporterService.DetermineFileType(e);
            List<TradeData> importedData = await _dataImporterService.ImportDataFromFileAsync(e, fileType);

            Dispatcher.Invoke(() =>
            {
                dtGridView.ItemsSource = importedData;
                MessageBox.Show($"{Messages.FileChanged} {e}");
            });
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _fileWatcherService.StopWatching();
        }
    }
}
