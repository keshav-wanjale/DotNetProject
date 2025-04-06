using System;
using System.Collections.Generic;
namespace CasCap
{
    public interface IDITestService
    {
        List<int> GetIntValues();
        List<string> GetStringValues();
    }

    /// <summary>
    /// This is a dependency injection test service.
    /// </summary>
    public class DITestService : IDITestService
    {
        public List<int> GetIntValues()
        {
            return new List<int> { DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Minute, DateTime.UtcNow.Second };
        }

        public List<string> GetStringValues()
        {
            return new List<string> { Environment.MachineName, DateTime.UtcNow.ToString() };
        }
    }
}