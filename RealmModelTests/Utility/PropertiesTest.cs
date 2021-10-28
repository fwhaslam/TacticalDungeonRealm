//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Utility {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System.Collections.Generic;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class PropertiesTest : Dictionary<string,string> {
		
		[TestMethod]
		public void Constructor() {

			var result = new Properties();

			Assert.AreEqual( 0, result.Count );
		}

		
		[TestMethod]
		public void Constructor_withPath() {

			// invocation
			var result = new Properties("../../../../TeamSettings/testing.properties");

			// assertion
			Assert.AreEqual( 4, result.Count );

			AreEqual( "value.one", result["field.one"] );
			AreEqual( "", result["Field.Two"] );
			AreEqual( "some kind of longer string, but no wraparound", result["FIELD.THREE"] );
			AreEqual( "", result["end.of.file.key"] );
		}
		
		static readonly string PARSE_BODY = "#comment\n" +
			"\n" +
			"unparsable line\n"+
			"field.one= value.one \n" +
			" Field.Two =\n"+
			"!another comment\n"+
			"\tFIELD.THREE\t=";
		
		[TestMethod]
		public void Parse() {
			
			var props = new Properties();

			// invocation
			props.Parse( PARSE_BODY );

			// assertions
			AreEqual( 3, props.Count );

			AreEqual( "value.one", props["field.one"] );
			AreEqual( "", props["Field.Two"] );
			AreEqual( "", props["FIELD.THREE"] );
		}

	}
}
