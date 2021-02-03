using GoRogue.GameFramework.Components;
using Roguelike.Entities;
using Roguelike.Interfaces;

namespace Roguelike.Components
{
    public sealed class EntityBumpHandler : ComponentBase<GameEntity>, IBumpHandler
    {

        public EntityBumpHandler(GameEntity parent)
        {
            Parent = parent;
        }
        
        public void OnBump(GameEntity entity)
        {
            if (Parent == null) return;
            
            Game.DungeonScreen.MessageLog.AddMessage($"{entity.Name} bumps into {Parent.Name}");

            Parent.Health -= 1;

            if (Parent.Health > 0) return;
            
            if (Parent is Monster)
            {
                Game.DungeonScreen.MessageLog.AddMessage($"{Parent.Name} has been slain!");
                Game.DungeonScreen.Map.RemoveEntity(Parent);
            }
        }
    }
}