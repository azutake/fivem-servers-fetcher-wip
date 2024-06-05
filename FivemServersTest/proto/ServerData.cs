using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivemServersTest.proto
{
	[ProtoContract]
	public class ServerData
	{
		[ProtoMember(1)]
		public required int svMaxClients { get; set; }

		[ProtoMember(2)]
		public required int Clients { get; set; }

		[ProtoMember(3)]
		public required int Protocol { get; set; }

		[ProtoMember(4)]
		public required string Hostname { get; set; }

		[ProtoMember(5)]
		public required string GameType { get; set; }

		[ProtoMember(6)]
		public required string MapName { get; set; }

		[ProtoMember(8)]
		public required string[] Resources { get; set; }

		[ProtoMember(9)]
		public required string Server { get; set; }

		[ProtoMember(10)]
		public required Player[] Players { get; set; }

		[ProtoMember(11)]
		public required int IconVersion { get; set; }

		[ProtoMember(12)]
		public required Dictionary<string, string> Vars { get; set; }

		[ProtoMember(16)]
		public required bool EnhancedHostSupport { get; set; }

		[ProtoMember(17)]
		public required int UpvotePower { get; set; }

		[ProtoMember(18)]
		public required string[] ConnectEndpoints { get; set; }

		[ProtoMember(19)]
		public required int BurstPower { get; set; }
	}
}
