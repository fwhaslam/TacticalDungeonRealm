//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Puzzle {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Realm.Tools.YamlTools;
	using static Verbose.Utility.VerboseAsserts;
	using static Verbose.Utility.VerboseTools;

	[TestClass]
	public class AgentTest {

		[TestMethod]
		public void Constructor_int_int() {

			// invocation
			Agent result = new Agent( 3, 5 );

			// assertions
			StringsAreEqual( "name: Peasant\n"+
					"face: North\n"+
					"status: Alert\n"+
					"faction: 0\n"+
					"", ToYamlString(result) );

			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );
		}

		[TestMethod]
		public void Constructor_copy() {

			var src = new Agent( 3, 5 );
			src.Ident = 5;
			src.Type = AgentType.GHOST;
			src.Face = DirEnum.West;
			src.Status = StatusEnum.Sleep;

			// invocation
			Agent result = new Agent( src );

			// assertions
			StringsAreEqual( "name: Ghost\n"+
					"face: West\n"+
					"status: Sleep\n"+
					"faction: 0\n"+
					"", ToYamlString(result) );

			StringsAreEqual( src.ToDisplay(), result.ToDisplay() );

			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );
		}

		[TestMethod]
		public void Clone() {

			var src = new Agent( 3, 5 );
			src.Ident = 5;
			src.Type = AgentType.GHOST;
			src.Face = DirEnum.West;
			src.Status = StatusEnum.Sleep;

			// invocation
			Agent result = (Agent)src.Clone();

			// assertions
			StringsAreEqual( "name: Ghost\n"+
					"face: West\n"+
					"status: Sleep\n"+
					"faction: 0\n"+
					"", ToYamlString(result) );

			StringsAreEqual( src.ToDisplay(), result.ToDisplay() );

			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );
		}

		[TestMethod]
		public void constructor_where() {

			Where where = new Where( 3, 5 );

			// invocation
			Agent result = new Agent( where );

			// assertions
			AreNotSame( where, result.Where );
			StringsAreEqual( "name: Peasant\n"+
					"face: North\n"+
					"status: Alert\n"+
					"faction: 0\n"+
					"", ToYamlString(result) );

			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );

		}


		[TestMethod]
		public void IsFoe() {

			var who1 = new Agent();
			var who2 = new Agent();

			who1.Faction = 0;
			who2.Faction = 1;
			IsTrue( who1.IsFoe( who2  ) );

			who1.Faction = 1;
			IsFalse( who1.IsFoe(who2) );
		}

		[TestMethod]
		public void IsAlly() {

			var who1 = new Agent();
			var who2 = new Agent();

			who1.Faction = 0;
			who2.Faction = 0;
			IsTrue( who1.IsAlly( who2  ) );

			who1.Faction = 1;
			IsFalse( who1.IsAlly(who2) );
		}

		public void DamageTo() {
			
			var who1 = new Agent();
			var who2 = new Agent();

			AreEqual( 1, who1.DamageTo( who2, 0 ) );
			AreEqual( 0, who1.DamageTo( who2, -1 ) );
			AreEqual( 2, who1.DamageTo( who2, +1 ) );
		}

		[TestMethod]
		public void IsActive() {

			var who = new Agent();

			who.Status = StatusEnum.Active;
			IsTrue( who.IsActionReady() );

			who.Status = StatusEnum.Alert;
			IsFalse( who.IsActionReady() );
		}
	}
}
