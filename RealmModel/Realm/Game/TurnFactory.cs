//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Game {

	using Realm.Enums;
	using Realm.Game.Action;
	using Realm.Puzzle;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;


	public class TurnFactory {

		/// <summary>
		/// Is there room for another action in this turn?
		/// </summary>
		/// <param name="turn"></param>
		/// <returns></returns>
		static public bool TurnFull( GameTurn turn ) {

			if (turn.Move!=null) {
				if (turn.Attack!= null) return true;
				if (turn.Defend!= null) return true;
				if (turn.Sprint!= null) return true;
			}
			return false;
		}

		static public void AddMove(GameTurn turn, List<DirEnum> dirIds) {

			if (turn.Move==null) turn.Move = new List<DirEnum>();
			
			foreach ( int dirId in dirIds ) turn.Move.Add( (DirEnum)dirId );

		}

		/// <summary>
		/// Default or starting ActionChain.
		/// </summary>
		/// <param name="who"></param>
		/// <returns></returns>
		static public GameTurn NewTurn(Agent who) {
			return new GameTurn(who);
		}

		static public AttackAction AddAttack( GameTurn turn, int agentId, int dmg) {
			turn.Attack = new AttackAction( agentId, dmg );
			return turn.Attack;
		}

		/// <summary>
		/// New location, or current if there are no moves.
		/// </summary>
		/// <param name="moves"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		static public Where MoveWhere( List<DirEnum> moves, Where start ) {
			var next = new Where(start);
			if (moves!=null) foreach ( var move in moves ) {
				next.X += DirEnumTraits.DX((int)move);
				next.Y += DirEnumTraits.DY((int)move);
			}
			return next;
		}

		/// <summary>
		/// Last facing in move list, or previous facing.
		/// </summary>
		/// <param name="moves"></param>
		/// <param name="face"></param>
		/// <returns></returns>
		static public DirEnum MoveFace( List<DirEnum> moves, DirEnum face ) {
			if (moves!=null && moves.Count>0) return moves[ moves.Count-1 ];
			return face;
		}
	}
}
