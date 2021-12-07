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
		public void DirIx() { 
		
			AreEqual( 0, DirEnumTraits.DirIx( DirEnum.North) );
			AreEqual( 1, DirEnumTraits.DirIx( DirEnum.NorthEast) );
			AreEqual( 2, DirEnumTraits.DirIx( DirEnum.East) );
			AreEqual( 3, DirEnumTraits.DirIx( DirEnum.SouthEast) );

			AreEqual( 4, DirEnumTraits.DirIx( DirEnum.South) );
			AreEqual( 5, DirEnumTraits.DirIx( DirEnum.SouthWest) );
			AreEqual( 6, DirEnumTraits.DirIx( DirEnum.West) );
			AreEqual( 7, DirEnumTraits.DirIx( DirEnum.NorthWest) );
		}

		[TestMethod]
		public void DX() { 
		
			AreEqual( 0, DirEnumTraits.DX( (int)DirEnum.North) );
			AreEqual( 1, DirEnumTraits.DX( (int)DirEnum.NorthEast) );
			AreEqual( 1, DirEnumTraits.DX( (int)DirEnum.East) );
			AreEqual( 1, DirEnumTraits.DX( (int)DirEnum.SouthEast) );

			AreEqual( 0, DirEnumTraits.DX( (int)DirEnum.South) );
			AreEqual( -1, DirEnumTraits.DX( (int)DirEnum.SouthWest) );
			AreEqual( -1, DirEnumTraits.DX( (int)DirEnum.West) );
			AreEqual( -1, DirEnumTraits.DX( (int)DirEnum.NorthWest) );
		}

		[TestMethod]
		public void DY() { 
		
			AreEqual( 1, DirEnumTraits.DY( (int)DirEnum.North) );
			AreEqual( 1, DirEnumTraits.DY( (int)DirEnum.NorthEast) );
			AreEqual( 0, DirEnumTraits.DY( (int)DirEnum.East) );
			AreEqual( -1, DirEnumTraits.DY( (int)DirEnum.SouthEast) );

			AreEqual( -1, DirEnumTraits.DY( (int)DirEnum.South) );
			AreEqual( -1, DirEnumTraits.DY( (int)DirEnum.SouthWest) );
			AreEqual( 0, DirEnumTraits.DY( (int)DirEnum.West) );
			AreEqual( 1, DirEnumTraits.DY( (int)DirEnum.NorthWest) );
		}

		[TestMethod]
		public void Orth() { 
		
			AreEqual( true, DirEnumTraits.Orth( (int)DirEnum.North) );
			AreEqual( false, DirEnumTraits.Orth( (int)DirEnum.NorthEast) );
			AreEqual( true, DirEnumTraits.Orth( (int)DirEnum.East) );
			AreEqual( false, DirEnumTraits.Orth( (int)DirEnum.SouthEast) );

			AreEqual( true, DirEnumTraits.Orth( (int)DirEnum.South) );
			AreEqual( false, DirEnumTraits.Orth( (int)DirEnum.SouthWest) );
			AreEqual( true, DirEnumTraits.Orth( (int)DirEnum.West) );
			AreEqual( false, DirEnumTraits.Orth( (int)DirEnum.NorthWest) );
		}

		[TestMethod]
		public void Steps() { 
		
			AreEqual( 2, DirEnumTraits.Steps( (int)DirEnum.North) );
			AreEqual( 3, DirEnumTraits.Steps( (int)DirEnum.NorthEast) );
			AreEqual( 2, DirEnumTraits.Steps( (int)DirEnum.East) );
			AreEqual( 3, DirEnumTraits.Steps( (int)DirEnum.SouthEast) );

			AreEqual( 2, DirEnumTraits.Steps( (int)DirEnum.South) );
			AreEqual( 3, DirEnumTraits.Steps( (int)DirEnum.SouthWest) );
			AreEqual( 2, DirEnumTraits.Steps( (int)DirEnum.West) );
			AreEqual( 3, DirEnumTraits.Steps( (int)DirEnum.NorthWest) );
		}

		[TestMethod]
		public void Turns() {
			
			for (int fx=0;fx<8;fx++) { 
				AreEqual( 0, DirEnumTraits.Turns( fx, fx ) );

				AreEqual( 1, DirEnumTraits.Turns( fx, (fx+1)%8 ) );
				AreEqual( 2, DirEnumTraits.Turns( fx, (fx+2)%8 ) );
				AreEqual( 3, DirEnumTraits.Turns( fx, (fx+3)%8 ) );
				AreEqual( 4, DirEnumTraits.Turns( fx, (fx+4)%8 ) );

				AreEqual( -3, DirEnumTraits.Turns( fx, (fx+5)%8 ) );
				AreEqual( -2, DirEnumTraits.Turns( fx, (fx+6)%8 ) );
				AreEqual( -1, DirEnumTraits.Turns( fx, (fx+7)%8 ) );
			}


		}
	}
}
