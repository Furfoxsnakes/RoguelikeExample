using GoRogue.MapGeneration;
using GoRogue.MapGeneration.Steps;
using Roguelike.Screens;
using SadConsole;

namespace Roguelike
{
    class Game
    {
        public const int Width = 100;
        public const int Height = 70;

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create("Fonts/Buddy.font", Width, Height);
            Settings.ResizeMode = Settings.WindowResizeOptions.Stretch;

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;
                        
            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            var generator = new Generator(Width, Height);
            generator.AddSteps(new RectangleGenerator());
            generator.Generate();

            Global.CurrentScreen = new DungeonScreen();
        }
    }
}