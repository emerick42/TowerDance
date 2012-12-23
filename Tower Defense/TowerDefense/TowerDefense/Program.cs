using System;

namespace TowerDance
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ControllerManager game = new ControllerManager())
            {
                game.Run();
            }
        }
    }
#endif
}

