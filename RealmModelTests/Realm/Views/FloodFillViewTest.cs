//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Views {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Puzzle;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Utility.AssertStringLinesUtility;

	[TestClass]
	public class FloodFillViewTest {

		[TestMethod]
		public void Constructor() {
			
			var result = new FloodFillView();

			IsNull( result.Values );

		}

		[TestMethod]
		public void Values_getSet() {

			var work = new FloodFillView();
			Grid<int> grid = new Grid<int>(1,1);
			grid[0,0] = 123;

			// invocation
			work.Values = grid;

			// assertions
			AreEqual( work.Values, grid );
			AreEqual( 123, work.Values[0,0] );
			AreEqual( 1, work.Values.Wide );
			AreEqual( 1, work.Values.Tall );
		}
		
		[TestMethod]
		public void Reset() {

			var work = new FloodFillView();
			work.Reset(2,2);


			// invocation
			var result = work.ToDisplay();

			// assertion
Console.Out.WriteLine("DISPLAY="+result);
			StringLinesAreEqual("FloodFillView[\n"+
				"    -1-1\n"+
				"    -1-1\n"+
				"]", result );
		}

		[TestMethod]
		public void ToDisplay() {

			var work = new FloodFillView();
			work.Reset(3,3);
			work.Values[1,1] = 123;


			// invocation
			var result = work.ToDisplay();

			// assertion
			StringLinesAreEqual( "FloodFillView[\n"+
				"      -1  -1  -1\n"+
				"      -1 123  -1\n"+
				"      -1  -1  -1\n"+
				"]", result );
		}


	}
}
