//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Game {

	using System.Linq;
	using System.Collections.Generic;

	using Realm.Enums;
	using Realm.Puzzle;
	using Realm.Tools;
	using System;
	using YamlDotNet.Serialization;


	/// <summary>
	/// A subset of PuzzleMap used by the PuzzleGame to advance play.
	/// </summary>
	public class PlayMap : CloneableIf<PlayMap> {

		/// <summary>
		/// Provided to support YAML.
		/// </summary
		public PlayMap() { }

		/// <summary>
		/// Copy of another PlayMap.
		/// </summary>
		/// <param name="src"></param>
		public PlayMap( PlayMap src ) {

			this.Agents =  src.Agents.Select( a => (Agent)a.Clone() ).ToList<Agent>();
			this.Places =  src.Places.Clone();
			
			this.Wide = src.Wide;
			this.Tall = src.Tall;
		}

		public PlayMap Clone() {
			return new PlayMap(this);
		}

//======================================================================================================================

		// Wide + Talle contain important values during deserialization
		public int Wide { get; set; }

		// Wide + Talle contain important values during deserialization
		public int Tall { get; set; }


		public List<Agent> Agents { get; internal set; }

		/// <summary>
		/// Places become 'Map' for Yaml storage.
		/// </summary>
		[YamlIgnore]
		public Grid<Place> Places { get; internal set; }

		public List<string> Map { 
			get {
				return PuzzleMapFactory.MapAsStrings( this ); 
			} 
			set { 
				PuzzleMapFactory.StringsAsMap( this, value); 
			}
		}

//======================================================================================================================

		public void DoStep( Agent who, DirEnum dir ) {

			RemoveAgent( who.Where );

			var trait = DirEnumTraits.All[(int)dir];
			who.Where.X += trait.DX;
			who.Where.Y += trait.DY;
			who.Face = dir;
//Console.Out.WriteLine("Move "+who.Name+" to "+who.Where.ToDisplay() );

			Places[who.Where].AgentId = who.Ident;
		}

		public void DoInjure( int targetId, int damage ) {
			var who = Agents[targetId];
			who.Wounds += damage;
//Console.Out.WriteLine("Injure "+who.Name+" to "+who.Wounds+"/"+who.Type.Health );
			if (who.Wounds>=who.Type.Health) DoStatus( who, StatusEnum.Dead );
		}

		public void DoStatus( Agent who, StatusEnum status ) {
//Console.Out.WriteLine("Status "+who.Name+" to "+status );
			who.Status = status;
			if (status==StatusEnum.Dead) RemoveAgent( who.Where );
		}

		internal void RemoveAgent( Where loc ) {
//Console.Out.WriteLine("Remove agent at "+loc.ToDisplay() );
			Places[loc].ClearAgent();
		}

	}
}