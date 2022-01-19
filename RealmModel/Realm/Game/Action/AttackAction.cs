//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using Realm.Puzzle;

using YamlDotNet.Serialization;

namespace Realm.Game.Action {

	public class AttackAction {

		public AttackAction(int agentId, int dmg ) {
			this.TargetId = agentId;
			this.Damage = dmg;
		}

		/// <summary>
		/// Provided for SaveFile storage
		/// </summary>
		public int TargetId { get; internal set; }

		public int Damage { get; internal set; }
	}

}
