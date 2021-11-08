

namespace Realm.Tools {
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


	[TestClass]
	public class GridTest {

		static public Grid<int> Sample() {
			var sample = new Grid<int>( 2, 3 );

			sample[0] = 0;
			sample[1] = 1;
			sample[2] = 2;
			sample[3] = 3;
			sample[4] = 4;
			sample[5] = 5;

			return sample;
		}

		[TestMethod]
		public void Constructor_1D() {

			// invocation
			var result = Sample();

			// assertions
			AreEqual( 2, result.Wide );
			AreEqual( 3, result.Tall );

			AreEqual( 0, result[0] );
			AreEqual( 3, result[3] );
			AreEqual( 5, result[5] );
		}

		[TestMethod]
		public void Values_2D() {

			var work = Sample();

			// invocations
			work[1,0] = 10;
			work[0,2] = 20;

			// assertions
			AreEqual( 2, work.Wide );
			AreEqual( 3, work.Tall );

			AreEqual( 10, work[1,0] );
			AreEqual( 20, work[0,2] );

			AreEqual( 0, work[0,0] );
			AreEqual( 5, work[1,2] );
		}

		[TestMethod]
		public void Enumerates() {

			var work = Sample();

			// invocation
			int sum = 0;
			foreach( var element in work ) sum += element;

			// assertions
			AreEqual( 15, sum );
		}
	}
}
