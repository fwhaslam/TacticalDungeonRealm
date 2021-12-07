
namespace Realm.Views.Decider {

	using Realm.Puzzle;
	using Realm.Tools;
	using Realm.Views.Stepper;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public interface StepDeciderIf {

		StepDecision Decide( StepContext ctx, int value );

		List<Where> GetStarts(PuzzleMap map);
	}

}
