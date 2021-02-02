using GoRogue.DiceNotation;
using Roguelike.Cartography;
using Roguelike.Config;
using SadRogue.Primitives;

namespace Roguelike.Entities
{
    public class Zombie : Monster
    {
        public Zombie(DungeonMap map, Point pos, Color foreground, Color background, int glyph, int layer) : base(map, pos, foreground, background, glyph, layer)
        {
            
        }

        public static Zombie Create(DungeonMap map, Point pos, int level)
        {
            var health = Dice.Roll("2d5");
            return new Zombie(map, pos, Colours.ZombieColour, Color.Black, 'Z', (int) MapLayers.MONSTERS)
            {
                Attack = Dice.Roll("2d5") + level / 3,
                AttackChance = Dice.Roll("25d3"),
                Awareness = 10,
                Defense = Dice.Roll("1d3") + level / 3,
                DefenseChance = Dice.Roll("10d4"),
                Gold = Dice.Roll("5d5"),
                Health = health,
                MaxHealth = health,
                Name = "Zombur"
            };
        }
    }
}