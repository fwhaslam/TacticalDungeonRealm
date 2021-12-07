//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Views {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Puzzle;
	using Realm.Tools;

	using Verbose.Utility;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	//using static Utility.AssertStringLinesUtility;
	using static Verbose.Utility.VerboseAsserts;

	[TestClass]
	public class FloodFillViewTest {

		[TestMethod]
		public void Constructor() {

			var map = PuzzleMapTest.Sample();
			
			var result = new FloodFillView( map.Places );

			AreEqual( 25, result.View.Length );

		}

		
		[TestMethod]
		public void Reset() {

			var map = PuzzleMapTest.Sample();
			var work = new FloodFillView( map.Places );

			// invocation
			work.ResetValues( 3 );

			// assertion
			StringsAreEqual("FloodFillView[\n"+
				"      3  3  3  3  3\n"+
				"      3  3  3  3  3\n"+
				"      3  3  3  3  3\n"+
				"      3  3  3  3  3\n"+
				"      3  3  3  3  3\n"+
				"]", work.ToDisplay() );
		}

		[TestMethod]
		public void ToDisplay() {

			var map = PuzzleMapTest.Sample();
			var work = new FloodFillView( map.Places);


			// invocation
			var result = work.ToDisplay();

			// assertion
			StringsAreEqual( "FloodFillView[\n"+
				"     -1 -1 -1 -1 -1\n"+
				"     -1 -1 -1 -1 -1\n"+
				"     -1 -1 -1 -1 -1\n"+
				"     -1 -1 -1 -1 -1\n"+
				"     -1 -1 -1 -1 -1\n"+
				"]", result );
		}


	}
}
