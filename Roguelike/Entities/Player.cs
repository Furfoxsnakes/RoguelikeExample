using GoRogue;
using Roguelike.Cartography;
using SadRogue.Primitives;

namespace Roguelike.Entities
{
    public class Player : GameEntity
    {
        // public FOV FOV;
        public Player(DungeonMap map) : base(map,Color.White, Color.Black, '@', (int)MapLayers.PLAYER)
        {
            
        }
    }
}