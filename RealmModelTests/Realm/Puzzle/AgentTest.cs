//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Puzzle {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Verbose.Utility.VerboseAsserts;
	using static Verbose.Utility.VerboseTools;

	[TestClass]
	class AgentTest {

		[TestMethod]
		public void constructor_int_int() {

			// invocation
			Agent result = new Agent( 3, 5 );

			// assertions
			StringsAreEqual( "", AsPrettyString( result ) );
			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );

			IsNull( result.Type );
			IsNull( result.Face );

		}

		[TestMethod]
		public void constructor_where() {

			Where where = new Where( 3, 5 );

			// invocation
			Agent result = new Agent( where );

			// assertions
			AreNotSame( where, result.Where );
			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );
			
			IsNull( result.Type );
			IsNull( result.Face );

		}

		[TestMethod]
		public void constructor_copy() {

			Agent src = new Agent( 3, 5 );
			src.Type = AgentType.PEASANT;
			src.Face = DirEnum.NorthEast;

			// invocation
			Agent result = new Agent( src );

			// assertions
			AreEqual( 3, result.Where.X );
			AreEqual( 5, result.Where.Y );
			
			AreEqual( AgentType.PEASANT, result.Type );
			AreEqual( DirEnum.NorthEast, result.Face );

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
			IsTrue( who.IsActive() );

			who.Status = StatusEnum.Alert;
			IsFalse( who.IsActive() );
		}
	}
}
