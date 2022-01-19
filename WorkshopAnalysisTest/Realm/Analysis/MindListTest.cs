
namespace Realm.Analysis {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class MindListTest {

		[TestMethod]
		public void Constructor() {

			// invocation
			var result = new MindList();

			// assertions
			AreEqual( 2, result.Minds.Count );
			AreEqual( "Realm.Brain.ChaseBrain", result.Minds[0].GetType().ToString() );
			AreEqual( "Realm.Brain.CautiousBrain", result.Minds[1].GetType().ToString() );
		}
	}
}
