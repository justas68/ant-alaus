using System.Collections.Generic;
using Alus.Client;
using Alus.Core.Models;

namespace Alus
{
    public class BarWebServiceContainer : IBarContainer
    {
        private readonly AlusClient _client;

        public BarWebServiceContainer(AlusClient client)
        {
            _client = client;
        }

        public void Add(Bar bar)
        {
            _client.Add(bar);
        }

        public IEnumerable<Bar> GetAll()
        {
            return _client.GetBars();
        }

        public void SetAll(IEnumerable<Bar> bars)
        {
            _client.SetBars(bars);
        }
    }
}
