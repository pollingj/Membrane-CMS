using System.Configuration;

namespace Membrane.Tests.Acceptance
{
	public class AcceptanceTestBase
	{	
		protected string server;
		protected int port;
		protected string extension;
		protected string productName;

		protected AcceptanceTestBase()
		{
			server = ConfigurationManager.AppSettings["server"] ?? "localhost";
			extension = ConfigurationManager.AppSettings["extension"] ?? "rails";
			extension = extension.Length == 0 ? "" : "." + extension;
			port = 80;

			if (ConfigurationManager.AppSettings["port"] != null)
			{
				port = int.Parse(ConfigurationManager.AppSettings["port"]);
			}
		}



		protected string BuildUrl(string controller, string action)
		{
			return string.Format("http://{0}:{1}/{2}/{3}{4}", server, port, controller, action, extension);
		}

	}
}