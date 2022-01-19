//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using System;
	using System.Collections.Generic;
	using Realm.Enums;
	using Realm.Tools;
	using System.Linq;

	using System.Text;
	using YamlDotNet.Serialization;
	using Realm.Game;

	/// <summary>
	/// Representation of the play region.  Always a rectangle.
	/// Each tile is a square, has height, type, and sometimes an item.
	/// Items are Agents and Objects.  Agents are Heros and Villains.
	/// Objects are interactive terrain.
	/// 
	/// This is the play 'template'.  This builds the PlayMap for PuzzleGame.
	/// </summary>
	public class PuzzleMap : PlayMap, CloneableIf<PuzzleMap> {

		internal Dictionary<string, string> textMap = new Dictionary<string, string>();

		/// <summary>
		/// Provided to support YAML.
		/// </summary
		public PuzzleMap() { }

		public PuzzleMap(PuzzleMap src) : base((PlayMap)src) { 
				
			//this.Agents =  src.Agents.Select( a => (Agent)a.Clone() ).ToList<Agent>();
			//this.Places =  src.Places.Clone();

			this.Title = src.Title;
			this.Image = src.Image;
			this.Text = new Dictionary<string,string>(src.Text);
		}

		public new PuzzleMap Clone() {
			return new PuzzleMap(this);
		}

		static public PuzzleMap Allocate(int w, int t) {

			var work = new PuzzleMap();
			work.Wide = w;
			work.Tall = t;

			work.Title = "Empty Map";
			work.Image = "pic1.png";
			work.Text["Start"] = "Some Story";

			work.Agents = new List<Agent>();
			work.Places = new Grid<Place>( w, t );
			for (int dw=0;dw<w;dw++) for (int dt=0;dt<t;dt++) work.Places[dw,dt] = new Place(dw,dt);

			return work;
		}

//======================================================================================================================

		public string Title { get; set; }

		public string Image { get; set; }

		public Dictionary<string, string> Text { get => textMap; set => textMap = value; }

//======================================================================================================================

		/// <summary>
		/// Create an agent on the map, removing any in the way.
		/// Faction=0, Status=Alert
		/// </summary>
		public Agent AddAgent(AgentType type, Where loc, DirEnum face) {
			return AddAgent( type, loc, face, 0, StatusEnum.Alert );
		}

		/// <summary>
		/// Create an agent on the map, removing any in the way.
		/// </summary>
		public Agent AddAgent(AgentType type, Where loc, DirEnum face, int faction, StatusEnum status) {

			// agent instance
			Agent who = new Agent(loc);
			who.Type = type;
			who.Face = face;
			who.Faction = faction;
			who.Status = status;

			// find index in list
			who.Ident = Agents.IndexOf(null);
//Console.Out.WriteLine(" >>>> ADDING AGENT = "+who.Ident+"/"+Agents.Count );
			if (who.Ident<0) {
				who.Ident = Agents.Count;
				Agents.Add(who);
			}
			else {
				Agents[who.Ident] = who;
			}
			
			// may need to displace another agent, set in spot
			Place spot = Places[loc.X, loc.Y];
//Console.Out.WriteLine("PLACES LOC="+loc.ToDisplay()+"   Agent="+spot.AgentId );
			if (spot.IsOccupied) {
				var them = Agents[spot.AgentId];
//Console.Out.WriteLine("PLACES LOC="+loc.ToDisplay()+"   Agent="+them.ToDisplay() );
				DropAgent(them);
			}
			spot.AgentId = who.Ident;

			// what have we wrought
			return who;
		}

		/// <summary>
		/// Agents use their position in the list as an Identifier.
		/// So we cannot reduce the list, just replace agents with nulls.
		/// </summary>
		/// <param name="who"></param>
		public void DropAgent(Agent who) {
			if (who != null) {
//Console.Out.WriteLine("DROP AGENT = "+who.Ident+"  LIST SiZE = "+Agents.Count +"   where="+who.Where.ToDisplay() );
				Agents[who.Ident] = null;
				Places[who.Where.X, who.Where.Y].AgentId = Place.NO_AGENT_ID;
			}
		}

		public void AddFlag(FlagEnum type, int x, int y) {
			Places[x, y].Flag = type;
		}

		public void DropFlag(int x, int y) {
			Places[x, y].Flag = FlagEnum.None;
		}

	}
}