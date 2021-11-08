//
//	Copyright 2021 Frederick William Haslam born 1962
//


namespace Realm.Views {

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;


	public class StepContext<T> {

		/// <summary>
		/// All the information necessary for functions to decide on stepping.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="Dest"></param>
		/// <param name="dir"></param>
		public StepContext( T from, T Dest, DirEnum dir ) {
			this.From = from;
			this.Dest = Dest;
			this.Dir = dir;
		}

		public T From { get; set; }

		public T Dest { get; set; }

		public DirEnum Dir { get; set; }

	}

	/// <summary>
	/// One context for each Direction.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StepContextGuide<T> {

		StepContext<T>[] Array = new StepContext<T>[ DirEnumTraits.Count ];

		public StepContext<T> this[int index] {
			get {  return Array[index]; }
			set {  Array[index] = value; }
		}

	}

	/// <summary>
	/// Uses pointers to record links between locations, speeding up flood fill type processing.
	/// </summary>
	public class GridStepper<T> {

		internal GridStepper(Grid<T> map) {
			this.Map = map;
			this.Adj = new Grid<StepContextGuide<T>>( map.Wide, map.Tall );
		}

		public Grid<T> Map { get; internal set; }
		
		//public T this[int index] { get { return Map[index]; } }

		//public T this[int dw, int dt] { get { return Map[dw,dt]; } }

		//public T this[Where where] { get { return Map[where]; } }

		public Grid<StepContextGuide<T>> Adj { get; internal set; }

		/// <summary>
		/// Given the current location as an index, what is the next location as an index?
		/// </summary>
		/// <param name="index"></param>
		/// <param name="dir"></param>
		/// <returns></returns>
		public int Next( int index, DirEnumTrait dir ) {

			// Grid.Contains?
			int nw = (index%Map.Wide) + dir.DX;
			if (nw<0 || nw>=Map.Wide) return -1;

			int nt = (index/Map.Wide) + dir.DY;
			if (nt<0 || nt>=Map.Tall) return -1;

			return Map.Index( nw, nt );
		}

		static public GridStepper<T> Build( Grid<T> map ) {

			var stepper = new GridStepper<T>( map );

			var dirs = DirEnumTraits.All;

			for (int dw=0;dw<map.Wide;dw++) {
				for (int dt=0;dt<map.Tall;dt++) {
					 
					int index = stepper.Map.Index(dw,dt);
					var place = stepper.Map[index];
					var linker = stepper.FixAdj( index );

					for ( int dx=0;dx<dirs.Length;dx++) {

						var dir = dirs[dx];

						// Grid.Contains?
						//int nw = dw + dir.DX;
						//if (nw<0 || nw>=map.Wide) continue;
						//int nt = dt + dir.DY;
						//if (nt<0 || nt>=map.Tall) continue;
						//var nindex = stepper.Map.Index(nw,nt);
						var nindex = stepper.Next( index, dir );
						if (nindex<0) continue;

						var next = stepper.Map[nindex];
						linker[dx] = new StepContext<T>( place, next, (DirEnum)dx );
					}
				}
			}

			return stepper;
		}

		///// <summary>
		///// Find or create a place link wrapper.
		///// </summary>
		///// <param name="place"></param>
		///// <returns></returns>
		//internal StepContextArray<T> FixAdj( int w, int t ) {
		//	if (Adj[w,t]==null) {
		//		Adj[w,t] = new StepContextArray<T>();
		//	}
		//	return Adj[w,t];
		//}

		/// <summary>
		/// Find or create a place link wrapper.
		/// </summary>
		/// <param name="place"></param>
		/// <returns></returns>
		internal StepContextGuide<T> FixAdj( int index ) {
			if (Adj[index]==null) {
				Adj[index] = new StepContextGuide<T>();
			}
			return Adj[index];
		}

	}
}
