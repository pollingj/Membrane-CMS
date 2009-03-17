using Castle.Windsor;

namespace Membrane.Commons
{
	public interface IWindsorPlugin
	{
		string Name { get; }
		void RegisterComponents(IWindsorContainer container);
	}

}