//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Utility {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class TeamSettingsTest : Properties {

		[TestMethod]
		public void Constructor() {
			
			var results = new TeamSettings();

			AreEqual( 2, results.Count );

			IsNotNull( results.GetPuzzlesFolder() );

		}

	}

}
