namespace Membrane.Test
{
	/// <summary>
	/// Taken from Tim Escott.  Not put into use yet though.
	/// Basically the idea is to give base unit test class the ability
	/// to build valid and invalid objects.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IBuilder<T>
	{
		T BuildVaildObject();
		T BuildInVaildObject();
	}
}