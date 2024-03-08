namespace VisualTimer
{
	internal class Program
	{
		static void Main()
		{
			//Here we can add the path to the program we want to track 
			string processPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe";
			//Here we can add the path where the data file will be stored
			string saveFilePath = @"C:\Users\Vlados\Documents\time.txt";

			Timer timer = new Timer(processPath, saveFilePath);

			timer.Start();

			timer.WaitForComplete();
		}
	}
}
