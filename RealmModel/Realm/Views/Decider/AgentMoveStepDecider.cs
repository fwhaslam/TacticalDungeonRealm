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
	/// Given a StopContext, can an Agent enter?
	/// </summary>

	public class AgentMoveStepDecider : StepDeciderIf {

		public AgentMoveStepDecider(Agent who) {
			this.Agent = who;
		}

		public List<Where> GetStarts(PuzzleMap map) {
			var starts = new List<Where>();
			starts.Add( Agent.Where );
			return starts;
		}

		public Agent Agent { get; set; }

		public StepDecision Decide( StepContext ctx, int value ) {

			var decision = new StepDecision();
			decision.Enter = CanStep( Agent, ctx );
			decision.Update = value + DirEnumTraits.All [ctx.DirIx].Steps;

			// too far to move
			if ( decision.Update > Agent.Type.Steps ) decision.Enter = false;

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

			// cannot step onto another agent
			if (ctx.Dest.Agent!=null) return false;

			// won't step into pit or wall
			HeightEnum height = ctx.Dest.Height;
			if ( height==HeightEnum.Pit || height==HeightEnum.Wall ) return false;

			// absolute height differential
			int nheight = (int)height;
			int fheight = (int) ctx.From.Height;
			int diff = (nheight>fheight ? nheight - fheight : fheight - nheight );

			// can't cross height differential
			if  ( climb<diff ) return false;

			return true;
		}

	}
}
