using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace VisualTimer
{
    internal class Timer
    {
        private Process _process = new Process();
        private Stopwatch _stopWatch = new Stopwatch();
        private TimeSpan _timeSpan;

        private MessageSender _sender = new MessageSender();

		//Here we can add the path to the program we want to track 
		private string _processPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe";
		//Here we can add the path where the data file will be stored
		private string _saveFilePath = @"C:\Users\Vlados\Documents\time1.txt";

        private string _globalTime = "00:00:00";

        private int _hours;
        private int _minutes;
        private int _seconds;

        private bool _exitToken = true;
        public void Start()
        {
            if(!File.Exists(_saveFilePath)) 
                File.WriteAllText(_saveFilePath, _globalTime);

            try
            {
                _process.StartInfo.FileName = _processPath;

                _process.EnableRaisingEvents = true;
                _process.Exited += new EventHandler(ProcessExited);

                Read();
                _process.Start();
                _stopWatch.Start();
                Console.WriteLine("Im here");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ProcessExited(object sender, EventArgs e)
        {            
            Save();
            _stopWatch.Stop();
        }

        private void Save()
        {
            _timeSpan = _stopWatch.Elapsed;

            _hours += _timeSpan.Hours;
            _minutes += _timeSpan.Minutes;
            _seconds += _timeSpan.Seconds;

            _globalTime = String.Format("{0:00}:{1:00}:{2:00}",
                _hours, _minutes, _seconds);

            File.WriteAllText(_saveFilePath, _globalTime);

            _sender.SendMessage(_timeSpan, _globalTime, _stopWatch);
        }

        private void Read()
        {
            _globalTime = File.ReadAllText(_saveFilePath);

            string[] temp = _globalTime.Split(':');

            _hours = int.Parse(temp[0]);
            _minutes = int.Parse(temp[1]);
            _seconds = int.Parse(temp[2]);

            if (int.Parse(temp[2]) >= 60)
            {
                _minutes++;
                _seconds = int.Parse(temp[2]) - 60;
            }

            if (int.Parse(temp[1]) >= 60)
            {
                _hours++;
                _minutes = int.Parse(temp[1]) - 60;
            }
        }

        public void WaitForComplete()
        {
			//This method was created so that the program does not complete its work.
			while (_exitToken)
            {
				//This is in order not to load the processor
				Thread.Sleep(5000);
            }
        }
    }
}
