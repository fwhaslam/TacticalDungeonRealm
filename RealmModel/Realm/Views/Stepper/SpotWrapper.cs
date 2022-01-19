//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views.Stepper {

	using System;
	using Realm.Enums;
	using Realm.Puzzle;


	/// <summary>
	/// One context for each Direction.
	/// Used for rapid stepping during flood fill.
	/// </summary>
	public class SpotWrapper : CloneableIf<SpotWrapper> {

		public SpotWrapper(Place here, int val){
			this.Here = here;
			this.Value = val;
		}

		public SpotWrapper( SpotWrapper src ) {
			this.Here = src.Here;
			this.Value = src.Value;
			for (int ix=0;ix<DirEnumTraits.Count;ix++) this.Array[ix] = src.Array[ix];
		}
		
		public SpotWrapper Clone() {
			return new SpotWrapper(this);
		}

		public Place Here {  get; set; }
		
		public int Value { get; set; }

		StepContext[] Array = new StepContext[ DirEnumTraits.Count ];

		public StepContext this[int index] {
			get {  return Array[index]; }
			set {  Array[index] = value; }
		}

	}
}
