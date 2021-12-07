﻿//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Game {
	using Realm.Brain;
	using Realm.Puzzle;

	using Realm.Enums;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;


	/// <summary>
	/// A game is a puzzle in progress.  The game states are organized into 'actions' and 'turns'.
	/// An action is a the advancement of a single agent with onboard consequences.  
	/// A turn is when all agents have acted and we progress to a new round of actions.
	/// Each action and turn creates a snapshot state of the puzzle, so there is complete playback.
	/// </summary>
	public class PuzzleGame {

		public PuzzleGame() : this(new PuzzleMap(), new SimpleBrain() ) {}

		public PuzzleGame( PuzzleMap start, BrainIf mind ) {

			this.Snapshots = new List<PuzzleSnapshot>();
			this.Snapshots.Add( new PuzzleSnapshot( start ) );	// slot zero

			this.Mind = mind;

			this.Queue = new ActorQueue();
		}

		// one brain per active faction, with PlayerBrain as zero
		public BrainIf Mind {  get; set; }

		public GameStateEnum State { get; set; }

		// snapshot of puzzle after individual agent action-chains :: do we also need marks at 'turn' points?
		public List<PuzzleSnapshot> Snapshots { get; internal set; }

		public ActorQueue Queue { get; internal set; }

//======================================================================================================================

		/// <summary>
		/// Who will perform the next action
		/// </summary>
		/// <returns></returns>
		public Agent GetNextActor() {
			return Queue.Next();
		}

		public PuzzleMap GetCurrentMap() {
			return Snapshots.Last<PuzzleSnapshot>().Puzzle;
		}

		/// <summary>
		/// Mind will build the next action for the current agent.
		/// </summary>
		/// <returns></returns>
		public ActionChain GetNextAction() {
			return Mind.ChooseAction( GetCurrentMap(), GetNextActor() );
		}

		public void ApplyAction( ActionChain action ) {
			var nextMap = ActionHandler.Apply( GetCurrentMap(), action );
			Snapshots.Add( new PuzzleSnapshot( action, nextMap ) );
			Queue.NextTurn( action.Actor );
		}

		//var state = game.GetState();
		//while (state!=GameState.Stable) { 
		//	var agent = game.NextAgent();
		//	var action = game.NextAction();
		//	var state = game.ApplyAction( action );
		//	Print("Game State = "+state);
		//}


//======================================================================================================================


		///// <summary>
		///// Is the game state such the player must take actions ?
		///// </summary>
		///// <returns></returns>
		//public bool IsPlayerTurn() {
		//	return true;
		//}

		//public bool IsGameOver() {
		//	return false;
		//}

		///// <summary>
		///// True when it is NOT the playersw turn, and game is not over.
		///// </summary>
		///// <returns></returns>
		//public bool HasNext() {
		//	return true;
		//}

		///// <summary>
		///// What are all the enemy faction actions?
		///// </summary>
		///// <returns></returns>
		//public List<ActionChain> Next() {
		//	return null;
		//}

		//public PuzzleSnapshot GetState( int turnId ) {
		//	return turnSnapshots[turnId];
		//}

		//public void SetFirstSnapshot( PuzzleMap map ) {
		//	turnSnapshots[0] SetStartStatemap );
		//}

		//public void SetState( int turnId, ActionChain actions, PuzzleMap map ) {
		//	turnSnapshots[turnId] = new PuzzleSnapshot( actions,map );
		//}



			//		var state = game.GetState();
			//while (state!=GameState.Stable) { 
			//	var agent = game.NextAgent();
			//	var action = game.NextAction();
			//	var state = game.ApplyAction( action );
			//	Print("Game State = "+state);
			//}
	}

}
