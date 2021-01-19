using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using Roguelike.Cartography;
using Roguelike.Entities;
using SadConsole;
using SadConsole.Entities;
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

        public DungeonScreen(Generator mapGen)
        {
            Map = new DungeonMap(mapGen.Context.Width, mapGen.Context.Height);
            _mapRenderer = Map.CreateRender((100,70));
            Children.Add(_mapRenderer);
            
            GenerateMap(mapGen);

            IsFocused = true;
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            if (keyboard.IsKeyPressed(Keys.D))
            {
                Game.Player.Position += (1, 0);
                Map.PlayerFOV.Calculate(Game.Player.Position, 10);
            }

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