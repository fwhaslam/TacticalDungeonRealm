//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Views {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Enums;
	using Realm.Game;
	using Realm.Puzzle;

	using System;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Verbose.Utility.VerboseAsserts;

	[TestClass]
	public class ViewManagerTest {

		[TestMethod]
		public void Constructor_settersGetters() {
			
			// invocations
			var result = new ViewManager();

			// assertions
			IsNotNull( result );
		}

		
		[TestMethod]
		public void GetAgentMoves() {

			var work = new ViewManager();
			var puzzle = PuzzleMapTest.Sample();
			var who = puzzle.Agents[0];

			// agent(same faction) blocks
			puzzle.AddAgent( AgentType.GHOST, new Tools.Where(1,3), DirEnum.East );

			// height blocks
			puzzle.Places[1,4].Height = HeightEnum.Three;

			var map = new PlayMap( puzzle );

			// invocation
			var result = work.GetAgentMoves( map, who );
			
			// assertions
			StringsAreEqual("FloodFillView[\n"+
					"     -1 -1  2  3  5\n"+
					"     -1 -1  0  2  4\n"+
					"      5  3 -1  3  5\n"+
					"     -1  5 -1  5 -1\n"+
					"     -1 -1 -1 -1 -1\n"+
					"]", result.ToDisplay() );
		}

		
		[TestMethod]
		public void GetAgentPaths() {

			var work = new ViewManager();
			var puzzle = PuzzleMapTest.Sample();
			var who = puzzle.Agents[0];

//Console.Out.WriteLine( "AGENT[0]="+work.Map.Agents[0].ToDisplay() );
			puzzle.AddAgent( AgentType.GOBLIN, new Tools.Where(1,3), DirEnum.South);
			puzzle.Agents[1].Faction = 1;
			puzzle.AddAgent( AgentType.GOBLIN, new Tools.Where(4,0), DirEnum.South);
			puzzle.Agents[2].Faction = 2;

			var map = new PlayMap( puzzle );

			// invocation
			var result = work.GetAgentPaths( map, who );
			
			// assertions
			StringsAreEqual("FloodFillView[\n"+
					"      3  2  3  5  7\n"+
					"      2  0  2  4  6\n"+
					"      3  2 -1  5  4\n"+
					"      5  4  5  3  2\n"+
					"      7  6  4  2  0\n"+
					"]", result.ToDisplay() );
		}

	}
}
