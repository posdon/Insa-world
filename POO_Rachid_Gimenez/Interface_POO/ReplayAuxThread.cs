using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Interface_POO
{
    class ReplayAuxThread
    {

        private volatile bool _shouldStop;
        private ViewModelReplayGame mainView;

        public ReplayAuxThread(ViewModelReplayGame view)
        {
            mainView = view;
        }

        public void DoWork()
        {
            while (!_shouldStop)
            {
                mainView.Next();
                Thread.Sleep(1000);
            }
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }

        public void RequestGo()
        {
            _shouldStop = false;
        }
    }
}
