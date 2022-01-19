//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Game {

	using Realm.Game.Action;
	using Realm.Puzzle;

	public class PlaySnapshot {

		public PlaySnapshot() { }

		public PlaySnapshot( PlayMap map ) { this.Arena = map; }


		public PlaySnapshot( GameTurn action, PlayMap map ) { 
			this.Action = action;
			this.Arena = map; 
		}

		/// <summary>
		/// The action that created this state from the previous state.
		/// </summary>
		public GameTurn Action { get; set; }
		
		public PlayMap Arena { get; set; }

	}
}
