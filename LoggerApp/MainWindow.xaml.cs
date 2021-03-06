﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace LoggerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> _Log = new ObservableCollection<string>();

        public ObservableCollection<string> Log
        {
            get
            {
                return _Log;
            }
        }

        private Action<string> addtoLogAction;
        
        public MainWindow()
        {
            InitializeComponent();

            Worker.WorkDone += Worker_WorkDone;
            Worker.WorkCompleted += Worker_WorkCompleted;

            addtoLogAction = AddToLog;
        }

        void Worker_WorkCompleted(object sender, WorkerEventArgs e)
        {
            MessageBox.Show("Work Completed.");
        }

        void Worker_WorkDone(object sender, WorkerEventArgs e)
        {
            Dispatcher.BeginInvoke(addtoLogAction, e.Message);
        }

        private void AddToLog(string message)
        {
            Log.Add(message);

            // Maintain position at the bottom of the listbox
            lbLogger.ScrollIntoView(lbLogger.Items[lbLogger.Items.Count - 1]);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() => { Worker.DoWork(); }).Start();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if(lbLogger.SelectedItems.Count > 0)
            {
                Clipboard.SetText(string.Join(Environment.NewLine, lbLogger.SelectedItems.Cast<string>()));
            }
        }
    }
}
