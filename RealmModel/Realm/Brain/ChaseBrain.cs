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

	using static Realm.Tools.YamlTools;

	/// <summary>
	/// Move directly towards enemies and strike when possible, otherwise sprint.
	/// </summary>
	public class ChaseBrain : BrainIf {

		static readonly int DIR_COUNT = DirEnumTraits.Count;

		// zero is forward ( current facing ), 1 is right, 7 is left, etc.
		static readonly int[] LOOK_ORDER = { 0, 1, 7, 2, 6, 3, 5, 4 };

		static internal readonly ViewManager VIEWER = new ViewManager();

		/// <summary>
		/// Simple logic.  Move towards closest foe, hit them.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="actor"></param>
		/// <returns></returns>
		virtual public GameTurn ChooseAction( PlayMap map, Agent who ) {

			var turn = TurnFactory.NewTurn(who);

//Console.Out.WriteLine("  ISACTIVE="+who.IsActive() );
//Console.Out.WriteLine("  AGENT="+ ToYamlString(who) );
			if ( who.IsActionReady() ) BuildTurn( turn, map, who );

			return turn;
		}

		/// <summary>
		/// Simple Logic, move + attack, or move twice.
		/// </summary>
		/// <param name="turn"></param>
		/// <param name="map"></param>
		/// <param name="pathView"></param>
		/// <param name="who"></param>
		virtual internal void BuildTurn( GameTurn turn, PlayMap map, Agent who ) {

			var pathView = VIEWER.GetAgentPaths( map, who );

			// move as far as possible
			turn.Move = PickMove( map, pathView, who.Where, who.Type.Steps, (int)who.Face, who.Faction );
			var nextWhere = TurnFactory.MoveWhere( turn.Move, who.Where );
			var nextFace = TurnFactory.MoveFace( turn.Move, who.Face );

//if (turn.Move==null) Console.Out.WriteLine("    MOVE = NULL");
			// is there something to attack?
			var target = PickTarget( map, pathView, nextWhere, (int)nextFace, who.Faction );
			if (target!=null) {
				var damage = ActionLogic.DamageTo( map, who, target );
				TurnFactory.AddAttack( turn, target.Ident, damage );
			}

			// no target, keep moving
			if (target==null) {
				turn.Sprint = PickMove( map, pathView, nextWhere, who.Type.Steps, (int)nextFace, who.Faction );
			}
//Console.Out.WriteLine("    ACTION = "+ToYamlString(turn));
		}

		/// <summary>
		/// Using an agent path view, choose movement which takes us closer to foes.
		/// </summary>
		/// <param name="chain"></param>
		/// <param name="map"></param>
		/// <param name="pathView"></param>
		/// <param name="who"></param>
		internal List<DirEnum> PickMove( PlayMap map, FloodFillView pathView, Where start, int stepLimit, int face, int faction ) {

			var picked = new List<DirEnum>();

			var spot = pathView.View[start];
			var lowVal = spot.Value;
			var target = (Agent)null;

			// Select steps :: stop for foe
			int steps = 0;
			while ( steps < stepLimit ) {

//Console.Out.WriteLine("Loop Start val="+lowVal );
				var lowDir = -1;
				var lowSpot = spot;
				foreach ( var dir in LOOK_ORDER ) { 

					// rotate around agent starting with forward, right, left
					var dirIx = (face + (int)dir) % DIR_COUNT;
//Console.Out.WriteLine("DIRIX="+dirIx);

					var nextCtx = spot[dirIx];
					if (nextCtx==null) continue;	// cannot go off-grid

					var nextSpot = nextCtx.There;
					if (nextSpot.Value<0) continue;	// cannot enter negative

					var nextAgentId = nextSpot.Here.AgentId;
					if (nextAgentId>=0) {
						var nextAgent = map.Agents[nextAgentId];
						if (nextAgent.IsFoe(faction)) {	// stepped next to enemy, stop moving
							target = nextAgent;
							break;
						}
						// cannot step onto friend, keep looking
					}

					var nextVal = nextSpot.Value;
					if (nextVal<lowVal) {
//Console.Out.WriteLine("next="+nextVal+"  DIR="+dirIx+"  Where="+nextSpot.Here.Where );
						lowSpot = nextSpot;
						lowDir = dirIx;
						lowVal = nextVal;
					}
				}

				// found enemy, break for attack
				if (target!=null) {
					//int damage = ActionLogic.DamageTo( map, who, target );
					//TurnFactory.AddAttack( chain, target, damage );
					break;
				}

				if (lowSpot==spot) break;	// no progress

				// apply the step
				picked.Add( (DirEnum)lowDir );
				steps += DirEnumTraits.Steps( lowDir );
				spot = lowSpot;
			}

			return picked;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="map"></param>
		/// <param name="pathView"></param>
		/// <param name="who"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		internal Agent PickTarget( PlayMap map, FloodFillView pathView, Where start, int face, int faction ) {
//Console.Out.WriteLine( "PickTarget = " + start.ToDisplay() + "  face="+face+"  faction="+faction );

			var spot = pathView.View[start];

			foreach ( var dx in LOOK_ORDER ) { 

				// rotate around agent starting with forward, right, left
				var dirIx = (face + dx) % DIR_COUNT;

				var nextCtx = spot[dirIx];
				if (nextCtx==null) continue;	// cannot go off-grid

				var nextSpot = nextCtx.There;
				if (nextSpot.Value<0) continue;	// cannot enter negative

//Console.Out.WriteLine( "Considering NextSpot = " + (nextSpot.Here.Where.ToDisplay()) );
				var nextAgentId = nextSpot.Here.AgentId;
				if (nextAgentId>=0) {
					var nextAgent = map.Agents[nextAgentId];
					if (nextAgent.IsFoe(faction)) {	// !! Found It!
//Console.Out.WriteLine( "RETURNING NextAgent = " + nextAgent.Name + "  faction="+faction+"   "+nextAgent.Where.ToDisplay() + "  ID="+nextAgent.Ident );
						return nextAgent;
					}
				}
			}

//Console.Out.WriteLine( "RETURNING NULL");
			return null;
		}
	}
}
