using Microsoft.VisualStudio.TestTools.UnitTesting;

using Realm.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Game {

	using Realm.Tools;
	using static Realm.Enums.DirEnum;
	using static Verbose.Utility.VerboseAsserts;

	[TestClass]
	public class TurnFactoryTest {

		[TestMethod]
		public void MoveWhere_threeSteps() {
			
			var where = new Where( 10, 10 );
			var moves = new List<DirEnum>();
			moves.Add( North );
			moves.Add( NorthEast );
			moves.Add( East );

			// invocation
			var result = TurnFactory.MoveWhere( moves, where );

			// assertions
			StringsAreEqual( "Where(12,12)", result.ToDisplay() );
		}

		[TestMethod]
		public void MoveFace_threeSteps() {
			
			var moves = new List<DirEnum>();
			moves.Add( North );
			moves.Add( NorthEast );
			moves.Add( East );

			// invocation
			var result = TurnFactory.MoveFace( moves, SouthWest );

			// assertions
			StringsAreEqual( "East", result.ToString() );
		}

		[TestMethod]
		public void MoveFace_null() {
			
			// invocation
			var result = TurnFactory.MoveFace( null, SouthWest );

			// assertions
			StringsAreEqual( "SouthWest", result.ToString() );
		}

		[TestMethod]
		public void MoveFace_empty() {
			
			// invocation
			var result = TurnFactory.MoveFace( null, SouthWest );

			// assertions
			StringsAreEqual( "SouthWest", result.ToString() );
		}
	}
}
