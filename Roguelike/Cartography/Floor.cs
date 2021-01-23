using SadConsole;
using SadRogue.Primitives;

namespace Roguelike.Cartography
{
    public class Floor : Terrain
    {
        public Floor(Point position) : base(new ColoredGlyph(Color.White, Color.Black, '.'), position, true, true)
        {
            
        }
    }
}