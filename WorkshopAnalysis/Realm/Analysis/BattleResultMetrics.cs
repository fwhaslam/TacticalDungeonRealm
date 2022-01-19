using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Analysis {

	/// <summary>
	/// Sum of 'success' metric for all battles.
	/// Success is percentage of health destroyed by wounds.
	/// </summary>
	public class BattleResult {
		public BattleResult( float first,float second) {
			this.First = first;
			this.Second = second;
		}
		public float First { get; }
		public float Second { get; }
	}

	public class BattleResultMetrics {

		public BattleResultMetrics( string info, int firstAgentId, int secondAgentId ) {
			this.Info = info;
			this.First = firstAgentId;
			this.Second = secondAgentId;
			this.Results = new List<BattleResult>();
		}

		public string Info { get; }
	
		public int First { get; }

		public int Second { get; }

		public List<BattleResult> Results {  get; }

		public void Record( float firstSuccess, float secondSuccess) {
			Results.Add( new BattleResult( firstSuccess, secondSuccess ) );
		}
	}
}
