//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using System;
	using System.Collections.Generic;
	using Realm.Enums;
	using Realm.Tools;
	using System.Linq;

	using static Realm.Tools.MapTools;
	using System.Text;
	using YamlDotNet.Serialization;
	using System.Diagnostics.CodeAnalysis;
	using System.Collections;

	/// <summary>
	/// Representation of the play region.  Always a rectangle.
	/// Each tile is a square, has height, type, and sometimes an item.
	/// Items are Agents and Objects.  Agents are Heros and Villains.
	/// Objects are interactive terrain.
	/// </summary>
	public class PuzzleMap {

		internal Dictionary<string, string> textMap = new Dictionary<string, string>();

		/// <summary>
		/// Provided to support YAML.
		/// </summary
		public PuzzleMap() { }

		static public PuzzleMap Allocate(int w, int t) {

			var work = new PuzzleMap();

			work.Title = "Empty Map";
			work.Image = "pic1.png";
			work.Text["Start"] = "Some Story";

			work.Wide = w;
			work.Tall = t;

			work.Places = FillGrid( new Grid<Place>( w, t ) );

			work.Agents = new List<Agent>();

			return work;
		}

		static internal Grid<Place> FillGrid( Grid<Place> grid ) {
			for (int dt=0;dt<grid.Tall;dt++) {
				for (int dw=0;dw<grid.Wide;dw++) {
					grid[dw,dt] = new Place( dw, dt );
				}
			}
			return grid;
		}

//======================================================================================================================

		public string Title { get; set; }

		public string Image { get; set; }

		public int Wide { get; set; }

		public int Tall { get; set; }

		public List<Agent> Agents { get; internal set; }

		/// <summary>
		/// Places become 'Map' for Yaml storage.
		/// </summary>
		[YamlIgnore]
		public Grid<Place> Places { get; internal set; }

		public List<string> Map { get => MapAsStrings(); set => StringsAsMap(value); }

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

			// may need to displace another agent, set in spot
			Place spot = Places[loc.X, loc.Y];
			if (spot.Agent != null) {
				DropAgent(spot.Agent);
			}
			spot.Agent = who;

			// location in list
			var agentId = Agents.IndexOf(null);
			if (agentId<0) {
				agentId = Agents.Count;
				Agents.Add(who);
			}
			else {
				Agents[agentId] = who;
			}
			who.Ident = agentId;

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
				Agents[who.Ident] = null;
				Places[who.Where.X, who.Where.Y] = null;
			}
		}

		public void AddFlag(FlagEnum type, int x, int y) {
			Places[x, y].Flag = type;
		}

		public void DropFlag(int x, int y) {
			Places[x, y].Flag = FlagEnum.None;
		}

//======================================================================================================================

		// format: HeightChar, FlagChar, AgentID(##)
		//static readonly string PART_FORMAT = "%c%c%02d";

		// one 'part' is one 'tile'
		static readonly char STRING_PARTS_SEP = '/';

		// agent ids
		static readonly string NO_AGENT_SYMBOL = "__";
		static readonly string AGENT_ID_FORMAT = "00";

//======================================================================================================================

		/// <summary>
		/// String representation of the map.
		/// </summary>
		/// <returns></returns>
		List<string> MapAsStrings() {

			StringBuilder buf = new StringBuilder();

			List<string> list = new List<string>();
			for (int row = 0; row < Tall; row++) {
				buf.Clear();
				for (int col = 0; col < Wide; col++) {
					if (col > 0) buf.Append(STRING_PARTS_SEP);

					Place place = Places[col, row];
					buf.Append(HeightEnumTraits.Symbol(place.Height));
					buf.Append(FlagEnumTraits.Symbol(place.Flag));
					if (place.Agent == null) {
						buf.Append(NO_AGENT_SYMBOL);
					}
					else {
						int indexOf = Agents.IndexOf(place.Agent);
						buf.Append(indexOf.ToString(AGENT_ID_FORMAT));
					}
				}
				list.Add(buf.ToString());
			}
			return list;
		}

		/// <summary>
		/// Use strings to reconstruct map.
		/// </summary>
		/// <param name="source"></param>
		public void StringsAsMap(List<string> source) {

			// fix agent.Ident field
			if (Agents==null) Agents = new List<Agent>();
			for (int id=0;id<Agents.Count;id++) {
				if (Agents[id]!=null) Agents[id].Ident = id;
			}

			// craete and parse grid
			Places = FillGrid( new Grid<Place>( Wide, Tall ) );

			for (int row = 0; row < Tall; row++) {

				string line = source[row];
				string[] parts = line.Split(STRING_PARTS_SEP);

				for (int col = 0; col < Wide; col++) {

					Place place = Places[col, row];
					string part = parts[col];

					// parse single location
					place.Height = HeightEnumTraits.FromSymbol(part[0]);
					place.Flag = FlagEnumTraits.FromSymbol(part[1]);

					string symbol = "" + part[2] + part[3];
					if (!symbol.Equals(NO_AGENT_SYMBOL)) {

						int agentId;
						try { agentId = int.Parse(symbol); }
						catch (FormatException ex) { throw new FormatException("Invalid agent symbol [" + symbol + "] at [" + col + "/" + row + "]", ex); }

						if (agentId > Agents.Count)
							throw new FormatException("No agent defined for id=[" + agentId + "] at [" + col + "/" + row + "]");
						place.Agent = Agents[agentId];
					}
				}

			}
		}

	}
}