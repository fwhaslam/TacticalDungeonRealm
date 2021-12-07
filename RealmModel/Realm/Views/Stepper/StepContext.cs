//
//	Copyright 2021 Frederick William Haslam born 1962
//

using Realm.Puzzle;

namespace Realm.Views.Stepper {
	/// <summary>
	/// Information necessary to make step decisions.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StepContext {

		/// <summary>
		/// All the information necessary for functions to decide on stepping.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="Dest"></param>
		/// <param name="dir"></param>
		public StepContext( Place from, Place dest, int dirIx, SpotWrapper there ) {
			this.From = from;
			this.Dest = dest;
			this.DirIx = dirIx;
			this.There = there;
		}

		public Place From { get; set; }

		public Place Dest { get; set; }

		public int DirIx { get; set; }

		public SpotWrapper There {  get; set; }

	}
}
