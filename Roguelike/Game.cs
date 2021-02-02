using GoRogue.MapGeneration;
using Roguelike.Entities;
using Roguelike.Screens;
using SadConsole;
using SadRogue.Primitives;

namespace Roguelike
{
    class Game
    {
        public const int Width = 128;
        public const int Height = 72;
        public static Player Player;
        public static DungeonScreen DungeonScreen;

        public static Point MapRenderSize = (68, 61);
        public static Point MessageLogSize = (Width, 11);
        public static Point InventorySize = (30, Height - MessageLogSize.Y);
        public static Point PlayerStatSize = (30, Height - MessageLogSize.Y);

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height, "Fonts/Buddy.font");
            Settings.ResizeMode = Settings.WindowResizeOptions.Fit;

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.Instance.OnStart = Init;
                        
            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            var generator = new Generator(100, 100);
            generator
                .AddSteps(DefaultAlgorithms.BasicRandomRoomsMapSteps(roomMinSize:7, roomMaxSize: 12, minRooms:10))
                // .AddSteps(DefaultAlgorithms.CellularAutomataGenerationSteps(fillProbability: 80))
                // .AddStep(new TranslateToDungeonMapStep())
                .Generate();

            // var map = generator.Context.GetFirst<DungeonMap>();

            DungeonScreen = new DungeonScreen(generator);
            GameHost.Instance.Screen = DungeonScreen;
        }
    }
}