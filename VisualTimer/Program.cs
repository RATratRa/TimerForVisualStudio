namespace VisualTimer
{
    //      TODO
    // Сейф в облаці 
    // Коли був перший запуск mb
    // Придумати як забезпечити файлик від змін
    //
    // 
    // 
    // 
    //
    internal class Program
    {
        static void Main()
        {
            Timer timer = new Timer();

            timer.Start();

            timer.WaitForComplete();
        }
    }
}
