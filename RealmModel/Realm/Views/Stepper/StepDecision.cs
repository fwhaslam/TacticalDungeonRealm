//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views.Stepper {

	/// <summary>
	/// Response from StepDecision functions.
	/// * will we enter ?
	/// * what is the cost ?
	/// </summary>
	public class StepDecision {
		public bool Enter {  get; set; }
		public int Update {  get; set ; }
	}
}
