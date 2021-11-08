using Realm.Enums;
using Realm.Puzzle;
using Realm.Tools;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Views {
	
	/// <summary>
	/// Flood fill view for Agent stepping through map.
	/// </summary>
	public class AgentMoveView : FloodFillView {

		public void DoStepFill( PuzzleMap map, Agent who  ) {
		
				var stepper = GridStepper<Place>.Build( map.Places );

			DoStepFill( stepper, who );
		}

		
		/// <summary>
		/// 
		/// TODO: 
		/// * start points
		/// * flood function
		/// * ? stepper or map
		/// 
		/// </summary>
		/// <param name="map">puzzle</param>
		/// <param name="start">flood from this point</param>
		/// <param name="steps">limit on steps</param>
		public void DoStepFill( GridStepper<Place> stepper, Agent who  ) {
			
			var map = stepper.Map;
			Reset( map.Wide, map.Tall );

			var start = map[who.Where.X,who.Where.Y];
			int stepLimit = who.Type.Steps;

			Queue<Place> scan = new Queue<Place>();

			Values[start.Where.X,start.Where.Y] = 0;	// no steps to get to starting point
			scan.Enqueue( start );

			while (scan.Count>0) {

				var from = scan.Dequeue();
				var index = stepper.Map.Index(from.Where);
				int steps = Values[index];

				foreach ( var dir in Enumerable.Range(0,8) ) { 

					var nextCtx = stepper.Adj[index][dir];
					if (nextCtx==null) continue;	// edge of map;

					if (!CanStep( who, nextCtx )) continue;

					int nsteps = steps + DirEnumTraits.Steps((DirEnum)dir);
					if ( nsteps > stepLimit ) continue;

					var nplace = nextCtx.Dest;
					var nindex = stepper.Map.Index(nplace.Where);
					int nvalue = Values[nindex];
					if ( nvalue>=0 && nsteps >= nvalue) continue;

					Values[nindex] = nsteps;
					scan.Enqueue( nplace );
				}

			}
		}


		/// <summary>
		/// Can an agent in the this place step into an adjacent place?
		/// </summary>
		/// <param name="who"></param>
		/// <param name="dir"></param>
		/// <returns>negative for 'no', or number of steps for 'yes'</returns>
		internal bool CanStep( Agent who, StepContext<Place> ctx ) {

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
