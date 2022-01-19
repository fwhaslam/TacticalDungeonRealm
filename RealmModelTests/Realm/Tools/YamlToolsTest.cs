

namespace Realm.Tools {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Puzzle;

	using static Verbose.Utility.VerboseAsserts;

	[TestClass]
	public class YamlToolsTest {

		[TestMethod]
		public void ToYamlString() {

			var what = new Agent();

			// invocation
			var result = YamlTools.ToYamlString( what );

			// assertions
			StringsAreEqual( "name: Peasant\n"+
					"face: North\n"+
					"status: Alert\n"+
					"faction: 0\n"+
					"", result );
		}
	}
}
