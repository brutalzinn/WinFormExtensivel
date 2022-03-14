using System.Text.Json;

namespace SocketIo.Core.Serializers
{
	public sealed class JsonTester : ISerializer
	{
		private readonly JsonSerializerOptions SETTINGS = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		public T Deserialize<T>(byte[] array)
		{
			string json = System.Text.Encoding.UTF8.GetString(array);

			T obj = JsonSerializer.Deserialize<T>(json, SETTINGS);

			return obj;
		}

		public byte[] Serialize<T>(T obj)
		{
			string json = JsonSerializer.Serialize(obj, SETTINGS);
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

			return bytes;
		}
	}
}
