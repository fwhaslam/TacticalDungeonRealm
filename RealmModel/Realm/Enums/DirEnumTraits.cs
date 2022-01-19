//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class DirEnumTrait {

		public DirEnumTrait( DirEnum dir, int dx,int dy,bool orth,int stp ) {
			this.DirIx = (int)dir;
			this.DX = dx;
			this.DY = dy;
			this.Orth = orth;
			this.Steps = stp;
		}
		public int DirIx { get; internal set; }
		public int DX {  get; internal set; }
		public int DY {  get; internal set; }

		public bool Orth { get; internal set; }
		public int Steps {  get; internal set; }

		public string ToDisplay() {
			return "DirEnumTrait(DX="+DX+",DY="+DY+",Steps="+Steps+")";
		}

	}

	public class DirEnumTraits {

		static readonly DirEnumTrait[] traits = {
			new DirEnumTrait( DirEnum.North,		0, 1, true, 2 ),	// north
			new DirEnumTrait( DirEnum.NorthEast,	1, 1, false,  3 ),
			new DirEnumTrait( DirEnum.East,			1, 0, true, 2 ),
			new DirEnumTrait( DirEnum.SouthEast,	1, -1, false, 3 ),

			new DirEnumTrait( DirEnum.South,		0, -1, true, 2 ),	// south
			new DirEnumTrait( DirEnum.SouthWest,	-1, -1, false, 3 ),
			new DirEnumTrait( DirEnum.West,			-1, 0, true, 2 ),
			new DirEnumTrait( DirEnum.NorthWest,	-1, 1, false, 3 )

		};

		public DirEnumTrait this[int index] {
			get { return traits[ index ]; }
		}

		// set once, becomes constant
		static public readonly int Count =  Enum.GetNames(typeof(DirEnum)).Length; 

		// set once, becomes constant?
		static public readonly IEnumerable<int> Range = Enumerable.Range(0,Count);

		static public int DirIx(DirEnum dir) { return traits[(int)dir].DirIx; }

		static public int DX(int dir) { return traits[dir].DX; }

		static public int DY(int dir) { return traits[dir].DY; }

		static public bool Orth(int dir) { return traits[dir].Orth; }

		static public int Steps(int dir) { 
			return traits[dir].Steps; 
		}

		static public DirEnumTrait Trait(int dir) { return traits[dir]; }

		static public DirEnumTrait[]  All {  
			get {return traits; }
		}

		/// <summary>
		/// given a starting face and ending face, do we turn up to four right, or up to three left?
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns>Negative for left turns, positive for right turns.</returns>
		static public int Turns( int start, int end ) {
			var turns = ( end + Count - start ) % Count;
			return ( turns>4 ? turns-8 : turns );
		}
	}

}
