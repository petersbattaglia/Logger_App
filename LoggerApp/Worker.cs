using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerApp
{
    public static class Worker
    {
        public delegate void WorkDoneEventHandler(object sender, WorkerEventArgs e);
        public delegate void WorkCompletedEventHandler(object sender, WorkerEventArgs e);

        public static event WorkDoneEventHandler WorkDone;
        public static event WorkCompletedEventHandler WorkCompleted;

        public static void DoWork()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(2000);
                OnWorkDone(new WorkerEventArgs(string.Format("Iteration #{0} Complete.", (i + 1).ToString())));
            }

            OnWorkCompleted(new WorkerEventArgs("All done."));
        }

        public static void OnWorkDone(WorkerEventArgs e)
        {
            if (WorkDone != null)
                WorkDone(null, e);
        }

        public static void OnWorkCompleted(WorkerEventArgs e)
        {
            if (WorkCompleted != null)
                WorkCompleted(null, e);
        }
    }

    public class WorkerEventArgs : EventArgs
    {
        public WorkerEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
