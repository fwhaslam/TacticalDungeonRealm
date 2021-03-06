//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Enums {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class FlagEnumTraitsTest {

		[TestMethod]
		public void Count() {
			AreEqual( 17, FlagEnumTraits.Count() );
		}

		[TestMethod]
		public void FromSymbolMap() {
			AreEqual( 17, FlagEnumTraits.FromSymbolMap.Count() );
		}

		[TestMethod]
		public void ToSymbolMap() {
			AreEqual( 17, FlagEnumTraits.FromSymbolMap.Count() );
		}

		[TestMethod]
		public void Symbol() { 
		
			AreEqual( 'T', FlagEnumTraits.Symbol( FlagEnum.Switch) );
			AreEqual( '.', FlagEnumTraits.Symbol( FlagEnum.None) );
		}

		[TestMethod]
		public void FromSymbol() { 
		
			AreEqual( FlagEnum.Switch, FlagEnumTraits.FromSymbol( 'T' ) );
			AreEqual( FlagEnum.None, FlagEnumTraits.FromSymbol( '.' ) );
		}

	}

}
