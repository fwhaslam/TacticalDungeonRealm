using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Views {

	public class MoveManager {

		public PuzzleMap Map {  get; set; }

		public Agent Who { get; set; }

		public AgentMoveView GetAllMoves() {
			var move = new AgentMoveView();
			move.DoStepFill( Map, Who );
			return move;
		}
	}
}
