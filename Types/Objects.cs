using Jetsons.CSV;
using Jetsons.JSON;
using Jetsons.MsgPack;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicObject = System.Collections.Generic.Dictionary<string, object>;
using DynamicList = System.Collections.Generic.List<object>;

namespace Jetsons.JetPack {
	public static class Objects {

		/// <summary>
		/// Serialize the given data into JSON representation and return the JSON string.
		/// Optionally pretty prints the string with newlines and tabs to improve readability.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
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
		/// Serialize the given data into MessagePack representation and return the bytes.
		/// Powered by the fastest MessagePack Serializer (MessagePack-CSharp).
		/// </summary>
		public static byte[] EncodeMsgPack<T>(this T data) {
			return MessagePackSerializer.Serialize<T>(data);
		}

		/// <summary>
		/// Deserialize the given JSON string into a strongly-typed object.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
		/// </summary>
		public static T DecodeJSON<T>(this string json) {
			var data = JsonSerializer.Deserialize<T>(json);
			return data;
		}

		/// <summary>
		/// Deserialize the given JSON string into a dynamically-typed object.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
		/// </summary>
		public static DynamicObject DecodeJSON(this string json) {
			var data = JsonSerializer.Deserialize<DynamicObject>(json);
			return data;
		}

		/// <summary>
		/// Deserialize the given JSON data into a strongly-typed object.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
		/// </summary>
		public static T DecodeJSON<T>(this byte[] json) {
			var data = JsonSerializer.Deserialize<T>(json);
			return data;
		}

		/// <summary>
		/// Deserialize the given JSON data into a dynamically-typed object.
		/// Powered by the fastest Zero Allocation JSON Serializer (Utf8Json).
		/// </summary>
		public static DynamicObject DecodeJSON(this byte[] json) {
			var data = JsonSerializer.Deserialize<DynamicObject>(json);
			return data;
		}

		/// <summary>
		/// Deserialize the given MessagePack data into a strongly-typed object.
		/// Powered by the fastest MessagePack Serializer (MessagePack-CSharp).
		/// </summary>
		public static T DecodeMsgPack<T>(this byte[] json) {
			var data = MessagePackSerializer.Deserialize<T>(json);
			return data;
		}
		
		/// <summary>
		/// Deserialize the given MessagePack data into a dynamically-typed object.
		/// Powered by the fastest MessagePack Serializer (MessagePack-CSharp).
		/// </summary>
		public static DynamicObject DecodeMsgPack(this byte[] json) {
			var data = MessagePackSerializer.Deserialize<DynamicObject>(json);
			return data;
		}
		
		/// <summary>
		/// Parse a CSV file and convert it into a List of strongly typed Objects. Never returns null.
		/// If the first line is not headers, and you don't supply any columnProps,
		/// then the names of the columns are assumed.
		/// </summary>
		/// <param name="csv">CSV-formatted string</param>
		/// <param name="headers">Read the first line as the column headers?</param>
		/// <param name="columnProps">Provide the properties per column, if known</param>
		/// <param name="delimiter">Uses the given delimiter</param>
		public static CsvResults<T> DecodeCSV<T>(this string csv, CsvHeaders headers, List<string> columnProps = null, char delimiter = ',') {

			if (!csv.Exists()) {
				return new CsvResults<T>();
			}

			csv = csv.Trim();

			return new CsvDecoder<T>() {
				Csv = csv,
				Lines = csv.Lines(),
				Header = headers,
				ColumnProps = columnProps,
				Delimiter = delimiter
			}.DecodeString();
		}

		/// <summary>
		/// Compress the given folder and all its files into the target archive path.
		/// Optionally archives the files that match the given filter.
		/// Supported archives: Zip, GZip, 7-Zip, BZip, BZip2, LZip, Tar.
		/// </summary>
		public static bool ArchiveFolder(this string path, string targetPath, ArchiveType archive, CompressionType compression, bool recursive = true, string fileFilter = "*") {
			if (path.FolderExists()) {
				targetPath.DeleteFile();
				using (Stream stream = File.OpenWrite(targetPath)) {
					using (var writer = WriterFactory.Open(stream, archive, new WriterOptions(compression))) {
						writer.WriteAll(path, fileFilter, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Read any supported archive file and extract it to the given folder.
		/// The target folder is created and the files are overwritten if specified.
		/// Supported archives: Zip, GZip, 7-Zip, BZip, BZip2, LZip, Tar.
		/// </summary>
		public static bool ExtractArchive(this string path, string targetPath, bool overwrite = true) {

			// create the target folder
			targetPath.EnsureFolderExists(false);

			// open the zip and loop thru all files
			using (Stream stream = File.OpenRead(path)) {
				using (var reader = ReaderFactory.Open(stream)) {
					while (reader.MoveToNextEntry()) {
						if (!reader.Entry.IsDirectory) {

							// save the file to disk at the correct path
							reader.WriteEntryToDirectory(targetPath, new ExtractionOptions() {
								ExtractFullPath = true,
								Overwrite = overwrite
							});
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Read any supported archive file and extract the first file matching the filter, and save the file to disk.
		/// Returns true if the file was found and saved.
		/// Supported archives: Zip, GZip, 7-Zip, BZip, BZip2, LZip, Tar.
		/// </summary>
		public static bool ExtractFileFromArchive(this string path, string targetPath, Func<string, bool> fileFilter, bool overwrite = true) {

			// quickly exit if the file exists and we dont want to overwrite
			if (!overwrite && targetPath.FileExists()) {
				return false;
			}

			// open the zip and loop thru all files
			using (Stream stream = File.OpenRead(path)) {
				using (var reader = ReaderFactory.Open(stream)) {
					while (reader.MoveToNextEntry()) {
						if (!reader.Entry.IsDirectory) {

							// check if the file matches the filter
							var filePath = reader.Entry.Key;
							if (fileFilter(filePath) == true){

								// extract the file to disk
								var fs = new FileStream(targetPath, FileMode.Create);
								reader.WriteEntryTo(fs);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Read any supported archive file and find the first file matching the filter, and return the file data as bytes.
		/// Returns null if no matching file was found.
		/// Supported archives: Zip, GZip, BZip, BZip2, LZip, Tar.
		/// </summary>
		public static byte[] GetFileFromArchive(this string path, Func<string, bool> fileFilter) {
			
			// open the zip file and loop thru all files
			using (Stream stream = File.OpenRead(path)) {
				using (var reader = ReaderFactory.Open(stream)) {
					while (reader.MoveToNextEntry()) {
						if (!reader.Entry.IsDirectory) {

							// check if the file matches the filter
							var filePath = reader.Entry.Key;
							if (fileFilter(filePath) == true) {

								// extract the file to disk
								var ms = new MemoryStream();
								reader.WriteEntryTo(ms);
								return ms.ToBytes();
							}
						}
					}
				}
			}
			return null;
		}

	}
}
