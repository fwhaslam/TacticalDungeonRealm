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
	public class DirEnumTraitsTest {

		[TestMethod]
		public void DX() { 
		
			AreEqual( 0, DirEnumTraits.DX( DirEnum.North) );
			AreEqual( 1, DirEnumTraits.DX( DirEnum.NorthEast) );
			AreEqual( 1, DirEnumTraits.DX( DirEnum.East) );
			AreEqual( 1, DirEnumTraits.DX( DirEnum.SouthEast) );

			AreEqual( 0, DirEnumTraits.DX( DirEnum.South) );
			AreEqual( -1, DirEnumTraits.DX( DirEnum.SouthWest) );
			AreEqual( -1, DirEnumTraits.DX( DirEnum.West) );
			AreEqual( -1, DirEnumTraits.DX( DirEnum.NorthWest) );
		}

		[TestMethod]
		public void DY() { 
		
			AreEqual( 1, DirEnumTraits.DY( DirEnum.North) );
			AreEqual( 1, DirEnumTraits.DY( DirEnum.NorthEast) );
			AreEqual( 0, DirEnumTraits.DY( DirEnum.East) );
			AreEqual( -1, DirEnumTraits.DY( DirEnum.SouthEast) );

			AreEqual( -1, DirEnumTraits.DY( DirEnum.South) );
			AreEqual( -1, DirEnumTraits.DY( DirEnum.SouthWest) );
			AreEqual( 0, DirEnumTraits.DY( DirEnum.West) );
			AreEqual( 1, DirEnumTraits.DY( DirEnum.NorthWest) );
		}

		[TestMethod]
		public void Orth() { 
		
			AreEqual( true, DirEnumTraits.Orth( DirEnum.North) );
			AreEqual( false, DirEnumTraits.Orth( DirEnum.NorthEast) );
			AreEqual( true, DirEnumTraits.Orth( DirEnum.East) );
			AreEqual( false, DirEnumTraits.Orth( DirEnum.SouthEast) );

			AreEqual( true, DirEnumTraits.Orth( DirEnum.South) );
			AreEqual( false, DirEnumTraits.Orth( DirEnum.SouthWest) );
			AreEqual( true, DirEnumTraits.Orth( DirEnum.West) );
			AreEqual( false, DirEnumTraits.Orth( DirEnum.NorthWest) );
		}

		[TestMethod]
		public void Steps() { 
		
			AreEqual( 2, DirEnumTraits.Steps( DirEnum.North) );
			AreEqual( 3, DirEnumTraits.Steps( DirEnum.NorthEast) );
			AreEqual( 2, DirEnumTraits.Steps( DirEnum.East) );
			AreEqual( 3, DirEnumTraits.Steps( DirEnum.SouthEast) );

			AreEqual( 2, DirEnumTraits.Steps( DirEnum.South) );
			AreEqual( 3, DirEnumTraits.Steps( DirEnum.SouthWest) );
			AreEqual( 2, DirEnumTraits.Steps( DirEnum.West) );
			AreEqual( 3, DirEnumTraits.Steps( DirEnum.NorthWest) );
		}


	}
}
