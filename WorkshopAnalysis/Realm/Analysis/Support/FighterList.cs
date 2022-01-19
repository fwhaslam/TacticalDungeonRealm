

namespace Realm.Analysis {

	using System;
	using System.Linq;
	using Realm.Puzzle;

	using System.Collections.Generic;
	using System.Collections.ObjectModel;


	public class FighterList {

		public FighterList() {
			BuildSingleList();
			BuildTeamList();
		}

		public ReadOnlyCollection<AgentType> Singles { get; internal set; }

		public ReadOnlyCollection<ReadOnlyCollection<AgentType>> Teams { get; internal set; }

		internal void BuildSingleList() {

			var start = new List<AgentType>();
			for (int ix=0;ix<AgentType.Count();ix++) {
				start.Add( AgentType.Get(ix) );
			}

			// sort by 'Name'
			var work = start.OrderBy( t => t.Name );

			Singles = new List<AgentType>(work).AsReadOnly();
		}

		internal void BuildTeamList() {

		}
	}
}
