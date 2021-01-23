using System;
using System.Linq;
using GoRogue;
using GoRogue.Components;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.Pathing;
using GoRogue.SpatialMaps;
using SadConsole;
using SadConsole.Entities;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace Roguelike.Cartography
{
    public enum MapLayers
    {
        TERRAIN,
        ITEMS,
        MONSTERS,
        PLAYER
    }
    
    public class DungeonMap : Map
    {
        private ScreenSurface _renderer;
        private Renderer _entityRenderer;

        private static ColoredGlyph _transparentAppearance = new ColoredGlyph(Color.Transparent, Color.Transparent);

        public DungeonMap(int width, int height, uint layersBlockingWalkability = 4294967295, uint layersBlockingTransparency = 4294967295, uint entityLayersSupportingMultipleItems = 4294967295, FOV? customPlayerFOV = null, AStar? customPather = null, ITaggableComponentCollection? customComponentContainer = null) : base(width, height, Enum.GetValues(typeof(MapLayers)).Length - 1, Distance.Chebyshev, layersBlockingWalkability, layersBlockingTransparency, entityLayersSupportingMultipleItems, customPlayerFOV, customPather, customComponentContainer)
        {
            ObjectAdded += OnObjectAdded;
            ObjectRemoved += OnObjectRemoved;

            PlayerFOV.Recalculated += OnPlayerFOVCalculated;
        }

        private void OnPlayerFOVCalculated(object? sender, FOVRecalculatedEventArgs e)
        {
            foreach (var point in PlayerFOV.NewlySeen)
                GetTerrainAt<Terrain>(point)?.SetForeground(Color.White);


            foreach (var point in PlayerFOV.NewlyUnseen)
                GetTerrainAt<Terrain>(point)?.SetForeground(Color.Gray);
            
        }

        private void OnObjectRemoved(object? sender, ItemEventArgs<IGameObject> e)
        {
            switch (e.Item)
            {
                case Entity entity:
                    _entityRenderer.Remove(entity);
                    break;
            }
        }

        private void OnObjectAdded(object? sender, ItemEventArgs<IGameObject> e)
        {
            switch (e.Item)
            {
                case Terrain terrain:
                    terrain.AppearanceChanged += OnTerrainAppearanceChanged;
                    OnTerrainAppearanceChanged(terrain, EventArgs.Empty);
                    break;
                case Entity entity:
                    _entityRenderer?.Add(entity);
                    break;
            }
        }
        
        public ScreenSurface CreateRender(Point? viewSize = null)
        {
            var (viewWidth, viewHeight) = viewSize ?? (Width, Height);
            var cellSurface = new CellSurface(viewWidth, viewHeight,100,100);
            _renderer = new ScreenSurface(cellSurface);
            _entityRenderer = new Renderer();
            _renderer.SadComponents.Add(_entityRenderer);
            
            // add any entity associated with the map the to entity renderer
            _entityRenderer.AddRange(Entities.Items.Cast<Entity>());
            return _renderer;
        }

        private void OnTerrainAppearanceChanged(object? sender, EventArgs e)
        {
            var terrtain = sender as Terrain;
            if (!PlayerExplored[terrtain.Position]) return;
            _renderer.Surface.SetCellAppearance(terrtain.Position.X, terrtain.Position.Y, terrtain.Appearance);
        }
    }
}