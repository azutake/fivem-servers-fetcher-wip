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
		public required string Name { get; set;}

		[ProtoMember(2)]
		public required string[] Identifiers { get; set; }

		[ProtoMember(3)]
		public required string Endpoint { get; set; }

		[ProtoMember(4)]
		public required int Ping { get; set; }

		[ProtoMember(5)]
		public required int Id { get; set; }
	}
}
