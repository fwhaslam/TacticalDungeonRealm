//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Puzzle {
	
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Realm.Tools.YamlTools;
	using static Verbose.Utility.VerboseAsserts;

	[TestClass]
	public class PlaceTest {

		[TestMethod]
		public void Constructor_int_int() {

			// invocation
			var result = new Place( 5, 3 );

			// assertions
			StringsAreEqual( "Where:\n"+
					"  x: 5\n"+
					"  y: 3\n"+
					"Height: One\n"+
			  		"AgentId: -1\n"+
					"", ToYamlString(result) );
		}

		[TestMethod]
		public void Constructor_copy() {

			var place =  new Place( 5, 3 );

			// invocation
			var result = new Place( place );

			// assertions
			StringsAreEqual( "Where:\n"+
					"  x: 5\n"+
					"  y: 3\n"+
					"Height: One\n"+
		    		"AgentId: -1\n"+
					"", ToYamlString(result) );
		}

		[TestMethod]
		public void Clone() {

			var place =  new Place( 5, 3 );

			// invocation
			var result = (Place)place.Clone();

			// assertions
			StringsAreEqual( "Where:\n"+
					"  x: 5\n"+
					"  y: 3\n"+
					"Height: One\n"+
    				"AgentId: -1\n"+
					"", ToYamlString(result) );
		}
	}
}
