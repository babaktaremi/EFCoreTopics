using BenchmarkDotNet.Attributes;
using EFCoreTopics.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTopics.Benchmarks
{
    [MemoryDiagnoser()]
    [SimpleJob(launchCount: -1, warmupCount: 3, targetCount: 10)]
    public class ChangeTrackerBenchmark
    {


        [Benchmark]
        public void WithChangeTrackerOn()
        {
            using AdventureWorksLContext db = new AdventureWorksLContext();
            var addresses = db.Addresses.Take(1000).ToList();

            foreach (var address in addresses)
            {
                if (address.AddressId % 2 == 0)
                {
                    address.City = "Tehran";
                    db.SaveChanges();
                }

            }
        }

        [Benchmark]
        public void WithChangeTrackerOff()
        {
             using AdventureWorksLContext db = new AdventureWorksLContext();
            db.ChangeTracker.AutoDetectChangesEnabled = false;

            var addresses =  db.Addresses.Take(1000).ToList();

            foreach (var address in addresses)
            {
                if (address.AddressId % 2 == 0)
                {
                    db.Entry(address).State = EntityState.Modified;
                    address.City = "Tehran22";
                     db.SaveChanges();
                }

            }
        }

    }
}
