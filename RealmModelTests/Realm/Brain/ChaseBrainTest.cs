//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Brain {

	using Realm;
	using Realm.Brain;
	using Realm.Enums;
	using Realm.Game;
	using Realm.Puzzle;
	using Realm.Tools;
	using Realm.Views;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using static Realm.Tools.YamlTools;
	using static Verbose.Utility.VerboseAsserts;


	[TestClass]
	public class ChaseBrainTest {

		// SAMPLE METHOD demonstrating use of JustMock
		//[TestMethod]
		//public void ChooseAction_justMock() {

		//	var map = PuzzleMapFactory.SimpleTerrain(4,4);
		//	var actor = map.Agents[0];
		//	//var paths = new MoveManager().GetAgentPaths( map, actor );
		//	var chain = new ActionChain( null );

		//   // Arrange 
		//	SimpleBrain mind = Mock.Create<SimpleBrain>();
 
		//	Mock.Arrange(
		//		() => mind.BuildMoveChain( map, Arg.IsAny<FloodFillView>(), actor )
		//	).Returns( chain ); 
 
		//	// Act 
		//	var result = mind.ChooseAction(map,actor);
 
		//	// Assert 
		//	AreSame( chain, result );
		//}
		

		// SAMPLE METHOD demonstrating use of FakeItEasy
		//[TestMethod]
		//public void ChooseAction_fakeItEasy() {

		//	var map = PuzzleMapFactory.SimpleTerrain(4,4);
		//	var actor = map.Agents[0];
		//	//var paths = new MoveManager().GetAgentPaths( map, actor );
		//	var chain = new ActionChain( null );

		//   // mock 
		//	SimpleBrain mind = A.Fake<SimpleBrain>();
 
		//	// expect
		//	A.CallTo(
		//		() => mind.BuildMoveChain( map, A<FloodFillView>.Ignored, actor )
		//	).Returns( chain ); 
		//	//A.CallTo(() => fake.SomeMethod()).CallBaseMethod();
		//	//A.CallTo(fake).CallBaseMethod();
 
		//	// invoke 
		//	var result = mind.ChooseAction(map,actor);
 
		//	// assert 
		//	AreSame( chain, result );
		//	A.CallTo(
		//		() => mind.BuildMoveChain( map, A<FloodFillView>.Ignored, actor )
		//	).MustHaveHappened();
		//}

				
		[TestMethod]
		public void ChooseAction_active_approachNearbyEnemy() {

			var mind = new ChaseBrain();
			
			var puzzle = PuzzleMapFactory.SimpleTerrain(6,6);

			var actor = puzzle.Agents[0];
			actor.Status = StatusEnum.Active;
			actor.Face = DirEnum.West;

			var foe = puzzle.AddAgent( AgentType.GHOST, new Where(5,5), DirEnum.North, 1, StatusEnum.Alert );

			var map = new PlayMap(puzzle);
 
			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual( "actorId: 0\n"+
					"move:\n"+
					"- North\n"+
					"- NorthEast\n"+
					"sprint:\n"+
					"- NorthEast\n"+
					"- East\n"+
					"", ToYamlString(result) );
		}

			
		[TestMethod]
		public void ChooseAction_active_approachAndAttackNearbyEnemy() {


			var mind = new ChaseBrain();
			
			var puzzle = PuzzleMapFactory.SimpleTerrain(6,6);

			var actor = puzzle.Agents[0];
			actor.Status = StatusEnum.Active;
			actor.Face = DirEnum.West;

			var foe = puzzle.AddAgent( AgentType.GHOST, new Where(2,4), DirEnum.North, 1, StatusEnum.Alert );

			var map = new PlayMap(puzzle);

			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual( "actorId: 0\n"+
					"move:\n"+
					"- North\n"+
					"attack:\n"+
					"  TargetId: 1\n"+
					"  Damage: 1\n"+
					"", ToYamlString(result) );
		}

				
		[TestMethod]
		public void ChooseAction_alert_noAction() {


			var mind = new ChaseBrain();
			
			var puzzle = PuzzleMapFactory.SimpleTerrain(6,6);

			var actor = puzzle.Agents[0];
			actor.Status = StatusEnum.Alert;
			actor.Face = DirEnum.West;

			var foe = puzzle.AddAgent( AgentType.GHOST, new Where(5,5), DirEnum.North, 1, StatusEnum.Alert );
 
			var map = new PlayMap( puzzle );

			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual( "actorId: 0\n"+
					"", ToYamlString(result) );
		}

	}
}
