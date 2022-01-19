using Realm.Brain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Analysis {

	public class MindList {

		public MindList() {

			List<BrainIf> list = new List<BrainIf>();
			list.Add( new ChaseBrain() );
			list.Add( new CautiousBrain() );
			Minds = list;
		}

		public List<BrainIf> Minds { get; internal set; }
	}
}
