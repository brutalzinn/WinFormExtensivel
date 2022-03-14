using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinFormExtensivel.Plugins.Message
{
	/// <summary>
	/// Class all emitter types inherit from
	/// </summary>
	public abstract class BaseEmitter
	{
		internal readonly string Event;
		internal readonly Guid Id;


		internal BaseEmitter(string @event)
		{
			Id = Guid.NewGuid();
			Event = @event;
		}

		internal void Invoke(SocketMessage arg)
		{
			Invoke(arg.Content);

		}
		internal abstract void Invoke(object arg);
	}

	/// <summary>
	/// Parameterless Emitter
	/// </summary>
	public sealed class Emitter : BaseEmitter
	{

		internal Action Body { get; set; }

		internal Emitter(string @event, Action body) : base(@event)
		{
			Body = body;
		}

		internal override void Invoke(object arg)
		{
			Body();
		}
	}

	/// <summary>
	/// Emitter with 1 Parameter
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Emitter<T> : BaseEmitter
	{

		internal Action<T> Body { get; set; }

		internal Emitter(string @event, Action<T> body) : base(@event)
		{
			Body = body;
		}

		internal override void Invoke(object arg)
		{
		
			Body((T)arg);
		}
	}
}
