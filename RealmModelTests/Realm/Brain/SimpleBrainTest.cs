

namespace Realm.Brain {

	using Realm;
	using Realm.Brain;
	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;
	using Realm.Views;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Verbose.Utility.VerboseTools;
	using static Verbose.Utility.VerboseAsserts;
	using Telerik.JustMock;
	using Realm.Game;
	using FakeItEasy;

	[TestClass]
	public class SimpleBrainTest {

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


			var mind = new SimpleBrain();
			
			var map = PuzzleMapFactory.SimpleTerrain(6,6);
			var actor = map.Agents[0];
			actor.Status = StatusEnum.Active;

			actor.Face = DirEnum.West;
			var foe = map.AddAgent( AgentType.GHOST, new Where(5,5), DirEnum.North, 1, StatusEnum.Alert );
 
			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual("[\n"+
					"  {\n"+
					"    \"Type\": \"Right\"\n"+
					"  },\n"+
					"  {\n"+
					"    \"Type\": \"Right\"\n"+
					"  },\n"+
					"  {\n"+
					"    \"Type\": \"Forward\"\n"+
					"  },\n"+
					"  {\n"+
					"    \"Type\": \"Right\"\n"+
					"  },\n"+
					"  {\n"+
					"    \"Type\": \"Forward\"\n"+
					"  }\n"+
					"]", AsPrettyString(result.Actions) );
		}

			
		[TestMethod]
		public void ChooseAction_active_approachAndAttackNearbyEnemy() {


			var mind = new SimpleBrain();
			
			var map = PuzzleMapFactory.SimpleTerrain(6,6);
			var actor = map.Agents[0];
			actor.Status = StatusEnum.Active;

			actor.Face = DirEnum.West;
			var foe = map.AddAgent( AgentType.GHOST, new Where(2,4), DirEnum.North, 1, StatusEnum.Alert );
 
			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual("[\n"+
					"  {\n"+
					"    \"Type\": \"Right\"\n"+
					"  },\n"+
					"  {\n"+
					"    \"Type\": \"Right\"\n"+
					"  },\n"+
					"  {\n"+
					"    \"Type\": \"Forward\"\n"+
					"  },\n"+
					"  {\n"+
					"    \"Damage\": 1,\n"+
					"    \"Target\": 1,\n"+
					"    \"Type\": \"Attack\"\n"+
					"  }\n"+
					"]", AsPrettyString(result.Actions) );
		}

				
		[TestMethod]
		public void ChooseAction_alert_noAction() {


			var mind = new SimpleBrain();
			
			var map = PuzzleMapFactory.SimpleTerrain(6,6);
			var actor = map.Agents[0];
			actor.Status = StatusEnum.Alert;

			actor.Face = DirEnum.West;
			var foe = map.AddAgent( AgentType.GHOST, new Where(5,5), DirEnum.North, 1, StatusEnum.Alert );
 
			// invoke 
			var result = mind.ChooseAction(map,actor);
 
			// assert 
			StringsAreEqual("[]", AsPrettyString(result.Actions) );
		}



			
	}
}
