using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FivemServersTest
{
	public class HttpClientPool
	{
		private const int MaxRetries = 25;
		private const int RetryDelay = 1000 * 15;
		private const int ToomanyRequestDelay = 1000 * 30;
		private readonly ConcurrentQueue<string> _urlQueue;
		private readonly List<HttpClient> _httpClients;
		private readonly SemaphoreSlim _semaphore;
		private readonly List<Task> _tasks;

		public event Action<string, HttpResponseMessage> RequestCompleted;

		public HttpClientPool(IEnumerable<string> urls)
		{
			_urlQueue = new ConcurrentQueue<string>(urls);
			_httpClients = new List<HttpClient>();

			var addresses = GetTemporaryIPv6Address();

			_semaphore = new SemaphoreSlim(addresses.Count());
			_tasks = new List<Task>();

			foreach (var address in addresses)
			{
				var handler = new SocketsHttpHandler
				{
					AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
					ConnectCallback = async (context, token) =>
					{
						var socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
						socket.Bind(new IPEndPoint(address, 0));
						await socket.ConnectAsync(context.DnsEndPoint, token);
						return new NetworkStream(socket, ownsSocket: true);
					}
				};

				var client = new HttpClient(handler);
				client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36");
				_httpClients.Add(client);
				_tasks.Add(Task.Run(() => ProcessQueue(client)));
			}
		}

		private IEnumerable<IPAddress> GetTemporaryIPv6Address()
		{
			var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
			var tempAddresses = networkInterfaces
				.SelectMany(ni => ni.GetIPProperties().UnicastAddresses
					.Where(a => a.Address.AddressFamily == AddressFamily.InterNetworkV6 &&
					!a.Address.IsIPv6LinkLocal &&
					a.SuffixOrigin == System.Net.NetworkInformation.SuffixOrigin.Random));

			return tempAddresses.Select(a => a.Address);
		}

		private async Task ProcessQueue(HttpClient client)
		{
			while (!_urlQueue.IsEmpty)
			{
				await _semaphore.WaitAsync();

				if (_urlQueue.TryDequeue(out var url))
				{
					await ProcessUrl(client, url);
				}

				_semaphore.Release();
			}
		}

		private async Task ProcessUrl(HttpClient client, string url)
		{
			HttpResponseMessage response = null;
			for (int attempt = 1; attempt <= MaxRetries; attempt++)
			{
				try
				{
					response = await client.GetAsync(url);
					if (response.IsSuccessStatusCode)
					{
						break;
					}
					else if(response.StatusCode == HttpStatusCode.TooManyRequests)
					{
						Debug.WriteLine($"Retrying too many requests {attempt}...");
						await Task.Delay(ToomanyRequestDelay);
					}
				}
				catch
				{
					await Task.Delay(RetryDelay);
				}
			}

			RequestCompleted?.Invoke(url, response);
		}

		public async Task WaitForCompletion()
		{
			await Task.WhenAll(_tasks);
		}
	}
}
