

namespace Realm.Tools {
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


	public class Tester : CloneableIf<Tester> {

		public Tester(string val) {
			this.Value = val;
		}
		public Tester(Tester src) {
			this.Value = src.Value;
		}

		public string Value { get; set; }

		public Tester Clone() {
			return new Tester(this);
		}
	}

	[TestClass]
	public class GridTest {

		static public Grid<Tester> Sample() {
			var sample = new Grid<Tester>( 2, 3 );

			sample[0] = new Tester("zero");
			sample[1] = new Tester("one");
			sample[2] = new Tester("two");
			sample[3] = new Tester("three");
			sample[4] = new Tester("four");
			sample[5] = new Tester("five");

			return sample;
		}

		[TestMethod]
		public void Constructor_2D() {

			// invocation
			var result = Sample();

			// assertions
			AreEqual( 2, result.Wide );
			AreEqual( 3, result.Tall );

			AreEqual( "zero", result[0].Value );
			AreEqual( "three", result[3].Value );
			AreEqual( "five", result[5].Value );
		}

		[TestMethod]
		public void Constructor_copy() {

			// invocation
			var result = new Grid<Tester>( Sample() );

			// assertions
			AreEqual( 2, result.Wide );
			AreEqual( 3, result.Tall );

			AreEqual( "zero", result[0].Value );
			AreEqual( "three", result[3].Value );
			AreEqual( "five", result[5].Value );
		}

		[TestMethod]
		public void Clone() {

			// invocation
			var result = (Grid<Tester>)Sample().Clone();

			// assertions
			AreEqual( 2, result.Wide );
			AreEqual( 3, result.Tall );

			AreEqual( "zero", result[0].Value );
			AreEqual( "three", result[3].Value );
			AreEqual( "five", result[5].Value );
		}

		[TestMethod]
		public void Values_2D() {

			var work = Sample();

			// invocations
			work[1,0].Value = "ten";
			work[0,2].Value = "twenty";

			// assertions
			AreEqual( 2, work.Wide );
			AreEqual( 3, work.Tall );

			AreEqual( "ten", work[1,0].Value );
			AreEqual( "twenty", work[0,2].Value );

			AreEqual( "zero", work[0,0].Value );
			AreEqual( "five", work[1,2].Value );
		}

		[TestMethod]
		public void Enumerates() {

			var work = Sample();

			// invocation
			string sum = "";
			foreach( var element in work ) sum += "/"+element.Value;

			// assertions
			AreEqual( "/zero/one/two/three/four/five", sum );
		}
	}
}
