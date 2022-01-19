//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Brain {

	using Realm.Game;
	using Realm.Game.Action;
	using Realm.Puzzle;

	/// <summary>
	/// Wrapper for player taking turns for the player faction.
	/// This will 'wait' for player action events, then pass them up.
	/// hmmm .... not sure this is a good way to handle it ...
	public class PlayerBrain : BrainIf {
		
		/// <summary>
		/// A brain will select actions based on a map and agent.
		/// </summary>
		/// <param name="who"></param>
		/// <param name="puzzle"></param>
		/// <returns></returns>
		public GameTurn ChooseAction( PlayMap puzzle, Agent actor ) {
			return null;
		}
	}
}
