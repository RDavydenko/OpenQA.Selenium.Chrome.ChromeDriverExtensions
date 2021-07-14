using System;
using System.IO;
using System.IO.Compression;

namespace OpenQA.Selenium.Chrome.ChromeDriverExtensions
{
	/// <summary>
	/// Extensions for ChromeOptions (Selenium Driver)
	/// </summary>
	public static class ChromeOptionsExtensions
	{
		/// <summary>
		/// Add HTTP-proxy by <paramref name="userName"/> and <paramref name="password"/>
		/// </summary>
		/// <param name="options">Chrome options</param>
		/// <param name="host">Proxy host</param>
		/// <param name="port">Proxy port</param>
		/// <param name="userName">Proxy username</param>
		/// <param name="password">Proxy password</param>
		public static void AddHttpProxy(this ChromeOptions options, string host, int port, string userName, string password)
		{
			var manifest_json = File.ReadAllText("Templates/manifest.json");
			var background_js = ReplaceTemplates((File.ReadAllText("Templates/background.js")), host, port, userName, password);

			if (!Directory.Exists("Plugins"))
				Directory.CreateDirectory("Plugins");

			var guid = Guid.NewGuid().ToString();

			var manifestPath = $"Plugins/manifest_{guid}.json";
			var backgroundPath = $"Plugins/background_{guid}.js";
			var archiveFilePath = $"Plugins/proxy_auth_plugin_{guid}.zip";

			File.WriteAllText(manifestPath, manifest_json);
			File.WriteAllText(backgroundPath, background_js);

			using (var zip = ZipFile.Open(archiveFilePath, ZipArchiveMode.Create))
			{
				zip.CreateEntryFromFile(manifestPath, "manifest.json");
				zip.CreateEntryFromFile(backgroundPath, "background.js");
			}

			File.Delete(manifestPath);
			File.Delete(backgroundPath);

			options.AddExtension(archiveFilePath);
		}

		private static string ReplaceTemplates(string str, string host, int port, string userName, string password)
		{
			return str
				.Replace("{HOST}", host)
				.Replace("{PORT}", port.ToString())
				.Replace("{USERNAME}", userName)
				.Replace("{PASSWORD}", password);
		}
	}
}
