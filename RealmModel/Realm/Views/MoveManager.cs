//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views {

	using Realm.Puzzle;
	using Realm.Tools;
	using Realm.Views.Decider;

	/// <summary>
	/// Discover valid desitnations for the agent/map combination.
	/// </summary>
	public class MoveManager {

		public FloodFillView GetAgentMoves( PuzzleMap map, Agent who) {

			var decider = new AgentMoveStepDecider( who );
			var starts = decider.GetStarts( map );

			var move = new FloodFillView( map.Places );
			move.DoStepFill( starts, decider.Decide );
			return move;
		}

		public FloodFillView GetAgentPaths( PuzzleMap map, Agent who) {

			var decider = new AgentPathsStepDecider( who );
			var starts = decider.GetStarts( map );

			var move = new FloodFillView( map.Places );
			move.DoStepFill( starts, decider.Decide );
			return move;
		}
	}
}
