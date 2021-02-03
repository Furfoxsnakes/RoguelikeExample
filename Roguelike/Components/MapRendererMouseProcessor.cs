using Roguelike.Cartography;
using Roguelike.Entities;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;

namespace Roguelike.Components
{
    public class MapRendererMouseProcessor : MouseConsoleComponent
    {
        private DungeonMap _map;
        
        public MapRendererMouseProcessor(DungeonMap map)
        {
            _map = map;
        }
        
        public override void ProcessMouse(IScreenObject host, MouseScreenObjectState state, out bool handled)
        {
            handled = true;

            var monster = _map.GetEntityAt<Monster>(state.CellPosition);

            if (monster == null)
            {
                Game.DungeonScreen.EnemyStatsConsole.HideStats();
                return;
            }

            Game.DungeonScreen.EnemyStatsConsole.ShowStats(monster);

        }
    }
}