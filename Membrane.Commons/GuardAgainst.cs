using System;

namespace Membrane.Commons
{
	/// <summary>
	/// Provides a number of guard statements to protect methods against invalid input parameters
	/// </summary>
	public static class GuardAgainst
	{
		/// <summary>
		/// Guards against a method parameter being null
		/// </summary>
		/// <param name="param">The parameter to validate</param>
		/// <param name="paramName">The name of the parameter to include in the exception (if thrown)</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the parameter is null</exception>
		public static void ArgumentNull(object param, string paramName)
		{
			if (param == null)
				throw new ArgumentNullException(paramName);
		}

		/// <summary>
		/// Guards against a method string parameter being null or empty
		/// </summary>
		/// <param name="param">The string parameter to validate</param>
		/// <param name="paramName">The name of the parameter to include in the exception (if thrown)</param>
		/// <exception cref="System.ArgumentNullException">Thrown if the parameter is null</exception>
		/// <exception cref="System.ArgumentException">Thrown if the parameter is empty</exception>
		public static void ArgumentNullOrEmpty(string param, string paramName)
		{
			if (param == null)
				throw new ArgumentNullException(paramName);

			if (param == string.Empty)
				throw new ArgumentException(string.Format("The parameter {0} cannot be empty.", paramName), paramName);
		}

		/// <summary>
		/// Guards against a method Guid parameter being empty
		/// </summary>
		/// <param name="param">The guid parameter to validate</param>
		/// <param name="paramName">The name of the parameter to include in the exception (if thrown)</param>
		/// <exception cref="System.ArgumentException">Thrown if the parameter is empty</exception>
		public static void ArgumentEmpty(Guid param, string paramName)
		{
			if (param == Guid.Empty)
				throw new ArgumentException(string.Format("The parameter {0} cannot be empty.", paramName), paramName);
		}

		/// <summary>
		/// Guards against a method integer parameter being outside a given range
		/// </summary>
		/// <param name="param">The string parameter to validate</param>
		/// <param name="paramName">The name of the parameter to include in the exception (if thrown)</param>
		/// <param name="minimum">The minimum integer value that is valid for the given parameter</param>
		/// <param name="maximum">The maximum integer value that is valid for the given parameter</param>
		/// <exception cref="System.ArgumentException">Thrown if the parameter is outside the given range</exception>
		public static void ArgumentOutsideRange(int param, string paramName, int minimum, int maximum)
		{
			if (param < minimum || param > maximum)
				throw new ArgumentException(string.Format("The parameter {0} must be between {1} and {2} (inclusive).", paramName, minimum, maximum), paramName);
		}
	}

}