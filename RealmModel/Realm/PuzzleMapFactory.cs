//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm {

	using System;
	using System.Collections.Generic;
	using System.Text;

	using Realm.Enums;
	using Realm.Game;
	using Realm.Puzzle;
	using Realm.Tools;

	/// <summary>
	/// Tools for producing and modifying puzzle maps.
	/// </summary>
	public class PuzzleMapFactory {

		static readonly Random random = new Random();

		static public PuzzleMap SimpleTerrain(int w,int t) {

			PuzzleMap map = PuzzleMap.Allocate( w, t );

			int hw = (w-1)/2;
			int ht = (t-1)/2;

			map.Places[hw,ht].Height = HeightEnum.Pit;
			map.Places[1+hw,ht].Height = HeightEnum.Pit;
			map.Places[hw,1+ht].Height = HeightEnum.Pit;
			map.Places[1+hw,1+ht].Height = HeightEnum.Pit;

			hw = ( hw<1 ? 0 : hw-1 );	// agent to one side

			map.AddAgent( AgentType.PEASANT, new Where(hw,ht), DirEnum.North );

			return map;
		}

		static public PuzzleMap RandomTerrain() {
			return GenerateTerrain(random.Next());
		}

		static public PuzzleMap GenerateTerrain(int seed) {

			Random rnd = new Random(seed);

			int wide = 5 + rnd.Next(11);
			int tall = 5 + rnd.Next(11);

			PuzzleMap map = PuzzleMap.Allocate(wide, tall);

			for (int w = 0; w < map.Wide; w++) for (int t = 0; t < map.Tall; t++) {
				map.Places[w, t].Height = (HeightEnum)rnd.Next(6);
			}

			// === add agents
			int agents = 3 + rnd.Next(12);
			int ax, at;

			for (int n = 0; n < agents; n++) {

				// find empty spot
				do {
					ax = rnd.Next(map.Wide);
					at = rnd.Next(map.Tall);
				} while (map.Places[ax, at].IsOccupied);

				DirEnum face = (DirEnum)rnd.Next(8);
				AgentType type = AgentType.Get(rnd.Next(AgentType.Count()));

				map.AddAgent(type, new Where(ax, at), face);
			}

			// === add flags
			int flags = 3 + rnd.Next(12);
			for (int n = 0; n < flags; n++) {

				// find empty spot
				do {
					ax = rnd.Next(map.Wide);
					at = rnd.Next(map.Tall);
				} while (map.Places[ax, at].Flag != FlagEnum.None);

				FlagEnum type = (FlagEnum)rnd.Next(FlagEnumTraits.Count());

				map.AddFlag(type, ax, at);
			}


			return map;
		}

		/// <summary>
		/// Used for testing agent power.
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		static public PuzzleMap SingleLineTerrain(int length) {
			return PuzzleMap.Allocate( length, 1 );
		}

//======================================================================================================================

		static public PuzzleMap AddRow(PuzzleMap work, DirEnum dir) {

			DirEnumTrait trait = DirEnumTraits.Trait((int)dir);
			if ( !trait.Orth ) throw new ArgumentException("Can only use orthogonal directions to add a row");

			// old wide/tall
			int ow = work.Wide, ot = work.Tall;
			// new wide/tall
			int nw = ow, nt = ot;
			// shift in copy
			int sw = 0, st = 0;

			switch (dir) {
				case DirEnum.North:  // tall top
					nt++;
					break;
				case DirEnum.South:  // tall zero
					nt++;
					st = 1;
					break;
				case DirEnum.East:  // wide top
					nw++;
					break;
				case DirEnum.West:  // wide zero
					nw++;
					sw = 1;
					break;
				default:
					throw new ArgumentException("Can only use orthogonal directions to add a row");
			}

			// copy old values into new layers
			PuzzleMap temp = PuzzleMap.Allocate(nw, nt);
			for (int wx = 0; wx < ow; wx++) {
				for (int tx = 0; tx < ot; tx++) {
					if (wx + sw < work.Wide || tx + st < work.Tall) {
						temp.Places[wx + sw, tx + st] = work.Places[wx, tx];
					}
				}
			}

			// shift some agents
			foreach (var who in work.Agents) {
				who.Where.X += sw;
				who.Where.Y += st;
				//Console.Out.WriteLine("AGENT AT ="+who.Where );
			}

			// copy over
			temp.Title = work.Title;
			temp.Agents = work.Agents;
			temp.Image = work.Image;
			temp.Text = work.Text;

			return temp;
		}

		static public PuzzleMap DropRow(PuzzleMap work, DirEnum dir) {

			DirEnumTrait trait = DirEnumTraits.Trait((int)dir);
			if ( !trait.Orth ) throw new ArgumentException("Can only use orthogonal directions to add a row");

			// old wide/tall
			int ow = work.Wide, ot = work.Tall;
			// new wide/tall
			int nw = ow, nt = ot;
			// shift in copy
			int sw = 0, st = 0;

			switch (dir) {
				case DirEnum.North:  // tall top
					nt--;
					break;
				case DirEnum.South:  // tall zero
					nt--;
					st = -1;
					break;
				case DirEnum.East:  // wide top
					nw--;
					break;
				case DirEnum.West:  // wide zero
					nw--;
					sw = -1;
					break;
				default:
					throw new ArgumentException("Can only use orthogonal directions to cut a row");
			}

			// copy old Places into new Places
			PuzzleMap temp = PuzzleMap.Allocate(nw, nt);
			//Console.Out.WriteLine("   sw="+sw+"  st="+st );
			for (int wx = 0; wx < nw; wx++) {
				for (int tx = 0; tx < nt; tx++) {
					if (wx - sw >= 0 && tx - st >= 0) {
						temp.Places[wx, tx] = work.Places[wx - sw, tx - st];
					}
				}
			}

			// remove dropped agents
			foreach (var who in work.Agents) {
				who.Where.X += sw;
				who.Where.Y += st;
				//Console.Out.WriteLine("AGENT AT ="+who.Where );
				// new spot is out of bounds
				if (who.Where.X >= nw || who.Where.Y >= nt || who.Where.X < 0 || who.Where.Y < 0) {
					work.Agents.Remove(who);
				}
			}


			// copy over
			temp.Title = work.Title;
			temp.Agents = work.Agents;
			temp.Image = work.Image;
			temp.Text = work.Text;

			return temp;
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
		/// Create an empty grid of places.
		/// </summary>
		/// <param name="grid"></param>
		/// <returns></returns>
		static internal Grid<Place> FillGrid( Grid<Place> grid ) {
			for (int dt=0;dt<grid.Tall;dt++) {
				for (int dw=0;dw<grid.Wide;dw++) {
					grid[dw,dt] = new Place( dw, dt );
				}
			}
			return grid;
		}

		/// <summary>
		/// String representation of the map.
		/// </summary>
		/// <returns></returns>
		static public List<string> MapAsStrings( PlayMap src ) {

			StringBuilder buf = new StringBuilder();

			List<string> list = new List<string>();
			for (int row = 0; row < src.Tall; row++) {
				buf.Clear();
				for (int col = 0; col < src.Wide; col++) {
//Console.Out.WriteLine("COL="+col+" row="+row);
					if (col > 0) buf.Append(STRING_PARTS_SEP);

					Place place = src.Places[col, row];
					buf.Append(HeightEnumTraits.Symbol(place.Height));
					buf.Append(FlagEnumTraits.Symbol(place.Flag));
					if (!place.IsOccupied) {
						buf.Append(NO_AGENT_SYMBOL);
					}
					else {
						buf.Append(place.AgentId.ToString(AGENT_ID_FORMAT));
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
		static public void StringsAsMap( PlayMap dst, List<string> source ) {

//Console.Out.WriteLine("PLAYMAP SIZE = "+dst.Wide+"/"+dst.Tall);

			// fix agent.Ident field
			if (dst.Agents==null) dst.Agents = new List<Agent>();

			List<Agent> agents = dst.Agents;
			for (int id=0;id<agents.Count;id++) {
				if (agents[id]!=null) agents[id].Ident = id;
			}

			// craete and parse grid
			dst.Places = new Grid<Place>(dst.Wide,dst.Tall);
			Grid<Place> places = dst.Places;

			for (int row = 0; row < dst.Tall; row++) {

				string line = source[row];
				string[] parts = line.Split(STRING_PARTS_SEP);

				for (int col = 0; col < dst.Wide; col++) {

					Place place = new Place( col, row );
					places[col,row] = place;
					string part = parts[col];

					// parse single location
					place.Height = HeightEnumTraits.FromSymbol(part[0]);
					place.Flag = FlagEnumTraits.FromSymbol(part[1]);

					string symbol = "" + part[2] + part[3];
					if (!symbol.Equals(NO_AGENT_SYMBOL)) {

						int agentId;
						try { agentId = int.Parse(symbol); }
						catch (FormatException ex) { 
							throw new FormatException("Invalid agent symbol [" + symbol + "] at [" + col + "/" + row + "]", ex); 
						}

						if (agentId > agents.Count) {
							throw new FormatException("No agent defined for id=[" + agentId + "] at [" + col + "/" + row + "]");
						}
						place.AgentId = agentId;
					}
				}

			}
		}

	}

}
