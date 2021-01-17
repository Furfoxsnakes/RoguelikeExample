using Microsoft.Xna.Framework;
using SadConsole;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Roguelike.Screens
{
    public class DungeonScreen : ContainerConsole
    {
        private ScrollingConsole _mapConsole;
        private ScrollingConsole _messageConsole;
        private Console _statConsole;
        private Console _inventoryConsole;
        
        public DungeonScreen()
        {
            _mapConsole = new ScrollingConsole(80, 48, new Rectangle(0, 0, 80, 48))
            {
                DefaultBackground = Color.Black,
                Position = new Point(0,11)
            };
            _mapConsole.Print(1, 1, "Map");
            Children.Add(_mapConsole);

            _messageConsole = new ScrollingConsole(80, 11, new Rectangle(0, 0, 80, 11))
            {
                DefaultBackground = Color.Gray,
                Position = new Point(0,Game.Height - 11)
            };
            _messageConsole.Print(1, 1, "Messages");
            Children.Add(_messageConsole);

            _statConsole = new Console(20, 70)
            {
                DefaultBackground = Color.Brown,
                Position = new Point(Game.Width - 20, 0)
            };
            _statConsole.Print(1, 1, "Stats");
            Children.Add(_statConsole);

            _inventoryConsole = new Console(80, 11)
            {
                DefaultBackground = Color.Cyan,
                Position = new Point(0,0)
            };
            _inventoryConsole.Print(1, 1, "Inventory");
            Children.Add(_inventoryConsole);
        }
    }
}