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
		public string EndPoint { get; set; }

		[ProtoMember(2)]
		public byte[] Icon { get; set; }

		[ProtoMember(3)]
		public int IconVersion { get; set; }
	}
}
