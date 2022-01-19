//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Game {

	using System;
	using System.Linq;
	using System.Collections.Generic;

	using Realm.Brain;
	using Realm.Puzzle;
	using Realm.Enums;
	using Realm.Game.Action;

	using static Realm.Tools.YamlTools;

	/// <summary>
	/// A game is a puzzle in progress.  The game states are organized into 'actions' and 'turns'.
	/// An action is a the advancement of a single agent with onboard consequences.  
	/// A turn is when all agents have acted and we progress to a new round of actions.
	/// Each action and turn creates a snapshot state of the puzzle, so there is complete playback.
	/// </summary>
	public class PuzzleGame {

		public PuzzleGame() : this(new PuzzleMap(), new ChaseBrain() ) {}

		public PuzzleGame( PuzzleMap map ) : this( map, new ChaseBrain() ) {}

		public PuzzleGame( PuzzleMap template, BrainIf mind ) {

			this.Template = template;
			this.Snapshots = new List<PlaySnapshot>();
			var start = new PlayMap( template );
			this.Snapshots.Add( new PlaySnapshot( start ) );	// slot zero

			this.Queue = new ActorQueue( start.Agents );
			var maxFaction = 1 + Queue.MaxFaction;

			this.Minds = new BrainIf[ maxFaction ];
			for (int ix=0;ix<maxFaction;ix++) this.Minds[ix] = mind;

			RoundIndex = new List<int>();
			RoundIndex.Add( 0 );		// first turn points to first snapshot.
		}

		public PuzzleMap Template { get; internal set; }

		// one brain per active faction :: Player = PlayerBrain
		public BrainIf[] Minds {  get; internal set; }

		public GameStateEnum State { get; set; }

		// snapshot of map after individual agent turns :: do we also need marks at 'turn' points?
		public List<PlaySnapshot> Snapshots { get; internal set; }

		public ActorQueue Queue { get; internal set; }

//======================================================================================================================

		// index into snapshots for start of round
		public List<int> RoundIndex { get; internal set; }

		public int CurrentRound() {  return RoundIndex.Count; }

		/// <summary>
		/// No more actions are possible.
		/// </summary>
		/// <returns></returns>
		public bool IsOver() {
			int first = -1;
			foreach ( var agent in GetCurrentMap().Agents ) {
				if (agent.IsDefeated()) continue;
				if (first<0) { 
					first = agent.Faction; 
				}
				else {
					if (agent.Faction!=first) return false;
				}
			}
			return true;
		}

//======================================================================================================================

		public PlayMap GetCurrentMap() {
			return Snapshots.Last<PlaySnapshot>().Arena;
		}

		/// <summary>
		/// Mind will build the next action for the current agent.
		/// </summary>
		/// <returns></returns>
		public GameTurn GetNextAction() {

			var agentCode = Queue.Peek();
			if (agentCode<0) return new GameTurn( agentCode );

			var map = GetCurrentMap();
			var actor = map.Agents[ agentCode ];
			var mind = Minds[actor.Faction];

			return mind.ChooseAction( map, actor );
		}

		/// <summary>
		/// For Player games, we apply action, making snapshots at each step.
		/// </summary>
		/// <param name="action"></param>
		public void ApplyAction( GameTurn action ) {
			ApplyAction( action, true );
		}

		/// <summary>
		/// For analysis, we apply actions without making snapshots.
		/// </summary>
		/// <param name="action"></param>
		public void DriveAction( GameTurn action ) {
			ApplyAction( action, false );
		}


		/// <summary>
		/// UIse an action to alter the underlying map.  
		/// For Player games we make snapshots, for analysis we do not.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="makeSnapshots"></param>
		internal void ApplyAction( GameTurn action, Boolean makeSnapshots ) {

//Console.Out.WriteLine("SNAPSHOT COUNT = "+Snapshots.Count);
//Console.Out.WriteLine("====================\n" + ToYamlString(GetCurrentMap()) + "\n====================");

			var workMap = GetCurrentMap();
			if (makeSnapshots) {
				workMap = workMap.Clone();
				Snapshots.Add( new PlaySnapshot( action, workMap ) );
			}
			
			ActionHandler.Apply( workMap, action );

			// mark a new round
			if (action.NextRound??false) {
				RoundIndex.Add( Snapshots.Count );
			}

		}

		public void Advance() {
			Queue.Pop();
		}

	}

}
