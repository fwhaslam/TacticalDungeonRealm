using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Game {

	/// <summary>
	/// Order of acting agents.
	/// 
	/// Agents are always in the order, but may change position based on changes in state.
	/// * eg.  An agent wakes up -> becomes last in order ?
	/// * note that agents stay grouped by faction ... ?
	/// </summary>
	public class ActorQueue {

		public ActorQueue() {
			this.Actors = new List<Agent>();
		}

		public List<Agent> Actors {  get; internal set; }

		public Agent Next() {
			return Actors[0];
		}

		/// <summary>
		/// Remove, then move to end of list.
		/// </summary>
		/// <param name="agent"></param>
		public void NextTurn(Agent agent) {
			Actors.Remove(agent);
			Actors.Add(agent);
		}
	}
}
