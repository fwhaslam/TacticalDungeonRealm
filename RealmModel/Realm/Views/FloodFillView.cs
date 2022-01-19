//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views {

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Views.Stepper;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Realm.Game;

	/// <summary>
	/// Base for flood fill views.
	/// </summary>
	public class FloodFillView {

		public FloodFillView(PlayMap map) {
			View = GridStepper.Build( map.Places, -1 );
		}

		public FloodFillView(Grid<Place> grid) {
			View = GridStepper.Build( grid, -1 );
		}

		public GridStepper View { get; set; }

		/// <summary>
		/// Can set to some default value for analysis.
		/// </summary>
		/// <param name="startVal"></param>
		internal void ResetValues( int startVal ) {
			for (int ix=0;ix<View.Length;ix++) View[ix].Value = startVal;
		}

		/// <summary>
		/// String format 'display' for testing.
		/// NOTE: north is at top ( first row )
		/// </summary>
		/// <returns></returns>
		public string ToDisplay() {

			var map = View;
			int max = map.Length;
			string form = (max<10 ? "{0,2}" : (max<100 ? "{0,3}" : ( max<1000 ? "{0,4}" : "{0,5}" )));

			var buf = new StringBuilder("FloodFillView[");

			for (int row=map.Tall-1;row>=0;row--) {
				buf.Append("\n    ");
				for (int col=0;col<map.Wide;col++) {
					buf.Append( String.Format( form, map[col,row].Value ) );
				}
			}
			
			buf.Append( "\n]");
			return buf.ToString();
		}
		
		/// <summary>
		/// Algorithm for stepping through grid and updating values.
		/// </summary>
		/// <param name="startPts"></param>
		/// <param name="decide"></param>
		public void DoStepFill( List<Where> startPts, Func<StepContext, int, StepDecision> decide  ) {

			ResetValues( -1 );

			//var start = map[who.Where.X,who.Where.Y];
			//int stepLimit = who.Type.Steps;

			var scan = new Queue<SpotWrapper>();

			foreach ( var start in startPts ) { 
//Console.Out.WriteLine("START POINT="+start);

				var startIx = View.Index(start);
				var spot = View[startIx];
				spot.Value = 0;			// no steps to get to starting point
				scan.Enqueue( spot );
			}

			while (scan.Count>0) {

				var spot = scan.Dequeue();
				int steps = spot.Value;

				foreach ( var dirIx in Enumerable.Range(0,8) ) { 

					var nextCtx = spot[dirIx];
					if (nextCtx==null) continue;	// edge of map;

					var decision = decide.Invoke( nextCtx, steps );
					if (!decision.Enter) continue;

					int nsteps = decision.Update;
					var nextSpot = nextCtx.There;
					int nvalue = nextSpot.Value;
					if ( nvalue>=0 && nsteps>=nvalue) continue;	// took longer to arrive, do not add to queue

					nextSpot.Value = nsteps;
					scan.Enqueue( nextSpot );
//Console.Out.WriteLine("PROGRESS="+ToDisplay());
				}

			}
		}

	}
}
