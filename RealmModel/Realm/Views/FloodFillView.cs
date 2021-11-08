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

	/// <summary>
	/// Base for flood fill views.
	/// </summary>
	public class FloodFillView {

		public Grid<int> Values { get; internal set; }

		internal void Reset( int wide, int tall ) {
			Values = new Grid<int>(wide,tall);
			for (int ix=0;ix<Values.Length;ix++) Values[ix] = -1;
		}

		/// <summary>
		/// String format 'display' for testing.
		/// </summary>
		/// <returns></returns>
		public string ToDisplay() {

			int max = Values.Array.Max();
			string form = (max<10 ? "{0,2}" : (max<100 ? "{0,3}" : ( max<1000 ? "{0,4}" : "{0,5}" )));

			var buf = new StringBuilder("FloodFillView[");

			for (int dt=0;dt<Values.Tall;dt++) {
				buf.Append("\n    ");
				for (int dw=0;dw<Values.Wide;dw++) {
					buf.Append( String.Format( form, Values[dw,dt] ) );
				}
			}
			
			buf.Append( "\n]");
			return buf.ToString();
		}

	}
}
