//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	/// <summary>
	/// Game state:  At start, in play, completed.  Goal status?
	/// </summary>
	public enum GameStateEnum {

		Start,		// no actions yet
		Active,		// agents have fulfillable goals -- eg. enemy to kill
		Static,		// agents have no fulfillable goals
		Done		// agents have no actions
	}
}
