//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class DirEnumTrait {

		public DirEnumTrait( int dx,int dy,bool orth,int stp ) {
			this.DX = dx;
			this.DY = dy;
			this.Orth = orth;
			this.Steps = stp;
		}
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
			new DirEnumTrait( 0, 1, true, 2 ),	// north
			new DirEnumTrait( 1, 1, false,  3 ),
			new DirEnumTrait( 1, 0, true, 2 ),
			new DirEnumTrait( 1, -1, false, 3 ),

			new DirEnumTrait( 0, -1, true, 2 ),	// south
			new DirEnumTrait( -1, -1, false, 3 ),
			new DirEnumTrait( -1, 0, true, 2 ),
			new DirEnumTrait( -1, 1, false, 3 )

		};

		static public readonly int Count =  Enum.GetNames(typeof(DirEnum)).Length; 

		static public readonly IEnumerable<int> Range = Enumerable.Range(0,Count);

		static public int DX(DirEnum dir) { return traits[(int)dir].DX; }

		static public int DY(DirEnum dir) { return traits[(int)dir].DY; }

		static public bool Orth(DirEnum dir) { return traits[(int)dir].Orth; }

		static public int Steps(DirEnum dir) { return traits[(int)dir].Steps; }

		static public DirEnumTrait Trait(DirEnum dir) { return traits[(int)dir]; }

		static public DirEnumTrait[]  All {  
			get {return traits; }
		}

	}

}
