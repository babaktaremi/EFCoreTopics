using System.Runtime.CompilerServices;
using EFCoreTopics.Database.Data;
using EFCoreTopics.Hubs.Stream.Models;
using Microsoft.AspNetCore.SignalR;

namespace EFCoreTopics.Hubs.Stream
{
    public class StreamingHub:Hub<IStreamingHub>
    {
        private readonly ILogger<StreamingHub> _logger;
        private readonly AdventureWorksLContext _db;

        public StreamingHub(ILogger<StreamingHub> logger, AdventureWorksLContext db)
        {
            _logger = logger;
            _db = db;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogWarning("User Connected With Connection ID {0}",base.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async IAsyncEnumerable<StreamingHubModel> RequestStream([EnumeratorCancellation]
            CancellationToken cancellationToken,int skipCount)
        {
            await foreach (var item in _db.GetPricesWithStreaming(cancellationToken, skipCount))
            {
                yield return new StreamingHubModel() { PriceValue = item.RecordedPrice, PriceTime = item.Date };

                await Task.Delay(1000,cancellationToken);
            }
        }

    }
}
