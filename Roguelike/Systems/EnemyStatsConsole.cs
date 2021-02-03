using Roguelike.Entities;
using SadConsole;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;

namespace Roguelike.Systems
{
    public class EnemyStatsConsole : ControlsConsole
    {
        private Label _nameLabel;
        private ProgressBar _healthProgress;
        
        public EnemyStatsConsole(Point pos) : base(Game.EnemyStatsSize.X, Game.EnemyStatsSize.Y)
        {
            Position = pos;

            _nameLabel = new Label("(1) Enemy Name")
            {
                TextColor = Color.Red,
                Position = (0, 0)
            };
            Controls.Add(_nameLabel);

            _healthProgress = new ProgressBar(Width, 1, HorizontalAlignment.Left)
            {
                Position = (0, 1)
            };
            Controls.Add(_healthProgress);
        }

        public void ShowStats(Monster monster)
        {
            _nameLabel.DisplayText = $"(1) {monster.Name}";
            _healthProgress.Progress = (float)monster.Health / monster.MaxHealth;
            IsVisible = true;
        }

        public void HideStats()
        {
            IsVisible = false;
        }
    }
}