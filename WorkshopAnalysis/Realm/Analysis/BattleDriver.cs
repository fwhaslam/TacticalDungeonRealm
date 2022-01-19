//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Analysis {

	using Realm.Brain;
	using Realm.Enums;
	using Realm.Game;
	using Realm.Puzzle;

	using System;
	using System.Collections.Generic;
	using System.Linq;

	using static Realm.Tools.YamlTools;

	/// <summary>
	/// Wrapper for a single battle.
	/// </summary>
	public class BattleDriver {

		static readonly int MAX_FACTIONS = 2;

		static public readonly FlagEnum[] FactionEntryFlag = {  FlagEnum.Entry, FlagEnum.Exit };

		public BattleDriver( PuzzleMap map) { 
			this.Template = (PuzzleMap)map.Clone();
			this.Minds = new Dictionary<int,BrainIf>();
		}

		public PuzzleMap Template { get; internal set; }

		public Dictionary<int,BrainIf> Minds { get; internal set; }

		public float[] Success { get; internal set; } = { 0f, 0f };

		/// <summary>
		/// Map should have flags to mark starting location for agents.
		/// </summary>
		/// <param name="types"></param>
		public void FillFaction ( int factionId, BrainIf mind, AgentType[] types ) {

			bool added = AddAgentsToMap( factionId, types );
			if ( !added ) {
				throw new WorkshopAnalysisException( "BattleDriver not enough locations for team["+factionId+"].");
			}

			this.Minds[factionId] = mind;
		}

		/// <summary>
		/// Fill agents into map to prepare game.
		/// </summary>
		/// <param name="factionId"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		internal bool AddAgentsToMap( int factionId, AgentType[] types ) {

			int limit = types.Length;
			FlagEnum startFlag = FactionEntryFlag[factionId];

//Console.Out.WriteLine("  BD SCANNING = ["+Template.Title+"]" );
			var found  = Template.Places.Where(p => (p.Flag==startFlag) );
//Console.Out.WriteLine("  BD FOUND = "+ToYamlString(found) );
			var spots = new List<Place>( found );

			// second faction fills in spots backwards
			if (factionId==1)  spots.Reverse();
			if (spots.Count<limit) return false;

//Console.Out.WriteLine("SPOTS COUNT ["+factionId+"] = "+spots.Count );
			for ( int ix=0; ix<spots.Count(); ix++ ) {
				var who = Template.AddAgent( types[ix],  spots[ix].Where, DirEnum.North, factionId, StatusEnum.Active );
//Console.Out.WriteLine("   BD ADD = ["+ToYamlString(who)+"]  where="+ToYamlString(who.Where));
			}

			return true;
		}

		public void Drive( int roundLimit ) {

#if DEBUG
Console.Out.WriteLine("=====================================================================================================");
Console.Out.WriteLine(  "BATTLE:  ["+Template.Title+"]  for ["+Template.Agents[0].Name+"] vs ["+Template.Agents[1].Name+"]");
//Console.Out.WriteLine("MAP+"+ToYamlString(Template));
//Console.Out.WriteLine("=====================================================================================================");
#endif


			var game = Prepare();

			while ( !game.IsOver() && game.CurrentRound()<roundLimit ) {

				var action = game.GetNextAction();
//Console.Out.WriteLine("ACTION = "+ToYamlString(action));

				//game.ApplyAction( action );
				game.DriveAction( action );

#if DEBUG
	if (action.NextRound==true) {
		Console.Out.WriteLine(">>> =============== Next Round "+game.CurrentRound() );
	}
#endif

				game.Advance();

			}

			Evaluate( game );
		}


		internal PuzzleGame Prepare() {

			var game = new PuzzleGame( Template );

			// set minds
			game.Minds[0] = Minds[0];
			game.Minds[1] = Minds[1];

			return game;
		}

		internal void Evaluate( PuzzleGame game) {

			// injury for factions zero + one
			int[] health = new int[ MAX_FACTIONS ];
			int[] wounds = new int[ MAX_FACTIONS ];

			foreach ( var who in game.GetCurrentMap().Agents ) {
				int faction = who.Faction;
				health[faction] += who.Type.Health;
				wounds[faction] += who.Wounds;
			}

			for ( int factionId=0; factionId<MAX_FACTIONS; factionId++ ) {
				Success[factionId] = wounds[factionId] * 1f / health[factionId];
			}

		}

	}
}
