using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Alus.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Alus.Client
{
    public class AlusClient
    {
        public static IConfigurationRoot Configuration { get; }

        static AlusClient()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Configuration = new ConfigurationBuilder()
                .AddXmlFile("appsettings.xml")
                .Build();
        }

        private RestClient _client = new RestClient(Configuration["host"]);
        private Lazy<JsonSerializer> _serializer = new Lazy<JsonSerializer>();

        private readonly string api = "api/feedback";

        public AlusClient()
        {
        }

        #region Feedback

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

        #endregion

        #region Bar

        /*
        public async Task AddAsync(Bar bar, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Request<Bar>(bar, "api/bar/add", Method.POST, cancellationToken);
        }

        public async Task<IEnumerable<Bar>> GetBarsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new RestRequest("api/bar", Method.GET);
            var response = await _client.ExecuteTaskAsync(request, cancellationToken);
            return Deserialize<IEnumerable<Bar>>(response);
        }

        public async Task SetBarsAsync(IEnumerable<Bar> bars, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Request<IEnumerable<Bar>>(bars, "api/bar/add", Method.POST, cancellationToken);
        }
        */
        public void Add(Bar bar)
        {
            Request<Bar>(bar, "api/bar/add", Method.POST);
        }

        public IEnumerable<Bar> GetBars()
        {
            var request = new RestRequest("api/bar", Method.GET);
            var response = _client.Execute(request);
            return Deserialize<IEnumerable<Bar>>(response);
        }

        public void SetBars(IEnumerable<Bar> bars)
        {
            Request<IEnumerable<Bar>>(bars, "api/bar", Method.POST);
        }

        private IRestResponse Request<T>(T obj, string api, Method method)
        {
            var request = new RestRequest(api, method);
            request.RequestFormat = DataFormat.Json;
            var data = JsonConvert.SerializeObject(obj);

            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", data, ParameterType.RequestBody);

            return _client.Execute(request);
        }

        #endregion

        private T Deserialize<T>(IRestResponse response)
        {
            using (var stream = new JsonTextReader(new StreamReader(new MemoryStream(response.RawBytes))))
            {
                return _serializer.Value.Deserialize<T>(stream);
            }
        }

        private async Task<IRestResponse> Request<T>(T obj, CancellationToken cancellationToken)
        {
            return await Request<T>(obj, api, Method.POST, cancellationToken);
        }

        private async Task<IRestResponse> Request<T>(T obj, string api, Method method, CancellationToken cancellationToken)
        {
            var request = new RestRequest(api, method);
            request.RequestFormat = DataFormat.Json;
            var data = JsonConvert.SerializeObject(obj);

            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", data, ParameterType.RequestBody);

            return await _client.ExecuteTaskAsync(request, cancellationToken);
        }

    }
}
