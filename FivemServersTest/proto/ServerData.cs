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
		public int svMaxClients { get; set; }

		[ProtoMember(2)]
		public int Clients { get; set; }

		[ProtoMember(3)]
		public int Protocol { get; set; }

		[ProtoMember(4)]
		public string Hostname { get; set; }

		[ProtoMember(5)]
		public string GameType { get; set; }

		[ProtoMember(6)]
		public string MapName { get; set; }

		[ProtoMember(8)]
		public string[] Resources { get; set; }

		[ProtoMember(9)]
		public string Server { get; set; }

		[ProtoMember(10)]
		public Player[] Players { get; set; }

		[ProtoMember(11)]
		public int IconVersion { get; set; }

		[ProtoMember(12)]
		public Dictionary<string, string> Vars { get; set; }

		[ProtoMember(16)]
		public bool EnhancedHostSupport { get; set; }

		[ProtoMember(17)]
		public int UpvotePower { get; set; }

		[ProtoMember(18)]
		public string[] ConnectEndpoints { get; set; }

		[ProtoMember(19)]
		public int BurstPower { get; set; }
	}
}
