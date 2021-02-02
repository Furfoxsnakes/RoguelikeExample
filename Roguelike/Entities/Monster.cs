using GoRogue.Components;
using Roguelike.Cartography;
using Roguelike.Components;
using SadConsole;
using SadRogue.Primitives;

namespace Roguelike.Entities
{
    public class Monster : GameEntity
    {
        public ColoredGlyph InFOVAppearance { get; }
        public Monster(DungeonMap map, Point pos, Color foreground, Color background, int glyph, int layer) : base(map, pos, Color.Transparent, Color.Transparent, glyph, layer)
        {
            GoRogueComponents.Add(new EntityBumpHandler(this));
            InFOVAppearance = new ColoredGlyph(foreground, background, glyph);
        }
    }
}