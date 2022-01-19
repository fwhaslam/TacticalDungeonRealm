//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Views.Stepper {

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
			var result = GridStepper.Build( map.Places, -1 );

			// stepping around
			var place = map.Places[1,1];

			var spot = result[place.Where];
			AreEqual( "Where(1,1)", spot.Here.Where.ToDisplay() );

			// step north
			spot = spot[ (int)DirEnum.North ].There;
			AreEqual( "Where(1,2)", spot.Here.Where.ToDisplay() );

			// cannot step north
			IsNull( spot[ (int)DirEnum.North ] );

			// step south
			spot = spot[ (int)DirEnum.South ].There;
			AreEqual( "Where(1,1)", spot.Here.Where.ToDisplay() );

			// step west
			spot = spot[ (int)DirEnum.West ].There;
			AreEqual( "Where(0,1)", spot.Here.Where.ToDisplay() );

			// step east
			spot = spot[ (int)DirEnum.East ].There;
			AreEqual( "Where(1,1)", spot.Here.Where.ToDisplay() );

		}
	}
}
