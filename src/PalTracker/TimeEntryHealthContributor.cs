using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steeltoe.Common.HealthChecks;

namespace PalTracker
{
    public class TimeEntryHealthContributor: IHealthContributor
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        public const int MaxTimeEntries = 5;

        public string Id { get; } = "timeEntry";

        public TimeEntryHealthContributor(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }


        public HealthCheckResult Health()
        {
            var count = _timeEntryRepository.List().Count();
            var status = count < MaxTimeEntries ? HealthStatus.UP : HealthStatus.DOWN;

            var health = new HealthCheckResult { Status = status };

            health.Details.Add("threshold", MaxTimeEntries);
            health.Details.Add("count", count);
            health.Details.Add("status", status.ToString());

            return health;
        }
    }
}
