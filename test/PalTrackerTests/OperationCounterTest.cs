using System.Collections.Generic;
using PalTracker;
using Xunit;

namespace PalTrackerTests
{
    public class OperationCounterTest
    {
        private readonly OperationCounter<TimeEntry> _counter;

        public OperationCounterTest()
        {
            _counter = new OperationCounter<TimeEntry>();
        }

        [Fact]
        public void Increment()
        {
            var exepectedCounts = new Dictionary<TrackedOperation, int>
            {
                {TrackedOperation.Create, 0},
                {TrackedOperation.Read, 1},
                {TrackedOperation.List, 2},
                {TrackedOperation.Update,3},
                {TrackedOperation.Delete, 4}
            };

            foreach (var entry in exepectedCounts)
            {
                for (var i = 0; i < entry.Value; i++)
                {
                    _counter.Increment(entry.Key);
                }
            }

            Assert.Equal(exepectedCounts, _counter.GetCounts);
        }

        [Fact]
        public void Name()
        {
            Assert.Equal("TimeEntryOperations", _counter.Name);
        }
    }
}