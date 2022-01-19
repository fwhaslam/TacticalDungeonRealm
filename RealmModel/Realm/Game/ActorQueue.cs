//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Game {
	
	using System;
	using System.Linq;
	using Realm.Puzzle;
	using System.Collections.Generic;

	internal class ActorRef {

		internal ActorRef( int agentId, int faction) {
			this.AgentId = agentId;
			this.Faction = faction;
			this.Ready = true;
		}
		// agent index
		internal int AgentId {  get; set; }
		// which faction owns the agent
		internal int Faction {  get; set; }
		// is the agent ready to act, or done acting
		internal bool Ready { get; set; }
	}

	/// <summary>
	/// Order of acting agents.
	/// 
	/// Agents may be in any order.  When scanning for a faction
	///  * find first member of faction who has not acted
	///  * after they act, move them to end of list.
	///  * when adding a new agent, add to end of list.
	///  
	/// How do we khnow that an agent has acted ?
	/// 
	/// </summary>
	public class ActorQueue {

		static public readonly int ROUND_ADVANCE = -2;
		static public readonly int FACTION_ADVANCE = -1;

		public ActorQueue() {}
		public ActorQueue( List<Agent> agents ) {
			foreach ( var who in agents ) AddActor( who );
		}

		/// <summary>
		/// Which faction is currently acting?
		/// </summary>
		public int Faction { get; internal set; } = 0;

		/// <summary>
		/// What is the Maximum Faction number.
		/// </summary>
		public int MaxFaction {  get; internal set; } = 0;

		/// <summary>
		/// List of Agent Ids.
		/// </summary>
		internal List<ActorRef> Actors {  get; } = new List<ActorRef>();

//======================================================================================================================

		public void AddActor( Agent who ) {
			var faction = who.Faction;
			Actors.Add( new ActorRef( who.Ident, faction ) );
			if (faction>MaxFaction) MaxFaction = faction;
		}

		/// <summary>
		/// Returns next acting agentId without removing.
		/// </summary>
		/// <returns>-2 means round advance; -1 means faction advance.</returns>
		public int Peek() {

			var nextRef = Find();

			if (nextRef!=null) return nextRef.AgentId;

			// next round or next faction
			return AdvanceCode();
		}

		internal int AdvanceCode() {
			return ( Faction==MaxFaction ? ROUND_ADVANCE : FACTION_ADVANCE );
		}

		/// <summary>
		/// Returns acting agentId, moves to end of list.
		/// </summary>
		/// <returns>-1 means faction change.</returns>
		public int Pop() {

//Console.Out.WriteLine("POP start = ["+Actors.Count+"]  FirstId="+Actors[0].AgentId);
			var nextRef = Find();

			if (nextRef==null) {
				int result = AdvanceCode();
				NextFaction();
//Console.Out.WriteLine("POP faction = ["+Actors.Count+"]  FirstId="+Actors[0].AgentId);
				return result;
			}
			else {
				Actors.Remove( nextRef );
				nextRef.Ready = false;
				Actors.Add( nextRef );
//Console.Out.WriteLine("POP rotate = ["+Actors.Count+"]  FirstId="+Actors[0].AgentId);
				return nextRef.AgentId;
			}
		}

		/// <summary>
		/// Next AgentId or null for change of faction.
		/// </summary>
		/// <returns></returns>
		internal ActorRef Find() {
			return Actors.FirstOrDefault( r => ( r.Ready && r.Faction==this.Faction) );
		}

		/// <summary>
		/// Rotate active faction.
		/// </summary>
		internal void NextFaction() {
			Faction = (Faction+1) % (MaxFaction+1);
			Actors.ForEach( a => { if (a.Faction==this.Faction) a.Ready = true; } );
		}

	}
}
