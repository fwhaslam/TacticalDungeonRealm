using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {

	/// <summary>
	/// Generic version of the IClonable interface.
	/// Also, using the more modern 'If' suffix for interface.
	/// 
	/// NOTE: Implementations must be DEEP COPY.
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface CloneableIf<T> {
		
		T Clone();

	}
}
