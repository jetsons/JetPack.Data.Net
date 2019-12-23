using System;
using System.Collections.Generic;
using System.Text;
using Jetsons.JetPack;

namespace Jetsons.CSV {

	public class CsvResults<T> {

		public bool Success;

		/// <summary>
		/// Headers that were picked up from the first row of the CSV, or the headers that were given by the caller via columnProps.
		/// </summary>
		public List<string> Headers = new List<string>();

		/// <summary>
		/// Strongly typed data of the CSV records
		/// </summary>
		public List<T> Data = new List<T>();

	}

	public enum CsvHeaders {

		/// <summary>
		/// The headers are surely on the first row of the CSV
		/// </summary>
		FirstRow,

		/// <summary>
		/// There are surely no headers in the CSV
		/// </summary>
		None,

		/// <summary>
		/// Auto-detect if there are headers on the first row of the CSV
		/// </summary>
		AutoDetect
	}

}