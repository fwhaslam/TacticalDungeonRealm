using Realm.Puzzle;

using Realm.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realm.Tools;
using System.Diagnostics;

namespace Realm.Game {
	

	/// <summary>
	/// An action creates a change, which results in a new puzzle state.
	/// </summary>
	public class Action {

		public Action ( ActionTypeEnum _type ) {
			this.Type = _type;
		}

		public ActionTypeEnum Type { get; internal set; }

		/// <summary>
		/// Used with 'Attack' actions.  AgentId of the target.
		/// </summary>
		public int? Target { get; set; }

		/// <summary>
		/// Used with 'Attack' actions.  Damage inflicted ( after armor et al. )
		/// </summary>
		public int? Damage { get; set; }

	}

}
