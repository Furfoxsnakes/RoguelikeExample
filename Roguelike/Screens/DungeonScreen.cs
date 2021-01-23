using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using Roguelike.Cartography;
using Roguelike.Entities;
using Roguelike.Systems;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Host;
using SadConsole.Input;
using SadConsole.Renderers;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace Roguelike.Screens
{
    public class DungeonScreen : ScreenObject
    {
        private ScreenSurface _mapRenderer;
        private Console _messageConsole;
        private Console _statConsole;
        private Console _inventoryConsole;
        
        public DungeonMap Map { get; }
        public bool PlayerDidAct;
        public CommandSystem CommandSystem { get; private set; }

        public DungeonScreen(Generator mapGen)
        {
            CommandSystem = new CommandSystem();
            
            Map = new DungeonMap(mapGen.Context.Width, mapGen.Context.Height);
            Game.DungeonMap = Map;
            _mapRenderer = Map.CreateRender((100,70));
            Children.Add(_mapRenderer);
            
            GenerateMap(mapGen);

            IsFocused = true;
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            var dir = Direction.None;
            
            if (keyboard.IsKeyPressed(Keys.Up))
                dir = Direction.Up;
            else if (keyboard.IsKeyPressed(Keys.Down))
                dir = Direction.Down;
            else if (keyboard.IsKeyPressed(Keys.Right))
                dir = Direction.Right;
            else if (keyboard.IsKeyPressed(Keys.Left))
                dir = Direction.Left;

            PlayerDidAct = CommandSystem.MovePlayer(dir);

            return true;
        }

        private void GenerateMap(Generator mapGen)
        {
            Map.ApplyTerrainOverlay(mapGen.Context.GetFirst<IGridView<bool>>("WallFloor"), GetTerrain);

            Game.Player = new Player(Map)
            {
                Position = Map.WalkabilityView.RandomPosition(true)
            };
            Map.PlayerFOV.Calculate(Game.Player.Position, 10);
        }

        private IGameObject GetTerrain(Point position, bool canWalk)
            => canWalk ? (IGameObject)new Floor(position) : new Wall(position);
    }
}