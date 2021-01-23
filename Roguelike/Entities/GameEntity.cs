using System;
using GoRogue;
using GoRogue.Components;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using Roguelike.Cartography;
using SadConsole;
using SadConsole.Entities;
using Color = SadRogue.Primitives.Color;
using Point = SadRogue.Primitives.Point;

namespace Roguelike.Entities
{
    public class GameEntity : Entity, IGameObject
    {
        private uint _id;
        private int _layer;
        private Map? _currentMap;
        private bool _isTransparent;
        private ITaggableComponentCollection _goRogueComponents;
        private bool _isWalkable;
        
        // Does not compile
        public Map? CurrentMap { get; set; }
        public FOV FOV;
        
        public GameEntity(DungeonMap map, Color foreground, Color background, int glyph, int layer) : base(foreground, background, glyph, layer)
        {
            _layer = layer;
            FOV = new FOV(map.TransparencyView);
            map.AddEntity(this);

            PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(object? sender, ValueChangedEventArgs<Point> e)
            => Moved?.Invoke(sender, new GameObjectPropertyChanged<Point>(this, e.OldValue, e.NewValue));

        public uint ID => _id;

        public int Layer => _layer;

        // Does not compile
        public void OnMapChanged(Map? newMap) => CurrentMap = newMap;

        public Map? CurrentMap => _currentMap;

        public bool IsTransparent
        {
            get => _isTransparent;
            set => _isTransparent = value;
        }

        public ITaggableComponentCollection GoRogueComponents => _goRogueComponents;

        public bool IsWalkable
        {
            get => _isWalkable;
            set => _isWalkable = value;
        }

        public event EventHandler<GameObjectPropertyChanged<bool>>? TransparencyChanged;
        public event EventHandler<GameObjectPropertyChanged<bool>>? WalkabilityChanged;
        public event EventHandler<GameObjectPropertyChanged<Point>>? Moved;
    }
}