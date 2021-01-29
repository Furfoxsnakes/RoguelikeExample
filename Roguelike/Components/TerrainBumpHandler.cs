using GoRogue.GameFramework.Components;
using Roguelike.Cartography;
using Roguelike.Entities;
using Roguelike.Interfaces;

namespace Roguelike.Components
{
    public class TerrainBumpHandler : ComponentBase<Terrain>, IBumpHandler
    {
        public void OnBump(GameEntity entity)
        {
            if (Parent == null) return;
            
            Game.DungeonScreen.MessageLog.AddMessage($"{entity.Name} bumps their face into a {Parent}");
        }
    }
}