using System;
using GoRogue.Components;
using GoRogue.GameFramework;
using SadConsole;
using SadRogue.Primitives;

namespace Roguelike.Cartography
{
    public class Terrain : GameObject
    {
        public ColoredGlyph Appearance
        {
            get => _appearance;
            set => _appearance = value;
        }
        private ColoredGlyph _appearance;

        public event EventHandler? AppearanceChanged;
        
        public Terrain(ColoredGlyph appearance, Point position, bool isWalkable = true, bool isTransparent = true, Func<uint>? idGenerator = null, ITaggableComponentCollection? customComponentContainer = null) : base(position, 0, isWalkable, isTransparent, idGenerator, customComponentContainer)
        {
            _appearance = appearance;
        }

        public void SetForeground(Color fg)
        {
            Appearance.Foreground = fg;
            AppearanceChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}