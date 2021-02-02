using System;
using Roguelike.Cartography;
using SadRogue.Primitives;

namespace Roguelike.Entities
{
    public class Player : GameEntity
    {

        // public FOV FOV;
        public Player(DungeonMap map, Point pos) : base(map, pos,Color.White, Color.Black, '@', (int)MapLayers.PLAYER)
        {
            Awareness = 10;
            Attack = 2;
            AttackChance = 60;
            Defense = 1;
            DefenseChance = 40;
            Gold = 0;
            Health = 10;
            MaxHealth = 10;
            Name = "Rogue";
        }
    }
}