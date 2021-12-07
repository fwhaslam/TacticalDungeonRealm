//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Puzzle {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Verbose.Utility.CollectionAsserts;
	using static Verbose.Utility.GenericCollectionAsserts;
	using static Verbose.Utility.VerboseAsserts;
	using static Verbose.Utility.VerboseTools;

	[TestClass]
	public class PuzzleMapTest {

		static public void DisplayMap(PuzzleMap map) {
			Console.Out.WriteLine(RealmManager.DumpLevelMap(map));
		}

		static public PuzzleMap Sample() {

			var map = PuzzleMap.Allocate( 5, 5 );

			map.Places[2,2].Height = HeightEnum.Pit;	// hole in the middle
			map.AddAgent(  AgentType.PEASANT, new Where(2,3), DirEnum.North );
			map.AddFlag( FlagEnum.Lever, 3,2 );

			return map;
		}

		[TestMethod]
		public void Allocate() {
			
			// invocation
			var result = PuzzleMap.Allocate(10,12);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 10, result.Places.Wide );
			AreEqual( 12, result.Places.Tall );

			AreEqual( 10, result.Wide );
			AreEqual( 12, result.Tall );

			IsNull( result.Places[0,0].Agent );
			AreEqual( 0, result.Agents.Count );
		}

		public void AddAgent_all() {

			var map = Sample();

			var FACTION = 1;
			var STATUS = StatusEnum.Sleep;

			// invoke
			var result = map.AddAgent( AgentType.GHOST, new Where(0,0), DirEnum.South, FACTION, STATUS );

			// assertions
			AreEqual( result, map.Places[0,0].Agent );
			Contains( result, map.Agents);
			AreEqual( 2, map.Agents.Count );

			StringsAreEqual( "", result.ToDisplay() );
			StringsAreEqual( "", AsPrettyString(result) );
		}

		public void AddAgent_min() {
						
			var map = Sample();

			// invocation
			var result = map.AddAgent( AgentType.GHOST, new Where(0,0), DirEnum.South );

			// assertions
			AreEqual( result, map.Places[0,0].Agent );
			Contains( result, map.Agents);
			AreEqual( 2, map.Agents.Count );

			StringsAreEqual( "", result.ToDisplay() );
			StringsAreEqual( "", AsPrettyString(result) );
		}

		public void DropAgent() {
						
			var map = Sample();
			var agent = map.Agents[0];

			// invocation
			map.DropAgent( agent );

			// assertions
			IsEmpty<Agent>( map.Agents );
			IsNull( map.Places[2,3].Agent );

		}

	}
}
