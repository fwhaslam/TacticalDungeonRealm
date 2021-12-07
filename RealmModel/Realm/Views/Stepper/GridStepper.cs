//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views.Stepper {

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Uses pointers to record links between locations, speeding up flood fill type processing.
	/// </summary>
	public class GridStepper : Grid<SpotWrapper> {

		internal GridStepper( int wide, int tall ) : base(wide,tall) { }

		/// <summary>
		/// Given the current location as an index, what is the next location as an index?
		/// </summary>
		/// <param name="index"></param>
		/// <param name="dir"></param>
		/// <returns></returns>
		public int Next( int index, DirEnumTrait dir ) {

			// Grid.Contains?
			int nw = (index%Wide) + dir.DX;
			if (nw<0 || nw>=Wide) return -1;

			int nt = (index/Wide) + dir.DY;
			if (nt<0 || nt>=Tall) return -1;

			return Index( nw, nt );
		}
		public int Next( int index, int dirIx ) {
			return Next( index, DirEnumTraits.All[dirIx] );
		}
	
		static public GridStepper Build( Grid<Place> src, int startVal ) {

			var make = new GridStepper( src.Wide, src.Tall );

			// locations
			for (int ix=0;ix<src.Length;ix++) {
				make[ix] = new SpotWrapper( src[ix], startVal );
			}

			// connect adjacencies
			var dirs = DirEnumTraits.All;

			for (int dw=0;dw<make.Wide;dw++) {
				for (int dt=0;dt<make.Tall;dt++) {
					 
					int index = make.Index(dw,dt);
					var linker = make[index];

					for ( int dx=0;dx<dirs.Length;dx++) {

						// Grid.Contains?
						var nindex = make.Next( index, dirs[dx] );
						if (nindex<0) continue;

						var next = make[nindex];
						linker[dx] = new StepContext( linker.Here, next.Here, dx, next );
					}
				}
			}

			return make;
		}

	}
}
