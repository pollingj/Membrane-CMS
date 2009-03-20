using Castle.Windsor;

namespace Membrane.Commons
{
	public interface IMembranePlugin
	{
		string Name { get; }

	    void Initialize();
		void RegisterComponents(IWindsorContainer container);
	}

}