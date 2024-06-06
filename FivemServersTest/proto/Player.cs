using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivemServersTest.proto
{
	[ProtoContract]
	public class Player
	{
		[ProtoMember(1)]
		public string Name { get; set;}

		[ProtoMember(2)]
		public string[] Identifiers { get; set; }

		[ProtoMember(3)]
		public string Endpoint { get; set; }

		[ProtoMember(4)]
		public int Ping { get; set; }

		[ProtoMember(5)]
		public int Id { get; set; }
	}
}
