using System.Collections.Generic;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using Roguelike.Entities;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace Roguelike.Cartography
{
    public class TranslateToDungeonMapStep : GenerationStep
    {
        protected override IEnumerator<object?> OnPerform(GenerationContext context)
        {
            var wallFloor = context.GetFirst<IGridView<bool>>("WallFloor");
            
            // create a new map
            var map = new DungeonMap(context.Width, context.Height);
            context.Add(map, "Map");
            map.ApplyTerrainOverlay(wallFloor, GetTerrain);

            yield return null;
            
            //TODO: Place doors
            // https://github.com/Chris3606/TheSadRogue.Integration/blob/more-feature-dev/TheSadRogue.Integration.Example/MapGeneration/TranslateToMapStep.cs
            
            // Spawn player
            // var player = new GameEntity(Color.White, Color.Black, '@', (int) MapLayers.PLAYER)
            // {
            //     Position = map.WalkabilityView.RandomPosition(true)
            // };
            // map.AddEntity(player);

            //TODO: Spawn monsters
        }

        private IGameObject GetTerrain(Point position, bool canWalk)
            => canWalk ? (IGameObject)new Floor(position) : new Wall(position);
    }
}