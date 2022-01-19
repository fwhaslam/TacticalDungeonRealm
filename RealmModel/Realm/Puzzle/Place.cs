//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Puzzle {

	using Realm.Enums;
	using Realm.Tools;

	using System;
	using System.Collections.Generic;

	/// <summary>
	/// One square in a LevelMap.
	/// Contains all details for that one location.
	/// I would call this a 'tile', but that could conflict with the game namespace.
	/// </summary>
	public class Place : CloneableIf<Place> {
		
		static readonly internal int NO_AGENT_ID = -1;

		public Place(int x, int y) {
			Where = new Where(x, y);
		}

		public Place( Place src ) {
			this.Where = new Where( src.Where );
			this.Height = src.Height;
			this.Flag = src.Flag;
			this.AgentId = src.AgentId;
		}

		public Place Clone() {
			return new Place( this );
		}

		public Where Where { get; set; }

		public int X() { return Where.X; }

		public int Y() { return Where.Y; }

		public HeightEnum Height { get; set; } = HeightEnum.One;

		public FlagEnum Flag { get; set; } = FlagEnum.None;

		public int AgentId { get; set; } = NO_AGENT_ID;

		public bool IsOccupied {  get {  return AgentId>NO_AGENT_ID; } }

		public void ClearAgent() {
			this.AgentId = NO_AGENT_ID;
		}

	}
}
