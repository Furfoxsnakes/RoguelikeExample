using System.Collections.Generic;
using SadConsole;

namespace Roguelike.Systems
{
    public class MessageLogConsole : Console
    {
        private static readonly int _maxLines = 9;

        private readonly Queue<string> _lines;
        // private readonly Stack<string> _lines;

        public MessageLogConsole(int width, int height) : base(width, height)
        {
            // _lines = new Stack<string>();
            _lines = new Queue<string>();
        }

        public void AddMessage(string message)
        {
            _lines.Enqueue(message);
            // _lines.Push(message);

            if (_lines.Count > _maxLines)
                _lines.Dequeue();
                // _lines.Pop();
            
            var lines = _lines.ToArray();
            
            this.Clear();

            for (int i = 0; i < _lines.Count; i++)
            {
                Cursor.Position = (0, i);
                Cursor.Print(lines[i] + "\n");
            }
        }
    }
}