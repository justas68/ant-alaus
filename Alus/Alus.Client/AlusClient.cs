using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alus.Core.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Alus.Client
{
    public class AlusClient
    {
        private RestClient _client = new RestClient("http://localhost:50680");
        private Lazy<JsonSerializer> _serializer = new Lazy<JsonSerializer>();

        private readonly string api = "api/feedback";

        public AlusClient()
        {
        }

        public async Task AddAsync(Feedback feedback, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Request<Feedback>(feedback, cancellationToken);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new RestRequest(api, Method.GET);
            var response = await _client.ExecuteTaskAsync(request, cancellationToken);
            return Deserialize<IEnumerable<Feedback>>(response);
        }

        private async Task<IRestResponse> Request<T>(T obj, CancellationToken cancellationToken)
        {
            var request = new RestRequest(api, Method.POST);
            request.RequestFormat = DataFormat.Json;
            var data = JsonConvert.SerializeObject(obj);

            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", data, ParameterType.RequestBody);

            return await _client.ExecuteTaskAsync(request, cancellationToken);
        }

        private T Deserialize<T>(IRestResponse response)
        {
            using (var stream = new JsonTextReader(new StreamReader(new MemoryStream(response.RawBytes))))
            {
                return _serializer.Value.Deserialize<T>(stream);
            }
        }
    }
}
