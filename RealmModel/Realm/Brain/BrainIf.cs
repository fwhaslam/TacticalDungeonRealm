//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Brain {

	using Realm.Game;
	using Realm.Game.Action;
	using Realm.Puzzle;

	public interface BrainIf {

		/// <summary>
		/// Select an action for the actor.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="actor"></param>
		/// <returns></returns>
		GameTurn ChooseAction( PlayMap map, Agent actor );
	}

}
