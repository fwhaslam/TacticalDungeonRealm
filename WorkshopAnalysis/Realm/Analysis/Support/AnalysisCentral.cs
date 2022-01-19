using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Analysis {

	public class AnalysisCentral {

		public AnalysisCentral() {

			Arenas = new ArenaList();
			Fighters = new FighterList();
			Minds = new MindList();
		}

		public ArenaList Arenas { get; internal set; }


		public FighterList Fighters { get; internal set; }


		public MindList Minds { get; internal set; }

		
	}
}
