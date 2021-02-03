using SadConsole;
using SadConsole.Components;
using SadConsole.Input;

namespace Roguelike.Components
{
    public class MapRendererKeyboardProcessor : KeyboardConsoleComponent
    {
        public override void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled)
        {
            handled = true;
        }
    }
}