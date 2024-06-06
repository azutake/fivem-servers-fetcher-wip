using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivemServersTest.proto
{
	[ProtoContract]
	public class ServersData
	{
		[ProtoMember(1)]
		public Server[] Servers { get; set; }
	}
}
