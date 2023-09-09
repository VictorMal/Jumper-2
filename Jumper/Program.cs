using System;

namespace Jumper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (JumperGame game = new JumperGame())
            {
                game.Run();
            }
        }
    }
}

