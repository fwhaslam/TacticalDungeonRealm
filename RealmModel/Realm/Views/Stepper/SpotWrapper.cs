//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views.Stepper {

	using Realm.Enums;
	using Realm.Puzzle;

	/// <summary>
	/// One context for each Direction.
	/// </summary>
	public class SpotWrapper {

		public SpotWrapper(Place here, int val){
			this.Here = here;
			this.Value = val;
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
