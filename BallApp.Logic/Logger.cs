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

        public async Task LogAsync(object data)
        {
            string jsonData = JsonSerializer.Serialize(data);
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    File.AppendAllText(_filePath, jsonData + Environment.NewLine);
                }
            });
        }
    }
}
