//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Views {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Puzzle;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class GridStepperTest {

		[TestMethod]
		public void Constructor_doSomeSteps() {

			var map = PuzzleMapFactory.SimpleTerrain( 3,3 );

			// invocation
			var result = GridStepper<Place>.Build( map.Places );

			// stepping around
			var place = map.Places[1,1];

			var spot = result.Map[place.Where];
			AreEqual( "Where(1,1)", spot.Where.ToString() );

			// step north
			spot = result.Adj[ spot.Where ][ (int)DirEnum.North ].Dest;
			AreEqual( "Where(1,2)", spot.Where.ToString() );

			// cannot step north
			IsNull( result.Adj[ spot.Where ][ (int)DirEnum.North ] );

			// step south
			spot = result.Adj[ spot.Where ][ (int)DirEnum.South ].Dest;
			AreEqual( "Where(1,1)", spot.Where.ToString() );

			// step west
			spot = result.Adj[ spot.Where ][ (int)DirEnum.West ].Dest;
			AreEqual( "Where(0,1)", spot.Where.ToString() );

			// step east
			spot = result.Adj[ spot.Where ][ (int)DirEnum.East ].Dest;
			AreEqual( "Where(1,1)", spot.Where.ToString() );

		}
	}
}
