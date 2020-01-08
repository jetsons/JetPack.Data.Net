using Jetsons.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Objects {

		/// <summary>
		/// Serialize the given data into JSON representation and return the JSON string.
		/// Optionally pretty prints the string with newlines and tabs to improve readabilitiy.
		/// </summary>
		public static string EncodeJSON<T>(this T data, bool prettyPrint = false) {
			var json = JsonSerializer.Serialize<T>(data);
			if (prettyPrint) {
				return JsonSerializer.PrettyPrint(json);
			}
			else {
				return json.DecodeUTF8();
			}
		}

		/// <summary>
		/// Deserialize the given JSON string into a strongly-typed data representation.
		/// </summary>
		public static T DecodeJSON<T>(this string json) {
			var data = JsonSerializer.Deserialize<T>(json);
			return data;
		}

		/// <summary>
		/// Deserialize the given JSON string into a dynamically-typed data representation.
		/// </summary>
		public static dynamic DecodeJSON(this string json) {
			return JsonSerializer.Deserialize<dynamic>(json);
		}

	}
}
