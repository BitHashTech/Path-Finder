using System;

namespace Find_The_Path
{
#if WINDOWS || XBOX
    static class program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FindThePathGame game = new FindThePathGame())
            {
                game.Run();
            }
        }
    }
#endif
}

