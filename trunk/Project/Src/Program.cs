using System;

namespace PhareAway
{
    class Program
    {
        /// The main entry point for the application.
        static void Main(string[] args)
        {
            //using (PhareAwayGame game = new PhareAwayGame(LevelName.Level_Logos))
            using (PhareAwayGame game = new PhareAwayGame(LevelName.Level_Main))
            {
                game.Run();
            }
           
        }
    }
}
