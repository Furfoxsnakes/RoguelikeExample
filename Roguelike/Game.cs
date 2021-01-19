using GoRogue.MapGeneration;
using GoRogue.MapGeneration.Steps;
using Roguelike.Cartography;
using Roguelike.Entities;
using Roguelike.Screens;
using SadConsole;
using SadRogue.Primitives;

namespace Roguelike
{
    class Game
    {
        public const int Width = 100;
        public const int Height = 70;
        public static DungeonMap DungeonMap;
        public static Player Player;

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height, "Fonts/Buddy.font");
            Settings.ResizeMode = Settings.WindowResizeOptions.Stretch;

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.Instance.OnStart = Init;
                        
            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            var generator = new Generator(Width, Height);
            generator
                .AddSteps(DefaultAlgorithms.RectangleMapSteps())
                // .AddStep(new TranslateToDungeonMapStep())
                .Generate();

            // var map = generator.Context.GetFirst<DungeonMap>();

            GameHost.Instance.Screen = new DungeonScreen(generator);
        }
    }
}