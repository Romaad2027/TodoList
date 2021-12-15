using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOListAPI
{
    public interface IAlertDao
    {
        public Guid AddAlert(DateTime time);

        public DateTime GetAlert(Guid id);
    }

    public class AlertDao : IAlertDao
    {
        private readonly Dictionary<Guid, DateTime> alerts = new Dictionary<Guid, DateTime>();

        public Guid AddAlert(DateTime time)
        {
            Guid id = Guid.NewGuid();
            this.alerts.Add(id, time);
            return id;
        }

        public DateTime GetAlert(Guid id)
        {
            return this.alerts[id];
        }
    }

    public class AlertService
    {
        private readonly IAlertDao storage;

        public AlertService(IAlertDao _storage)
        {
            storage = _storage;
        }

        public Guid RaiseAlert()
        {
            return this.storage.AddAlert(DateTime.Now);
        }

        public DateTime GetAlertTime(Guid id)
        {
            return this.storage.GetAlert(id);
        }
    }
}
