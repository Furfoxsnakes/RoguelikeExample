using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using Roguelike.Cartography;
using Roguelike.Entities;
using Roguelike.Systems;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace Roguelike.Screens
{
    public class DungeonScreen : ScreenObject
    {
        private ScreenSurface _mapRenderer;
        public MessageLogConsole MessageLog;
        private PlayerStatsConsole _statConsole;
        private Console _inventoryConsole;
        
        public DungeonMap Map { get; }
        public bool PlayerDidAct;
        public CommandSystem CommandSystem { get; private set; }

        public DungeonScreen(Generator mapGen)
        {
            CommandSystem = new CommandSystem();
            
            Map = new DungeonMap(mapGen.Context.Width, mapGen.Context.Height);
            GenerateMap(mapGen);
            
            GenerateConsoles();
            
            IsFocused = true;
        }

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            var dir = Direction.None;
            
            if (keyboard.IsKeyPressed(Keys.Up))
                dir = Direction.Up;
            else if (keyboard.IsKeyPressed(Keys.Down))
                dir = Direction.Down;
            else if (keyboard.IsKeyPressed(Keys.Right))
                dir = Direction.Right;
            else if (keyboard.IsKeyPressed(Keys.Left))
                dir = Direction.Left;

            if (dir != Direction.None)
                if (CommandSystem.MovePlayer(dir))
                    PlayerDidAct = true;
                else
                    Map.Bump(Game.Player, Game.Player.Position + dir); 

            return true;
        }

        private void GenerateConsoles()
        {
            MessageLog = new MessageLogConsole();
            Children.Add(MessageLog);

            _statConsole = new PlayerStatsConsole(Game.Player);
            Children.Add(_statConsole);

            _inventoryConsole = new Console(Game.InventorySize.X, Game.InventorySize.Y)
            {
                DefaultBackground = Color.Gray,
                Position = (Game.Width - Game.InventorySize.X, 0)
            };
            _inventoryConsole.Print(1, 1, "Inventory");
            Children.Add(_inventoryConsole);
        }

        private void GenerateMap(Generator mapGen)
        {
            _mapRenderer = Map.CreateRender(Game.MapRenderSize);
            _mapRenderer.Position = (Game.PlayerStatSize.X, 0);
            Children.Add(_mapRenderer);
            
            Map.ApplyTerrainOverlay(mapGen.Context.GetFirst<IGridView<bool>>("WallFloor"), GetTerrain);

            var randomPosition = Map.WalkabilityView.RandomPosition(true);
            Game.Player = new Player(Map, randomPosition);
            Game.Player.Moved += OnPlayerMoved;
            

            for (var i = 0; i < 10; i++)
            {
                var randomPos = Map.WalkabilityView.RandomPosition(true);
                var zombie = Zombie.Create(Map, randomPos, 1);
            }
            
            Map.PlayerFOV.Calculate(Game.Player.Position, 10);
            _mapRenderer.Surface.View = _mapRenderer.Surface.View.WithCenter(Game.Player.Position);
        }

        private void OnPlayerMoved(object? sender, GameObjectPropertyChanged<Point> e)
        {
            _mapRenderer.Surface.View = _mapRenderer.Surface.View.WithCenter(e.NewValue);
            Map.PlayerFOV.Calculate(e.NewValue, Game.Player.Awareness);
        }

        private IGameObject GetTerrain(Point position, bool canWalk)
            => canWalk ? (IGameObject)new Floor(position) : new Wall(position);
    }
}