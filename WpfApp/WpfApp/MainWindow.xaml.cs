using System;
using System.Collections.Generic;
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
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int poziom=1;
        string poziomStr="1";
        int iloPrzeciw=0;
        string iloPrzeciwStr = "0";
        double maxPrzeciw = 1;
        double maxPrzeciwCopy = 1; //specjalna zmienna dla progressbaru
        double d = 0.0;
        int i;
        DirectoryInfo datadir = new DirectoryInfo(@"C:\Users\MajuMaster\Desktop\gra sfml i aplikacja od lvl\gameSFML\Project1\Project1\test.txt");
        string textfileLevel = @"C:\Users\MajuMaster\Desktop\gra sfml i aplikacja od lvl\gameSFML\Project1\Project1\test.txt";
        DirectoryInfo datadir2 = new DirectoryInfo(@"C:\Users\MajuMaster\Desktop\gra sfml i aplikacja od lvl\gameSFML\Project1\Project1\iloscPrzeciwnikow.txt");
        string textfileIloPrzeciw = @"C:\Users\MajuMaster\Desktop\gra sfml i aplikacja od lvl\gameSFML\Project1\Project1\iloscPrzeciwnikow.txt";
        

        public MainWindow()
        {
            InitializeComponent();
            //ProgressBarProgLevelu.Maximum = 1;
            
            /*
            ProgressBarProgLevelu.Value = iloPrzeciw;
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
            */
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            File.WriteAllText(textfileLevel, poziomStr);
            MessageBox.Show("zatwierdzony level");
            //file.WriteLine(text);
            // file.Write(poziom);
            maxPrzeciwCopy = maxPrzeciw;
            ProgressBarProgLevelu.Maximum = maxPrzeciw;

            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork +=worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
            
            
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            iloPrzeciwStr=File.ReadAllText(textfileIloPrzeciw);
            iloPrzeciw = Convert.ToInt16(iloPrzeciwStr);

            ProgressBarProgLevelu.Value = iloPrzeciw;

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
         {


            do
            {
                if (iloPrzeciw != 0)
                {
                    d = (iloPrzeciw / maxPrzeciwCopy) * 100;
                    i = Convert.ToInt16(d);
                }
                else
                {
                    i = 0;
                }
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(100);
            } while (i < 100);



            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("ukończono level");
            ProgressBarProgLevelu.Value = 0;
        }
        /// <summary>
        /// /////////////////////////////////////////
        /// </summary>
        
        private void ButtonNastepny_Click(object sender, RoutedEventArgs e)
        {
            poziom += 1;
            maxPrzeciw =Convert.ToDouble(poziom);
            maxPrzeciw = Math.Pow(maxPrzeciw, 2);
            poziomStr = poziom.ToString();
            label2.Content = poziomStr;
        }

        private void ButtonPoprzedni_Click(object sender, RoutedEventArgs e)
        {
            if (poziom == 1)
            {
                maxPrzeciw = 1.0;
                MessageBox.Show("level 1 jest najnizszy");
            }
            else
            {
                poziom -= 1;
                maxPrzeciw = Convert.ToDouble(poziom);
                maxPrzeciw = Math.Pow(maxPrzeciw, 2);
                poziomStr =poziom.ToString();
                label2.Content = poziomStr;
            }
            
            
        }
    }
}
