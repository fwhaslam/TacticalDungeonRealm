using Realm.Enums;
using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Game {

	public class ActionFactory {

		static public Action MakeAction( ActionTypeEnum type ) {
			Debug.Assert( ! ActionTypeTraits.IsComplex(type) );
			return new Action( type );
		}
				
		static public void Add( ActionChain chain, ActionTypeEnum type ) {
			chain.Actions.Add(  MakeAction(type) );
		}
		
		static internal void AddSafe( ActionChain chain, ActionTypeEnum type ) {
			chain.Actions.Add(  new Action(type) );
		}

		static public void AddTurns( ActionChain chain, int start, int end ) {
			var turns = DirEnumTraits.Turns( start, end );
			var type = ( turns<0 ? ActionTypeEnum.Left : ActionTypeEnum.Right );
			if (type==ActionTypeEnum.Left) turns = -turns;
			for (int tx=0;tx<turns;tx++) AddSafe( chain, type );
		}
		
		/// <summary>
		/// Default or starting ActionChain.
		/// </summary>
		/// <param name="who"></param>
		/// <returns></returns>
		static public ActionChain EmptyChain(Agent who) {
			return new ActionChain(who);
		}

		static public void AddForward( ActionChain chain ) {
			chain.Actions.Add(  new Action(ActionTypeEnum.Forward ) );
		}

		static public Action AddAttack( ActionChain chain, int agentId, int dmg ) {

			var action = new Action( ActionTypeEnum.Attack );
			action.Target = agentId;
			action.Damage = dmg;

			chain.Actions.Add( action );
			return action;
		}

	}
}
