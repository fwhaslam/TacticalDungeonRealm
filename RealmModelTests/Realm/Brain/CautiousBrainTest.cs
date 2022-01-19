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
	public class CautiousBrainTest {

				
		[TestMethod]
		public void ChooseAction_active_approachNearbyEnemy() {

			var mind = new CautiousBrain();
			
			var puzzle = PuzzleMapFactory.SimpleTerrain(6,6);

			var actor = puzzle.Agents[0];
			actor.Status = StatusEnum.Active;
			actor.Face = DirEnum.West;

			var foe = puzzle.AddAgent( AgentType.GHOST, new Where(5,5), DirEnum.North, 1, StatusEnum.Waiting );

			var map = new PlayMap(puzzle);
 
			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual( "actorId: 0\n"+
					"move:\n"+
					"- North\n"+
					"- NorthEast\n"+
					"defend: true\n"+
					"", ToYamlString(result) );
		}

			
		[TestMethod]
		public void ChooseAction_active_approachAndAttackNearbyEnemy() {


			var mind = new CautiousBrain();
			
			var puzzle = PuzzleMapFactory.SimpleTerrain(6,6);

			var actor = puzzle.Agents[0];
			actor.Status = StatusEnum.Active;
			actor.Face = DirEnum.West;

			var foe = puzzle.AddAgent( AgentType.GHOST, new Where(2,4), DirEnum.North, 1, StatusEnum.Waiting );

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
			actor.Status = StatusEnum.Waiting;
			actor.Face = DirEnum.West;

			var foe = puzzle.AddAgent( AgentType.GHOST, new Where(5,5), DirEnum.North, 1, StatusEnum.Waiting );
 
			var map = new PlayMap( puzzle );

			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual( "actorId: 0\n"+
					"", ToYamlString(result) );
		}

	}
}
