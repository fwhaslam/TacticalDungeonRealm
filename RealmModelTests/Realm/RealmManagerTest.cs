//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Puzzle;

	using Verbose.Utility;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Verbose.Utility.GenericCollectionAsserts;


	[TestClass]
	public class RealmManagerTest {

		static readonly string MIN_LEVEL = "Wide: 1\n"+
					"Tall: 1\n"+
					"Map:\n"+
					"- 1.__";

		static readonly string CHECK_LEVEL = "Title: Empty Map\n" +
				"Image: pic1.png\n"+
				"Text:\n" + 
				"  Start: Some Story\n"+
				"Wide: 8\n" +
				"Tall: 8\n" +
				"Agents:\n" +
    			"- name: Peasant\n"+
				"  face: North\n"+
				"  status: Alert\n"+
				"  faction: 0\n"+
				"Map:\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.00/P.__/P.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/P.__/P.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n" +
				"- 1.__/1.__/1.__/1.__/1.__/1.__/1.__/1.__\n";

		[TestMethod]
		public void DumpLevelMap( ) {

			PuzzleMap map = PuzzleMapFactory.SimpleTerrain(8,8);

			// invocation
			string result = RealmManager.DumpLevelMap( map );

			// assertions
			result = result.Replace( "\r", "" );

			VerboseAsserts.StringsAreEqual( CHECK_LEVEL, result );
		}

		[TestMethod]
		public void ParseLevelMap( ) {

			// invocation
			PuzzleMap result = RealmManager.ParseLevelMap( CHECK_LEVEL );

Console.Out.WriteLine( "MAP>>"+RealmManager.DumpLevelMap( result ) );

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( 8, result.Wide );
			AreEqual( 8, result.Tall );

			AreEqual( "Where(0,0)", result.Places[0,0].Where.ToDisplay() );
			AreEqual( -1, result.Places[0,0].AgentId );
			AreEqual( 0, result.Places[2,3].AgentId );
			AreEqual( "Peasant", result.Agents[0].Name );
		}

		[TestMethod]
		public void ParseLevelMap_withStopAtEnd( ) {

			var check = CHECK_LEVEL +"...\n";

			// invocation
			PuzzleMap result = RealmManager.ParseLevelMap( check );

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( 8, result.Wide );
			AreEqual( 8, result.Tall );

			AreEqual( "Where(0,0)", result.Places[0,0].Where.ToDisplay() );
			AreEqual( -1, result.Places[0,0].AgentId );
			AreEqual( 0, result.Places[2,3].AgentId );
			AreEqual( "Peasant", result.Agents[0].Name );
		}

		
		[TestMethod]
		public void ParseLevelMap_minInfo( ) {

			// invocation
			PuzzleMap result = RealmManager.ParseLevelMap( MIN_LEVEL );

Console.Out.WriteLine( "MAP>>"+RealmManager.DumpLevelMap( result ) );

			// assertions
			IsNull( result.Title );
			IsNull( result.Image );
			AreEqual( 1, result.Wide );
			AreEqual( 1, result.Tall );
			IsEmpty( result.Agents );
			IsEmpty( result.Text );
		}

	}
}
