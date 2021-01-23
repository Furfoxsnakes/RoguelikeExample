using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace Roguelike.Systems
{
    public class CommandSystem
    {
        public bool MovePlayer(Direction direction)
        {
            var player = Game.Player;
            
            System.Console.WriteLine(player.CurrentMap.WalkabilityView[player.Position + direction]);

            if (player.CanMoveIn(direction))
            {
                player.Position += direction;
            }

            return player.CanMoveIn(direction);
        }
    }
}