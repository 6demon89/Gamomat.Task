using System.Text;

namespace SomeTask
{
    public class Reel
    {
        public int ID { get; set; }
        public SymbolOption[] Values { get; set; }
        public Reel(int _id, SymbolOption[] values)
        {
            ID = _id;
            Values = values;
        }
    }

    public enum SymbolOption : byte
    {
        A, B, C
    }

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Reel[] Reels = new Reel[3];
        private Random _random = new Random();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            int ReelLegth = 10;
            var optionsLength = Enum.GetValues(typeof(SymbolOption)).Length;

            for (int r = 0; r < Reels.Length; r++)
            {
                SymbolOption[] Values = new SymbolOption[ReelLegth];
                for (int i = 0; i < ReelLegth; i++)
                {
                    Values[i] = (SymbolOption)_random.Next(0, optionsLength);
                }
                Reels[r] = new Reel(r, Values);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}