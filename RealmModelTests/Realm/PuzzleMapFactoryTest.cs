//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;

	using System;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

	[TestClass]
	public class PuzzleMapFactoryTest {
		
		static public void DisplayMap(PuzzleMap map) {
			Console.Out.WriteLine(RealmManager.DumpLevelMap(map));
		}

		public PuzzleMap CheckMap() {
			var map = PuzzleMap.Allocate( 5, 5 );

			map.Places[2,2].Height = HeightEnum.Pit;	// hole in the middle
			map.AddAgent(  AgentType.PEASANT, new Where(2,3), DirEnum.North );
			map.AddFlag( FlagEnum.Lever, 3,2 );

			return map;
		}

//======================================================================================================================

		[TestMethod]
		public void SimpleTerrain() {

			// invocation
			var result = PuzzleMapFactory.SimpleTerrain( 10, 12 );

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 10, result.Places.Wide );
			AreEqual( 12, result.Places.Tall );

			AreEqual( 10, result.Wide );
			AreEqual( 12, result.Tall );

			IsNull( result.Places[0,0].Agent );
			IsNotNull( result.Places[3,5].Agent );
			AreEqual( 1, result.Agents.Count );

		}

		[TestMethod]
		public void RandomTerrain() {

			// invocation
			var result = PuzzleMapFactory.RandomTerrain();

			// assertions
			IsNotNull(result);

			IsTrue(result.Agents.Count >= 1);
			IsNotNull(result.Agents[0]);

		}

		[TestMethod]
		public void GenerateTerrain() {

			// invocation - fixed seed = same result every time
			var result = PuzzleMapFactory.GenerateTerrain(1);

			// assertions
			AreEqual(7, result.Wide);
			AreEqual(6, result.Tall);

			IsNotNull( result.Places[0, 0].Agent );

			AreEqual( 8, result.Agents.Count);

			AreEqual("Peasant", result.Agents[0].Type.Name);

		}

		[TestMethod]
		public void SingleLineTerrain() {

			// invocation
			var result = PuzzleMapFactory.SingleLineTerrain( 8 );

			// assertions
			AreEqual( 1, result.Wide );
			AreEqual( 8, result.Tall );

			IsNull( result.Places[0,0].Agent );
			AreEqual( 0, result.Agents.Count );

		}
		

//======================================================================================================================

		[TestMethod]
		public void CutRow_North() {

			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.DropRow( map, DirEnum.North );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 5, result.Places.Wide );
			AreEqual( 4, result.Places.Tall );

			AreEqual( 5, result.Wide );
			AreEqual( 4, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,2].Flag );

			IsNotNull( result.Places[2,3].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void CutRow_East() {

			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.DropRow( map, DirEnum.East );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 4, result.Places.Wide );
			AreEqual( 5, result.Places.Tall );

			AreEqual( 4, result.Wide );
			AreEqual( 5, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,2].Flag );

			IsNotNull( result.Places[2,3].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void CutRow_South() {

			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.DropRow( map, DirEnum.South );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 5, result.Places.Wide );
			AreEqual( 4, result.Places.Tall );

			AreEqual( 5, result.Wide );
			AreEqual( 4, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,1].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,1].Flag );

			IsNotNull( result.Places[2,2].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,2)", result.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void CutRow_West() {

			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.DropRow( map, DirEnum.West );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 4, result.Places.Wide );
			AreEqual( 5, result.Places.Tall );

			AreEqual( 4, result.Wide );
			AreEqual( 5, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[1,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[2,2].Flag );

			IsNotNull( result.Places[1,3].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(1,3)", result.Agents[0].Where.ToString() );
		}

//======================================================================================================================

		[TestMethod]
		public void AddRow_North() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.North );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 5, result.Places.Wide );
			AreEqual( 6, result.Places.Tall );

			AreEqual( 5, result.Wide );
			AreEqual( 6, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,2].Flag );

			IsNotNull( result.Places[2,3].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void AddRow_East() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.East );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 6, result.Places.Wide );
			AreEqual( 5, result.Places.Tall );

			AreEqual( 6, result.Wide );
			AreEqual( 5, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,2].Flag );

			IsNotNull( result.Places[2,3].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void AddRow_South() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.South );
DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 5, result.Places.Wide );
			AreEqual( 6, result.Places.Tall );

			AreEqual( 5, result.Wide );
			AreEqual( 6, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,3].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,3].Flag );

			IsNotNull( result.Places[2,4].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,4)", result.Agents[0].Where.ToString() );
		}

		[TestMethod]
		public void AddRow_West() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.West );
PuzzleMapTest.DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 6, result.Places.Wide );
			AreEqual( 5, result.Places.Tall );

			AreEqual( 6, result.Wide );
			AreEqual( 5, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[3,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[4,2].Flag );

			IsNotNull( result.Places[3,3].Agent );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(3,3)", result.Agents[0].Where.ToString() );
		}
	}

}
