//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Utility {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class SimpleToolsTest {

		[TestMethod]
		public void FixFilePath() {

			var sep = System.IO.Path.DirectorySeparatorChar;

			AreEqual( "some-path", SimpleTools.FixFilePath( "some-path") );
			AreEqual( "some"+sep+"long"+sep+"path", SimpleTools.FixFilePath( "some\\long\\path") );
			AreEqual( "some"+sep+"long"+sep+"path", SimpleTools.FixFilePath( "some/long/path") );

		}

		[TestMethod]
		public void WorkingDir() {

			IsTrue( SimpleTools.WorkingDir().Contains( @"TacticalDungeonRealm\RealmModelTests" ) );
			IsFalse( SimpleTools.WorkingDir().EndsWith( @"TacticalDungeonRealm\RealmModelTests" ) );

		}

		[TestMethod]
		public void ProjectDir() {

			IsTrue( SimpleTools.ProjectDir().EndsWith(@"TacticalDungeonRealm\RealmModelTests" ) );

		}

		[TestMethod]
		public void SolutionDir() {

			IsTrue( SimpleTools.SolutionDir().EndsWith(@"TacticalDungeonRealm" ) );

		}

	}
}
