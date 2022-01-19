//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Brain {

	using Realm.Enums;
	using Realm.Game;
	using Realm.Puzzle;
	using Realm.Views;

	using System.Collections.Generic;

	using Realm.Game.Action;
	using Realm.Tools;
	using System;

	/// <summary>
	/// Never sprint.  Defend when not attacking.
	/// </summary>
	public class CautiousBrain : ChaseBrain {

		/// <summary>
		/// Simple logic.  Move towards closest foe, hit them.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="actor"></param>
		/// <returns></returns>
		override public GameTurn ChooseAction( PlayMap map, Agent actor ) {

			var turn = TurnFactory.NewTurn(actor);

			if ( actor.IsActionReady() ) BuildTurn( turn, map, actor );

			return turn;
		}

		/// <summary>
		/// Simple Logic, move + attack, or move twice.
		/// </summary>
		/// <param name="turn"></param>
		/// <param name="map"></param>
		/// <param name="pathView"></param>
		/// <param name="who"></param>
		override internal void BuildTurn( GameTurn turn, PlayMap map, Agent who ) {

			var pathView = VIEWER.GetAgentPaths( map, who );

			// move as far as possible
			turn.Move = PickMove( map, pathView, who.Where, who.Type.Steps, (int)who.Face, who.Faction );
			var nextWhere = TurnFactory.MoveWhere( turn.Move, who.Where );
			var nextFace = TurnFactory.MoveFace( turn.Move, who.Face );

			// is there something to attack?
			var target = PickTarget( map, pathView, nextWhere, (int)nextFace, who.Faction );
			if (target!=null) {
				var targetId = target.Ident;
				var damage = ActionLogic.DamageTo( map, who, target );
				TurnFactory.AddAttack( turn, targetId, damage );
			}

			// no target, defend
			if (target==null) {
				turn.Defend = true;
			}
		}

	}
}
