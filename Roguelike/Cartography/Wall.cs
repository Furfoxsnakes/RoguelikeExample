using Roguelike.Components;
using SadConsole;
using SadRogue.Primitives;

namespace Roguelike.Cartography
{
    public class Wall : Terrain
    {
        public Wall(Point position) : base(new ColoredGlyph(Color.White, Color.Black, '#'), position, false, false)
        {
            GoRogueComponents.Add(new TerrainBumpHandler());
        }
    }
}