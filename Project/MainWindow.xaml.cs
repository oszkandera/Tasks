using MAD2_Tasks.General.Algorithms;
using MAD2_Tasks.General.Code;
using MAD2_Tasks.General.Code.Loaders;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFileClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            var isDialogClosed = fileDialog.ShowDialog();

            if (!isDialogClosed.HasValue || !isDialogClosed.Value)
            {
                //TODO: show error dialog
            }

            var path = fileDialog.FileName;

            PathToGraphFile.Text = path;
        }

        private void GeneratSampleButton_Click(object sender, RoutedEventArgs e)
        {
            var path = PathToGraphFile.Text;

            int requiredSampleSize;
            try
            {
                requiredSampleSize = int.Parse(RequiredSampleSize.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("Zadaná velikost vzorku je neplatná. Je nutné zadat celé číslo.");
                return;
            }

            if (String.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Je nutné nejprve cestu ke grafu.");
                return;
            }

            try
            {
                var adjacencyLoader = new AdjacencyListGraphLoader();            
                var originalGraph = adjacencyLoader.Load(path);

                var forestFireSampling = new ForestFireSampling();
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var sampleGraph = forestFireSampling.GenerateSample(originalGraph, requiredSampleSize);
                stopWatch.Stop();

                var detailWindow = new DetailWindow(originalGraph, sampleGraph, stopWatch.Elapsed);
                detailWindow.Show();
            }
            catch(Exception)
            {
                MessageBox.Show("Při generováni vzorku došlo k chybě.");
            }

        }
    }
}
