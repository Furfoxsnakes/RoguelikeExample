using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using Roguelike.Cartography;
using Roguelike.Entities;
using SadConsole;
using SadConsole.Entities;
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

            Map.ApplyTerrainOverlay(mapGen.Context.GetFirst<IGridView<bool>>("WallFloor"), GetTerrain);
            
            var player = new GameEntity(Color.White, Color.Black, '@', (int) MapLayers.PLAYER)
            {
                Position = Map.WalkabilityView.RandomPosition(true)
            };
            Map.AddEntity(player);
        }

        private IGameObject GetTerrain(Point position, bool canWalk)
            => canWalk ? (IGameObject)new Floor(position) : new Wall(position);
    }
}