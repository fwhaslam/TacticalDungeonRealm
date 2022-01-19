//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

#define ACTION_HANDLER_NOT_VERBOSE

namespace Realm.Game {

	using Realm.Game.Action;
	using Realm.Puzzle;
	using Realm.Enums;
	using System;

	/// <summary>
	/// Apply an action to a PuzzleMap to produce a NEW PuzzleMap.
	/// </summary>
	public class ActionHandler {

		static internal string DescAgent( Agent who ) {
			return who.Name+"/"+who.Ident;
		}

		static public void Apply( PlayMap map, GameTurn turn ) {

			Agent who = turn.Actor;

			// prep agent
			if (who!=null) {
#if (DEBUG && ACTION_HANDLER_VERBOSE)
Console.Out.WriteLine("PREPPING agentId={0} ",DescAgent(who));
#endif
				if (who.Status!=StatusEnum.Active) map.DoStatus( who, StatusEnum.Active );
			}

			// apply move
			if (turn.Move!=null) { 
				foreach ( var dir in turn.Move ) {
#if (DEBUG && ACTION_HANDLER_VERBOSE)
Console.Out.WriteLine("MOVE agentId={0} towards {1}",DescAgent(who),dir);
#endif
					map.DoStep( who, dir );
				}
			}

			// apply attack
			if (turn.Attack!=null) {
#if (DEBUG && ACTION_HANDLER_VERBOSE)
var target = map.Agents[turn.Attack.TargetId];
Console.Out.WriteLine("AGENT fromId={0} destId={1}  dmg={2}",DescAgent(who),DescAgent(target),turn.Attack.Damage);
#endif

				map.DoInjure( turn.Attack.TargetId, turn.Attack.Damage );
			}

			// apply attack
			if (turn.Defend!=null) {
#if (DEBUG && ACTION_HANDLER_VERBOSE)
Console.Out.WriteLine("BLOCKING agentId={0}",DescAgent(who));
#endif
				map.DoStatus( who, StatusEnum.Blocking );
			}

			// apply sprint
			if (turn.Sprint!=null) { 
				foreach ( var dir in turn.Sprint ) {
#if (DEBUG && ACTION_HANDLER_VERBOSE)
Console.Out.WriteLine("SPRINT agentId={0} towards {1}",DescAgent(who),dir);
#endif
					map.DoStep( who, dir );
				}
			}
		}

	}
}
