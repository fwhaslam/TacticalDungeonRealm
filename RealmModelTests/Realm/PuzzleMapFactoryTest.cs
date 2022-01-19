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
	using static Verbose.Utility.VerboseAsserts;
	using static Realm.Tools.YamlTools;

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

			IsFalse( result.Places[0,0].IsOccupied );
			AreEqual( 0, result.Places[3,5].AgentId );
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
			StringsAreEqual( "Title: Empty Map\n"+
					"Image: pic1.png\n"+
					"Text:\n"+
					"  Start: Some Story\n"+
					"Wide: 7\n"+
					"Tall: 6\n"+
					"Agents:\n"+
					"- name: Peasant\n"+
					"  face: SouthEast\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"- name: Goblin\n"+
					"  face: NorthEast\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"- name: Ghost\n"+
					"  face: NorthEast\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"- name: Skeleton\n"+
					"  face: SouthEast\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"- name: Peasant\n"+
					"  face: NorthEast\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"- name: Ghost\n"+
					"  face: NorthWest\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"- name: Goblin\n"+
					"  face: NorthEast\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"- name: Ghost\n"+
					"  face: West\n"+
					"  status: Alert\n"+
					"  faction: 0\n"+
					"Map:\n"+
					"- 2.04/P.06/4P__/5.__/4.__/4.__/3.__\n"+
					"- 4.__/3.__/3.__/PE__/1.__/P.__/3.__\n"+
					"- 3.__/P.__/1.__/PS__/4.02/5.__/1.07\n"+
					"- 2.__/1.__/3.01/2.__/5.__/4.__/5.__\n"+
					"- 2.__/1.__/4.__/4.__/3.00/5.__/4.__\n"+
					"- 5.03/5.__/4.05/1.__/4.__/P+__/2.__\n"+
					"", ToYamlString(result) );
			//AreEqual(7, result.Wide);
			//AreEqual(6, result.Tall);

			//AreEqual( 4, result.Places[0, 0].AgentId );
			//AreEqual( 8, result.Agents.Count);
			//AreEqual("Peasant", result.Agents[0].Type.Name);
		}

		[TestMethod]
		public void SingleLineTerrain() {

			// invocation
			var result = PuzzleMapFactory.SingleLineTerrain( 8 );

			// assertions
			AreEqual( 8, result.Wide );
			AreEqual( 1, result.Tall );

			IsFalse( result.Places[0,0].IsOccupied );
			AreEqual( 0, result.Agents.Count );

		}

//======================================================================================================================

		[TestMethod]
		public void CutRow_North() {

			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.DropRow( map, DirEnum.North );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 5, result.Places.Wide );
			AreEqual( 4, result.Places.Tall );

			AreEqual( 5, result.Wide );
			AreEqual( 4, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,2].Flag );

			AreEqual( 0, result.Places[2,3].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToDisplay() );
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

			AreEqual( 0, result.Places[2,3].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToDisplay() );
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

			AreEqual( 0, result.Places[2,2].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,2)", result.Agents[0].Where.ToDisplay() );
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

			AreEqual( 0, result.Places[1,3].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(1,3)", result.Agents[0].Where.ToDisplay() );
		}

//======================================================================================================================

		[TestMethod]
		public void AddRow_North() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.North );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 5, result.Places.Wide );
			AreEqual( 6, result.Places.Tall );

			AreEqual( 5, result.Wide );
			AreEqual( 6, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,2].Flag );

			AreEqual( 0, result.Places[2,3].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToDisplay() );
		}

		[TestMethod]
		public void AddRow_East() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.East );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 6, result.Places.Wide );
			AreEqual( 5, result.Places.Tall );

			AreEqual( 6, result.Wide );
			AreEqual( 5, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,2].Flag );

			AreEqual( 0, result.Places[2,3].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,3)", result.Agents[0].Where.ToDisplay() );
		}

		[TestMethod]
		public void AddRow_South() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.South );
//DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 5, result.Places.Wide );
			AreEqual( 6, result.Places.Tall );

			AreEqual( 5, result.Wide );
			AreEqual( 6, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[2,3].Height );
			AreEqual( FlagEnum.Lever, result.Places[3,3].Flag );

			AreEqual( 0, result.Places[2,4].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(2,4)", result.Agents[0].Where.ToDisplay() );
		}

		[TestMethod]
		public void AddRow_West() {
			
			var map = CheckMap();

			// invocation
			var result = PuzzleMapFactory.AddRow( map, DirEnum.West );
//PuzzleMapTest.DisplayMap(map);

			// assertions
			AreEqual( "Empty Map", result.Title );
			AreEqual( "pic1.png", result.Image );

			AreEqual( 6, result.Places.Wide );
			AreEqual( 5, result.Places.Tall );

			AreEqual( 6, result.Wide );
			AreEqual( 5, result.Tall );

			AreEqual( HeightEnum.Pit, result.Places[3,2].Height );
			AreEqual( FlagEnum.Lever, result.Places[4,2].Flag );

			AreEqual( 0, result.Places[3,3].AgentId );
			AreEqual( 1, result.Agents.Count );
			AreEqual( "Where(3,3)", result.Agents[0].Where.ToDisplay() );
		}
	}

}
