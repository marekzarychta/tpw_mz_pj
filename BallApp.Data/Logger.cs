using System.Text.Json;

namespace Logic
{
    public class Logger
    {
        private static readonly object _lock = new object();
        private readonly string _filePath;

        public Logger(string filePath)
        {
            _filePath = filePath;
        }

        public async Task LogAsync(object log)
        {
            string jsonData = JsonSerializer.Serialize(log);
            lock (_lock)
            {
                File.AppendAllText(_filePath, jsonData + Environment.NewLine);
            }
        }



    }
}
