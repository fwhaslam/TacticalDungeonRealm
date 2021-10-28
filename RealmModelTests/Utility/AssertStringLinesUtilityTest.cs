//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Utility {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	using static Utility.AssertStringLinesUtility;


	/// <summary>
	/// Compare strings based on internal line structure.  
	/// All end of line structures are discarded and are considered equivalent.
	/// </summary>
	[TestClass]
	public class AssertStringLinesUtilityTest {


		[TestMethod]
		public void StringLinesAreEqual_arrays_success() {

			string[] first = {"one","two" };
			string[] second = {"one","two"};

			StringLinesAreEqual( first, second );
		}

		[TestMethod]
		[ExpectedException(typeof(AssertFailedException))]
		public void StringLinesAreEqual_arrays_failure() {

			string[] first = {"one","two" };
			string[] second = {"one","two", "three" };

			StringLinesAreEqual( first, second );
		}

		[TestMethod]
		public void CompareStringLines_success() {

			string[] first = {"one","two" };
			string[] second = {"one","two"};

			// invocation
			string result = CompareStringLines( first, second );

			// assertions
			IsNull( result );
		}

		[TestMethod]
		public void CompareStringLines_fail_forExpectLength() {

			string[] first = {"one","two", "three" };
			string[] second = {"one","two" };

			// invocation
			string result = CompareStringLines( first, second );

			// assertions
			AreEqual( "Strings do not match at line[2]\nOne space is larger than the other", result );
		}
	}
}
