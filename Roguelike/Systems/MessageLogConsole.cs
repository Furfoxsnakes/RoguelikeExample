using System.Collections.Generic;
using SadConsole;

namespace Roguelike.Systems
{
    public class MessageLogConsole : Console
    {
        private static readonly int _maxLines = 9;

        private readonly Queue<string> _lines;

        public MessageLogConsole() : base(Game.MessageLogSize.X, Game.MessageLogSize.Y)
        {
            Position = (0, Game.Height - Height);
            _lines = new Queue<string>();
        }

        public void AddMessage(string message)
        {
            _lines.Enqueue(message);

            if (_lines.Count > _maxLines)
                _lines.Dequeue();

            var lines = _lines.ToArray();
            
            Surface.Clear();

            for (int i = 0; i < _lines.Count; i++)
            {
                Cursor.Position = (1, i + 1);
                Cursor.Print(lines[i] + "\n");
            }
        }
    }
}