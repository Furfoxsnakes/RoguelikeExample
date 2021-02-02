using System;
using Roguelike.Entities;
using SadConsole;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;

namespace Roguelike.Systems
{
    public class PlayerStatsConsole : SadConsole.UI.ControlsConsole
    {
        private Label _nameLabel;
        private Label _healthLabel;
        private Label _attackLabel;
        private Label _defenseLabel;
        private Label _goldLabel;

        private Player _player;
        
        public PlayerStatsConsole(Player player) : base(Game.PlayerStatSize.X, Game.PlayerStatSize.Y)
        {
            _player = player;
            Position = (0, 0);

            player.OnAttackDidChange += OnAttackChanged;
            player.OnDefenseDidChange += OnDefenseChange;
            player.OnGoldDidChange += OnGoldChanged;
            player.OnHealthDidChange += OnHealthChanged;

            // set the theme colours
            var colours = new Colors()
            {
                IsLightTheme = false,
                ControlForegroundNormal = new AdjustableColor(Color.White, "Control Foreground Normal")
            };
            colours.RebuildAppearances();
            Controls.ThemeColors = colours;

            // build the stat labels
            _nameLabel = new Label($"Name: {player.Name}")
            {
                Position = (1, 1)
            };
            Controls.Add(_nameLabel);

            _healthLabel = new Label($"Health: {player.Health}/{player.MaxHealth}")
            {
                Position = (1, 2)
            };
            Controls.Add(_healthLabel);

            _attackLabel = new Label($"Attack: {player.Attack}({player.AttackChance}%)")
            {
                Position = (1, 3)
            };
            Controls.Add(_attackLabel);

            _defenseLabel = new Label($"Defense: {player.Defense}({player.DefenseChance}%)")
            {
                Position = (1,4)
            };
            Controls.Add(_defenseLabel);

            _goldLabel = new Label($"Gold: {player.Gold}")
            {
                Position = (1,5),
                TextColor = Color.Gold
            };
            Controls.Add(_goldLabel);
        }

        private void OnGoldChanged(object? sender, EventArgs e)
        {
            _goldLabel.DisplayText = $"Gold: {_player.Gold}";
        }

        private void OnDefenseChange(object? sender, EventArgs e)
        {
            _defenseLabel.DisplayText = $"Defense: {_player.Defense}({_player.DefenseChance}%)";
        }

        private void OnAttackChanged(object? sender, EventArgs e)
        {
            _attackLabel.DisplayText = $"Attack: {_player.Attack}({_player.AttackChance}%)";
        }

        private void OnHealthChanged(object? sender, EventArgs e)
        {
            _healthLabel.DisplayText = $"Health: {_player.Health}/{_player.MaxHealth}";
        }
    }
}