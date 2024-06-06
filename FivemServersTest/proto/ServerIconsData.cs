using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivemServersTest.proto
{
	[ProtoContract]
	public class ServerIconsData
	{
		[ProtoMember(1)]
		public ServerIcon Icons { get; set; }
	}
}
