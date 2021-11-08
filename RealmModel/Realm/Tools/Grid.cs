//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Tools {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// 2D Array of generic values.  
	/// Wide + Tall define the location function = y * wide + x
	/// can be used to iterate over all elements.
	/// </summary>
	public class Grid<T> : IEnumerable<T> {

		public Grid( int wide, int tall ) {
			this.Wide = wide;
			this.Tall = tall;
			this.Array = new T[ wide * tall ];
		}

		public int Wide {  get; internal set; }
		public int Tall {  get; internal set; }

		public T[] Array {  get; internal set; }

		public int Length {
			get {
				return Array.Length;
			}
		}

		public T this[int index] {
			get { return Array[ index ]; }
			set { Array[ index ] = value; }
		}

		public T this[int x, int y] {
			get { return Array[ y * Wide + x ]; }
			set { Array[ y * Wide + x ] = value; }
		}

		public T this[Where where] {
			get { return Array[where.Y * Wide + where.X]; }
			set { Array[where.Y * Wide + where.X] = value; }
		}

		public int Index(int x,int y) {
			return y * Wide + x;
		}

		public int Index(Where where) {
			return where.Y * Wide + where.X;
		}

		public bool Contains(Where where) {
			if (where.Y < 0 || where.X < 0) return false;
			if (where.Y >= Tall || where.X >= Wide) return false;
			return true;
		}

		public IEnumerator<T> GetEnumerator() {
			return ((IEnumerable<T>)Array).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return Array.GetEnumerator();
		}
	}
}
