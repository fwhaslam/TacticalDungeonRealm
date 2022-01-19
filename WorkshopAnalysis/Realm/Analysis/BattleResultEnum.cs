using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Analysis {

	/// <summary>
	/// Standardized results for battles.
	/// Note that battles are round limited ( probably to 10 ).
	/// </summary>
	public enum BattleResultEnum {
		FirstWins,		// first faction/mind wins
		SecondWins,		// second faction/mind wins
		FirstLeads,		// first faction/mind is ahead on kills/wounds
		SecondLeads,	// second faction/mind is ahead on kills/wounds
		Tie,			// both factions have created the same number of kills/wounds
		Draw			// both factions are tied AND the game is not changing
	}
}
