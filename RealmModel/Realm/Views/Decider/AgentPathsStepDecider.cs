//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views.Decider {

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;
	using Realm.Views.Stepper;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// What is the shortest path to an enemy?
	/// </summary>

	public class AgentPathsStepDecider : StepDeciderIf {

		public AgentPathsStepDecider(Agent who) {
			this.Agent = who;
		}

		public Agent Agent { get; set; }

		/// <summary>
		/// All enemy locations are 'starts'
		/// </summary>
		/// <param name="map"></param>
		/// <returns></returns>
		public List<Where> GetStarts(PuzzleMap map) {
			var faction = Agent.Faction;
			var starts = new List<Where>();
			foreach ( var who in map.Agents ) {
//Console.WriteLine("CONSIDER="+who.ToDisplay());
				if ( who.Faction!=faction) starts.Add( who.Where );
			}			
			return starts;
		}
		
		/// <summary>
		/// For each step, can we enter?
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public StepDecision Decide( StepContext ctx, int value ) {

			var decision = new StepDecision();
			decision.Enter = CanStep( Agent, ctx );
			decision.Update = value + DirEnumTraits.All [ctx.DirIx].Steps;

			return decision;
		}


		/// <summary>
		/// Can an agent in the this place step into an adjacent place?
		/// </summary>
		/// <param name="who"></param>
		/// <param name="dir"></param>
		/// <returns>negative for 'no', or number of steps for 'yes'</returns>
		static internal bool CanStep( Agent who, StepContext ctx ) {

			int climb = who.Type.Climb;

			HeightEnum height = ctx.Dest.Height;

			// won't step into pit or wall
			if ( height==HeightEnum.Pit || height==HeightEnum.Wall ) return false;

			// abs height differential
			int nheight = (int)height;
			int fheight = (int) ctx.From.Height;
			int diff = (nheight>fheight ? nheight - fheight : fheight - nheight );

			// can't cross height differential
			if  ( climb<diff) return false;

			return true;
		}

	}
}
