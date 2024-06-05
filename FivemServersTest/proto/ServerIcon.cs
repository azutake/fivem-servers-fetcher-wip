using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivemServersTest.proto
{
	[ProtoContract]
	public class ServerIcon
	{
		[ProtoMember(1)]
		public required string EndPoint { get; set; }

		[ProtoMember(2)]
		public required byte[] Icon { get; set; }

		[ProtoMember(3)]
		public required int IconVersion { get; set; }
	}
}
