using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace Roguelike.Systems
{
    public class CommandSystem
    {
        public bool MovePlayer(Direction direction)
        {
            var player = Game.Player;

            var canMove = player.CanMoveIn(direction);

            if (canMove)
                player.Position += direction;

            return canMove;
        }
    }
}