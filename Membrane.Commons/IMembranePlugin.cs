using Castle.Windsor;

namespace Membrane.Commons
{
	public interface IMembranePlugin
	{
		string Name { get; }

	    void Initialize();
		void RegisterComponents(IWindsorContainer container);

		void Install();
		void Uninstall();
		void Upgrade();
	}

}