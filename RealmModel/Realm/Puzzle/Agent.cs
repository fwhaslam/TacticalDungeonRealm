//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using Realm.Enums;
	using Realm.Tools;

	using System;
	using System.ComponentModel;

	using YamlDotNet.Serialization;

	public class Agent : CloneableIf<Agent> {

		/// <summary>
		/// Provided to support YAML.
		/// </summary>
		public Agent() { }

		public Agent(int x, int y) {
			this.Where = new Where(x,y);
		}

		public Agent(Where loc) {
			this.Where = new Where(loc);
		}

		public Agent(Agent src) {
			this.Where = ( src.Where==null ? null : new Where(src.Where) );
			this.Type = src.Type;
			this.Face = src.Face;
			this.Faction = src.Faction;
			this.Ident = src.Ident;
			this.Status = src.Status;
			this.Wounds = src.Wounds;
		}
		
		public Agent Clone() {
			return new Agent( this );
		}

		public string ToDisplay() {
			return "Agent[ident="+Ident+",type="+Name+",face="+Face+","+Where.ToDisplay()+",status="+Status+",faction="+Faction+"]";
		}

//======================================================================================================================

		/// <summary>
		/// The identifier is the agents index in the list of Agents.
		/// This is set when the Agents list if built, so is not stored to YAML.
		/// </summary>
		[YamlIgnore]
		public int Ident {  get; set; }

		/// <summary>
		/// Type is summarized to AgentType.Name.
		/// The full Type is not stored to SaveFiles.
		/// </summary>
		[YamlIgnore]
		public AgentType Type { get; set; } = AgentType.PEASANT;

		[YamlMember(Alias = "name")]
		public string Name {
			get => Type.Name;
			set => Type = AgentType.Get(value);
		}

		[YamlMember(Alias = "face")]
		[DefaultValue(-1)]
		public DirEnum Face { get; set; } = DirEnum.North;

		/// <summary>
		/// Location does not need to be stored to SaveFiles.
		/// </summary>
		[YamlIgnore]
		public Where Where { get; set; }

		[YamlMember(Alias = "status")]
		[DefaultValue(-1)]
		public StatusEnum Status { get; set; } = StatusEnum.Alert;

		[YamlMember(Alias = "faction")]
		[DefaultValue(-1)]
		public int Faction { get; set; } = 0;

		[YamlMember(Alias = "wounds")]
		[DefaultValue(0)]
		public int Wounds { get; set; } = 0;

//======================================================================================================================

		public bool IsFoe( Agent who ) {
			return who.Faction != this.Faction;
		}

		public bool IsFoe( int faction ) {
			return faction != this.Faction;
		}
		public bool IsAlly( Agent who ) {
			return who.Faction == this.Faction;
		}

		public int DamageTo( Agent defender, int adds ) {

			if (defender.Status==StatusEnum.Blocking) adds--;

			return Type.DamageTo( defender.Type, adds ) ;
		}

		public bool IsActionReady() { 
			return Status==StatusEnum.Active || Status==StatusEnum.Blocking; 
		}

		public bool IsDefeated() { return Status==StatusEnum.Dead; }

		Agent CloneableIf<Agent>.Clone() {
			throw new NotImplementedException();
		}
	}
}
