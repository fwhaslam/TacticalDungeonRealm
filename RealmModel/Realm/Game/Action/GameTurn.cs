//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Game.Action {
	
	using Realm.Puzzle;
	using Realm.Enums;
	using System.Collections.Generic;
	using YamlDotNet.Serialization;
	using System.ComponentModel;


	/// <summary>
	/// An agent can take multiple sequential actions in his turn.
	/// 
	/// Standard is two actions:
	///		* Move / Attack / Defend / Sprint
	///	
	/// NonAgent ( agentId == -1 )
	///		* next faction, next round
	///		
	/// </summary>
	public class GameTurn {

		public GameTurn(Agent agent) { this.Actor = agent; }

		public GameTurn(int advance) {
			this.Actor = null; 
			this.NextFaction = ( advance == ActorQueue.FACTION_ADVANCE );
			this.NextRound = ( advance == ActorQueue.ROUND_ADVANCE );
		}

		[YamlIgnore]
		public Agent Actor { get; internal set; }

		/// <summary>
		/// Provided for SaveFile storage
		/// </summary>
		[YamlMember( Alias = "actorId")]
		[DefaultValue(-1)]
		public int ActorId { get { return (Actor==null ? -1 : Actor.Ident); } }

		[YamlMember( Alias = "move")]
		[DefaultValue(null)]
		public List<DirEnum> Move { get; set; }

		[YamlMember( Alias = "attack")]
		[DefaultValue(null)]
		public AttackAction Attack { get; set; }

		[YamlMember( Alias = "defend")]
		[DefaultValue(null)]
		public bool? Defend { get; set; }

		[YamlMember( Alias = "sprint")]
		[DefaultValue(null)]
		public List<DirEnum> Sprint { get; set; }

		// Advance the Round
		[YamlMember( Alias = "nextRound")]
		[DefaultValue(null)]
		public bool? NextRound { get; set; }

		// Advance the Faction
		[YamlMember( Alias = "nextFaction")]
		[DefaultValue(null)]
		public bool? NextFaction { get; set; }

	}
}
