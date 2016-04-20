
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MCHDAL;
using ScpModel;
using System.Data;

namespace Scpmch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _started = false;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {

            tb_log.Clear();
        }

        private void tb_SaveLocation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_SaveLocation.Text = f.SelectedPath;
            }
            else
            {
                tb_SaveLocation.Text = "";
            }

        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            int port = int.Parse(tb_Port.Text);
            if (!_started)
            {
                _started = true;
                btn_start.Content = "停止监听";
                var server = new DicomServer<PacsServer>(port);
                Console.ReadLine();

            }
            else
            {
                _started = false;
                btn_start.Content = "开始监听";
            }

        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {
         
            try
            {
                Methods m = new Methods();
                Study s = new Study();
                s.StudyDate = "hehe";


                m.Save<Study>(s,"tb_study");
                


            }
            catch (MySqlException ex)

            {

                System.Windows.Forms.MessageBox.Show("Error connecting to the server: " + ex.Message);

            }

        }


    }
       


}
