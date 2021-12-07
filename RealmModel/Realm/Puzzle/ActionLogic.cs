//
//	Copyright 2021 Frederick William Haslam born 1962
//


namespace Realm.Puzzle {
	
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ActionLogic {

		static public int DamageTo( PuzzleMap map, Agent attacker, Agent target) {

			int adds = 0;
			var grid = map.Places;

			// less damage when attacking uphill
			if ( grid[attacker.Where].Height < grid[target.Where].Height ) adds--;

			return attacker.DamageTo( target, adds );
		}
	}
}
