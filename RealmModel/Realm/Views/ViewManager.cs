﻿//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views {

	using Realm.Game;
	using Realm.Puzzle;
	using Realm.Tools;
	using Realm.Views.Decider;

	/// <summary>
	/// Discover valid desitnations for the agent/map combination.
	/// </summary>
	public class ViewManager {

		public FloodFillView GetAgentMoves( PlayMap map, Agent who) {

			var decider = new AgentMoveStepDecider( who );
			var starts = decider.GetStarts( map );

			var move = new FloodFillView( map.Places );
			move.DoStepFill( starts, decider.Decide );
			return move;
		}

		public FloodFillView GetAgentPaths( PlayMap map, Agent who) {

			var decider = new AgentPathsStepDecider( who );
			var starts = decider.GetStarts( map );

			var move = new FloodFillView( map.Places );
			move.DoStepFill( starts, decider.Decide );
			return move;
		}
	}
}
