using FivemServersTest.proto;
using ProtoBuf;
using System;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Diagnostics;

namespace FivemServersTest
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			var servers = new List<Server>();

			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36");

			using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://servers-frontend.fivem.net/api/servers/streamRedir")))
			using (var response = await httpClient.SendAsync(request))
			{
				if (response.StatusCode == HttpStatusCode.OK)
				{
					using var content = response.Content;
					using var stream = await content.ReadAsStreamAsync();
					await FrameReader.ReadFramesAsync(stream, frame =>
					{
						servers.Add(Serializer.Deserialize<Server>(frame));
					});
				}
			}

			var i = 0;

			foreach (var server in servers)
			{
				using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://servers-frontend.fivem.net/api/servers/single/{server.EndPoint}")))
				using (var response = await httpClient.SendAsync(request))
				{
					if (response.StatusCode == HttpStatusCode.OK)
					{
						using var content = response.Content;
						var json = await content.ReadAsStringAsync();
						var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
						var hoge = JsonSerializer.Deserialize<Server>(json, options);
						Debug.WriteLine($"Processing {server.EndPoint}... ({++i}/{servers.Count()})");
						if (hoge == null) continue;
						;
					}
				}
			}
		}
	}
}
