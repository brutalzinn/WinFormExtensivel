using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinFormExtensivel.Plugins.Message
{
	public class SocketMessage
	{
		public string Event { get; set; }

		public object Content { get; set; }

		[JsonIgnore]
		public Guid Id = Guid.NewGuid();

	}
}
