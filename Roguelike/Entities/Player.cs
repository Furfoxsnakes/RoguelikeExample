using GoRogue;
using GoRogue.GameFramework;
using Roguelike.Cartography;
using SadConsole;
using SadRogue.Primitives;

namespace Roguelike.Entities
{
    public class Player : GameEntity
    {
        public int Awareness;
        
        // public FOV FOV;
        public Player(DungeonMap map) : base(map,Color.White, Color.Black, '@', (int)MapLayers.PLAYER)
        {
            Awareness = 10;
            Moved += OnPlayerMoved;
        }

        private void OnPlayerMoved(object? sender, GameObjectPropertyChanged<Point> e)
        {
            Game.DungeonMap?.PlayerFOV.Calculate(Position, Awareness);
        }
    }
}