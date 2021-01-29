using System;
using GoRogue;
using GoRogue.Components;
using GoRogue.GameFramework;
using Roguelike.Cartography;
using Roguelike.Interfaces;
using SadConsole;
using SadConsole.Entities;
using Color = SadRogue.Primitives.Color;
using Point = SadRogue.Primitives.Point;

namespace Roguelike.Entities
{
    public class GameEntity : Entity, IGameObject, ICombatant
    {
        #region IGameObject
        
        public uint ID => _id;
        private uint _id;

        public bool IsTransparent
        {
            get => _isTransparent;
            set => this.SafelySetProperty(ref _isTransparent, value, TransparencyChanged);
        }
        private bool _isTransparent;
        
        public ITaggableComponentCollection GoRogueComponents => _goRogueComponents;
        private ITaggableComponentCollection _goRogueComponents;
        
        public bool IsWalkable
        {
            get => _isWalkable;
            set => this.SafelySetProperty(ref _isWalkable, value, WalkabilityChanged);
        }
        private bool _isWalkable;

        public int Layer => _layer;
        private int _layer;
        
        public Map? CurrentMap { get; private set; }
        private Map? _currentMap;
        
        #endregion
        
        #region ICombatant

        public int Attack
        {
            get => _attack;
            set => _attack = value;
        }

        public int AttackChance
        {
            get => _attackChance;
            set => _attackChance = value;
        }

        public int Awareness
        {
            get => _awareness;
            set => _awareness = value;
        }

        public int Defense
        {
            get => _defense;
            set => _defense = value;
        }

        public int DefenseChance
        {
            get => _defenseChance;
            set => _defenseChance = value;
        }

        public int Gold
        {
            get => _gold;
            set => _gold = value;
        }

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int Speed
        {
            get => _speed;
            set => _speed = value;
        }
        private int _attack;
        private int _attackChance;
        private int _awareness;
        private int _defense;
        private int _defenseChance;
        private int _gold;
        private int _health;
        private int _maxHealth;
        private int _speed;

        #endregion
        
        public event EventHandler<GameObjectPropertyChanged<bool>>? TransparencyChanged;
        public event EventHandler<GameObjectPropertyChanged<bool>>? WalkabilityChanged;
        public event EventHandler<GameObjectPropertyChanged<Point>>? Moved;
        
        public FOV FOV;

        public GameEntity(DungeonMap map, Point pos, Color foreground, Color background, int glyph, int layer) : base(foreground, background, glyph, layer)
        {
            PositionChanged += OnPositionChanged;

            _layer = layer;
            FOV = new FOV(map.TransparencyView);
            Position = pos;
            map.AddEntity(this);
        }

        private void OnPositionChanged(object? sender, ValueChangedEventArgs<Point> e)
            => Moved?.Invoke(sender, new GameObjectPropertyChanged<Point>(this, e.OldValue, e.NewValue));
        
        public void OnMapChanged(Map? newMap) => CurrentMap = newMap;

    }
}