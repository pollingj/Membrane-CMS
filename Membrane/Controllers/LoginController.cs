using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Test;
using Castle.MonoRail.Views.Spark;
using Membrane.Commons.SparkExtensions;
using Membrane.Commons.Web.Spark;
using Membrane.Commons.Wrappers.Interfaces;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities.Enums;
using Spark;
using Spark.FileSystem;

namespace Membrane.Controllers
{
	/// <summary>
	/// Controller to handle all of the Login actions
	/// </summary>
	public class LoginController : BaseController
	{
		private readonly IAuthenticationService authenticationService;
		private readonly IPluginsService pluginsService;
		private readonly ICultureService cultureService;
		private readonly IFormsAuthentication formsAuthentication;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="authenticationService">The <see cref="AuthenticationService"/></param>
		/// <param name="pluginsService">The <see cref="Core.Services.PluginsService"/></param>
		/// <param name="cultureService">The <see cref="Core.Services.CultureService"/></param>
		/// <param name="formsAuthentication">The <see cref="FormsAuthentication"/> wrapper</param>
		public LoginController(IAuthenticationService authenticationService, IPluginsService pluginsService, ICultureService cultureService, IFormsAuthentication formsAuthentication)
		{
			this.authenticationService = authenticationService;
			this.pluginsService = pluginsService;
			this.cultureService = cultureService;
			this.formsAuthentication = formsAuthentication;
		}

		/// <summary>
		/// The Initial Index action - registers all of the current plugins
		/// </summary>
		public void Index()
		{
			// Some spiking code for use later on
			/*var settings = new SparkSettings().SetPageBaseType(typeof(SparkTemplateBase));
			var templates = new InMemoryViewFolder();
			var engine = new SparkViewEngine(settings)
			             	{
			             		ViewFolder = templates,
								ExtensionFactory = new MembraneSparkExtensionsFactory()
							};
			
			templates.Add("test.spark", "<var names=\"new [] {'alpha', 'beta', 'gamma'}\"/><p>Test</p><news><p>testing</p></news>");
			templates.Add("test2.spark", "<news><p>testing2</p></news>");
			

			var descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate("test.spark").AddTemplate("test2.spark");
			
			var entry = engine.CreateEntry(descriptor);
			
			var view = (SparkTemplateBase)entry.CreateInstance();
			
			var writer = new StringWriter();
			try
			{
				view.RenderView(writer);
			}
			finally
			{
				engine.ReleaseInstance(view);
			}
			
			PropertyBag["content"] = writer;
			*/

			pluginsService.RegisterInstalledPlugins(ConfigurationManager.AppSettings["plugins.path"]);
		}

		/// <summary>
		/// Login action for when the user posts their login credentials
		/// </summary>
		/// <param name="authenticationRequest">The <see cref="AuthenticationRequestDTO"/></param>
		[AccessibleThrough(Verb.Post)]
		public void Login([DataBind("login")]AuthenticationRequestDTO authenticationRequest)
		{
			var authenticatedUser = authenticationService.AuthenticateUser(authenticationRequest);

			if (authenticatedUser.Id != Guid.Empty)
			{
				createAuthenticationTicket(authenticatedUser);

				// Redirect to relevant page based on usertype
				switch (authenticatedUser.Type)
				{
					case UserType.Administrator:
						Redirect("Administrator", "Home", "Index");
						break;
					case UserType.User:
						Redirect("Home", "Index");
						break;
				}
				setDefaultCulture();
			}
			else
			{
				Flash["error"] = "Username or Password not recognised";
				RedirectToReferrer();	
			}
		}

		private void setDefaultCulture()
		{
			Session["culture"] = cultureService.GetDefaultCulture();
		}

		private void createAuthenticationTicket(AuthenticatedUserDTO user)
		{
			// Set up user
			var ticket = new FormsAuthenticationTicket(1, user.Id.ToString(), DateTime.Now,
																			 DateTime.Now.AddMinutes(30), true,
																			 user.Type.ToString(),
																			 FormsAuthentication.FormsCookiePath);

			var hash = formsAuthentication.Encrypt(ticket);
			var cookie = new HttpCookie(formsAuthentication.FormsCookieName, hash);

			if (ticket.IsPersistent)
				cookie.Expires = ticket.Expiration;

			Context.CurrentUser = user;
			Context.Session["user"] = user;
		}
	}
}