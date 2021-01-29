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
        public Player(DungeonMap map, Point pos) : base(map, pos,Color.White, Color.Black, '@', (int)MapLayers.PLAYER)
        {
            Awareness = 10;
            Name = "Rogue";
        }
    }
}