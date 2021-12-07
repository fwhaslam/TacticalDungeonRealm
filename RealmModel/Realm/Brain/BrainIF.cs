using Realm.Game;
using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Brain {

	public interface BrainIf {

		/// <summary>
		/// Select an action for the actor.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="actor"></param>
		/// <returns></returns>
		ActionChain ChooseAction( PuzzleMap map, Agent actor );
	}

}
