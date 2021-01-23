using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace Roguelike.Systems
{
    public class CommandSystem
    {
        public bool MovePlayer(Direction direction)
        {
            var player = Game.Player;

            if (player.CanMoveIn(direction))
            {
                player.Position += direction;
            }

            return player.CanMoveIn(direction);
        }
    }
}