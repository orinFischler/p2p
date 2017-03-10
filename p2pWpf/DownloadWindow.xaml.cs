﻿using p2p.Entities.File;
using p2pWpf.p2pService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace p2pWpf
{
    /// <summary>
    /// Interaction logic for DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public MainWindow Parent { get; set; }
        public DownloadWindow()
        {
            InitializeComponent();

        }

        private void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultsListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select file");
                return;
            }


            var fr = (FileInfoDTO)resultsListBox.SelectedItem;
            FileRequestDTO request = new FileRequestDTO()
            {
                FileName = fr.FileName,
                FileType = fr.FileType,
                UserName = UserName,
                Password = Password,
                //Id = f.Id

            };

            using (Service1Client client = new Service1Client())
            {

                var result1 = client.downloadRequest(request);
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            FileSearchDTO file = new FileSearchDTO()
            {
                UserName = UserName,
                Password = Password,
                SearchText = fileNameTb.Text
            };

            using (Service1Client client = new Service1Client())
            {
                var result = client.searchFiles(p2p.Utils.XmlFormatter.GetXMLFromObject(file));

                if (result.SearchResult == "OK" && result.Files.Count() > 0)
                {
                    foreach (var item in result.Files)
                    {
                        resultsListBox.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("File Not Found");
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Parent.Show();
        }

       
    }
}
