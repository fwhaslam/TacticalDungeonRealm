using Realm.Puzzle;

using Realm.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realm.Tools;

namespace Realm.Game {
	

	/// <summary>
	/// An agent can take multiple sequential actions in his turn.
	/// We are calling this an action chain.
	/// </summary>
	public class ActionChain {

		public ActionChain(Agent agent) {
			this.Actor = agent;
			Actions = new List<Action>();
		}

		public Agent Actor { get; internal set; }

		public List<Action> Actions {  get; set; }

	}
}
