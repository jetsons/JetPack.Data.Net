﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Jetsons.CSV;
using Jetsons.JSON;
using Jetsons.MsgPack;
using DynamicObject = System.Collections.Generic.Dictionary<string, object>;
using DynamicList = System.Collections.Generic.List<object>;

namespace Jetsons.JetPack
{

    public static class Files
    {

		/// <summary>
		/// Parse a CSV file and convert it into a List of strongly typed Objects.
		/// Never returns null.
		/// If the first line is not headers, and you don't supply any columnProps, then the names of the columns are assumed.
		/// </summary>
		/// <param name="filePath">CSV file path</param>
		/// <param name="headers">Read the first line as the column headers?</param>
		/// <param name="columnProps">Provide the properties per column, if known</param>
		/// <param name="delimiter">Uses the given delimiter</param>
		/// <returns></returns>
		public static CsvResults<T> LoadCSV<T>(this string filePath, CsvHeaders headers, List<string> columnProps = null, char delimiter = ',') {
			var text = filePath.LoadTextFile();
			if (text == null) {
				return new CsvResults<T> { Success = false };
			}
			return text.DecodeCSV<T>(headers, columnProps, delimiter);
		}

		/// <summary>
		/// Parse a JSON file and convert it into an strongly-typed object.
		/// Returns null if the file does not exist.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
		/// </summary>
		/// <param name="filePath">JSON file path</param>
		/// <param name="resolver">Settings to use while decoding</param>
		/// <returns></returns>
		public static T LoadJSON<T>(this string filePath, IJsonFormatterResolver resolver = null) {
			var file = filePath.LoadBytes();
			if (file == null) {
				return default(T);
			}
			if (resolver == null) {
				return JsonSerializer.Deserialize<T>(file);
			}
			else {
				return JsonSerializer.Deserialize<T>(file, 0, resolver);
			}
		}

		/// <summary>
		/// Parse a JSON file and convert it into a dynamically-typed object.
		/// Returns null if the file does not exist.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
		/// </summary>
		/// <param name="filePath">JSON file path</param>
		/// <param name="resolver">Settings to use while decoding</param>
		/// <returns></returns>
		public static DynamicObject LoadJSON(this string filePath, IJsonFormatterResolver resolver = null) {
			var file = filePath.LoadBytes();
			if (file == null) {
				return null;
			}
			if (resolver == null) {
				return JsonSerializer.Deserialize<DynamicObject>(file);
			}
			else {
				return JsonSerializer.Deserialize<DynamicObject>(file, 0, resolver);
			}
		}

		/// <summary>
		/// Parse a MessagePack file and convert it into a strongly-typed object.
		/// Returns null if the file does not exist.
		/// Powered by the fastest MessagePack Serializer (MessagePack-CSharp).
		/// </summary>
		/// <param name="filePath">MessagePack file path</param>
		/// <param name="resolver">Settings to use while decoding</param>
		/// <returns></returns>
		public static T LoadMsgPack<T>(this string filePath, IFormatterResolver resolver = null) {
			var file = filePath.LoadBytes();
			if (file == null) {
				return default(T);
			}
			if (resolver == null) {
				return MessagePackSerializer.Deserialize<T>(file);
			}
			else {
				return MessagePackSerializer.Deserialize<T>(file, resolver);
			}
		}
		
		/// <summary>
		/// Parse a MessagePack file and convert it into a dynamically-typed object.
		/// Returns null if the file does not exist.
		/// Powered by the fastest MessagePack Serializer (MessagePack-CSharp).
		/// </summary>
		/// <param name="filePath">MessagePack file path</param>
		/// <param name="resolver">Settings to use while decoding</param>
		/// <returns></returns>
		public static DynamicObject LoadMsgPack(this string filePath, IFormatterResolver resolver = null) {
			var file = filePath.LoadBytes();
			if (file == null) {
				return null;
			}
			if (resolver == null) {
				return MessagePackSerializer.Deserialize<DynamicObject>(file);
			}
			else {
				return MessagePackSerializer.Deserialize<DynamicObject>(file, resolver);
			}
		}
		
		/// <summary>
		/// Saves the given objects as a serialized MessagePack file.
		/// Powered by the fastest MessagePack Serializer (MessagePack-CSharp).
		/// </summary>
		/// <param name="data">Objects</param>
		/// <param name="fileName">File path, overwritten if it already exists</param>
		/// <param name="createFolder">Create the parent folder?</param>
		/// <param name="resolver">Settings to use while encoding</param>
		public static void SaveToMsgPack<T>(this T data, string fileName, bool createFolder = true, IFormatterResolver resolver = null) {
			var bytes = resolver == null ?
				MessagePackSerializer.Serialize<T>(data) : MessagePackSerializer.Serialize<T>(data, resolver);
			if (bytes != null) {
				bytes.SaveToFile(fileName, createFolder);
			}
			else {
				fileName.DeleteFile();
			}
		}

		/// <summary>
		/// Saves the given objects as a serialized JSON file.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
		/// </summary>
		/// <param name="data">Objects</param>
		/// <param name="fileName">File path, overwritten if it already exists</param>
		/// <param name="createFolder">Create the parent folder?</param>
		/// <param name="resolver">Settings to use while encoding</param>
		public static void SaveToJSON<T>(this T data, string fileName, bool createFolder = true, IJsonFormatterResolver resolver = null) {
			var bytes = resolver == null ?
				JsonSerializer.Serialize<T>(data) : JsonSerializer.Serialize<T>(data, resolver);
			if (bytes != null) {
				bytes.SaveToFile(fileName, createFolder);
			}
			else {
				fileName.DeleteFile();
			}
		}
		
	}
}
