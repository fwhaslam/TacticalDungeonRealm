//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Views.Decider {

	using Realm.Game;
	using Realm.Puzzle;
	using Realm.Tools;
	using Realm.Views.Stepper;

	using System;
	using System.Collections.Generic;

	public interface StepDeciderIf {

		StepDecision Decide( StepContext ctx, int value );

		List<Where> GetStarts( PlayMap map );
	}

}
