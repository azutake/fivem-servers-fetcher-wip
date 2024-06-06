using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivemServersTest.proto
{
	[ProtoContract]
	public class Server
	{
		[ProtoMember(1)]
		public string EndPoint { get; set; }

		[ProtoMember(2)]
		public ServerData Data { get; set; }
	}
}
