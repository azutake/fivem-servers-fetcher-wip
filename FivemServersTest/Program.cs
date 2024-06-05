using FivemServersTest.proto;
using ProtoBuf;
using System;
using System.Net.Http;
using System.Net;

namespace FivemServersTest
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

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
						var hoge = Serializer.Deserialize<Server>(frame);
						Console.WriteLine(hoge.Data.Hostname);
					});
				}
			}
		}
	}
}