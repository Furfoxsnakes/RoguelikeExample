using System;
using GoRogue.Components;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using Roguelike.Cartography;
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

        public GameEntity(Color foreground, Color background, int glyph, int zIndex) : base(foreground, background, glyph, zIndex)
        {
            _layer = (int)MapLayers.PLAYER;
        }

        public uint ID => _id;

        public int Layer => _layer;

        public void OnMapChanged(Map? newMap)
        {
            
        }

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