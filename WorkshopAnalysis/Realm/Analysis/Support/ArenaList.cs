//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using Realm;
using Realm.Enums;
using Realm.Puzzle;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Analysis {
	public class ArenaList {

		public ArenaList() {
			BuildSinglesMaps();
			BuildTeamMaps();
		}

		
		public List<PuzzleMap> Singles { get; internal set;}
		public List<PuzzleMap> Teams { get; internal set;}


		internal void BuildSinglesMaps() { 
			
			Singles = new List<PuzzleMap>();
			for (int size=5;size<15;size++) {
				Singles.Add( SingleLineTerrain(size) );
			}
			
		}

		internal void BuildTeamMaps() { 
				
			Teams = new List<PuzzleMap>();
			for (int size=6;size<16;size++) {
				Teams.Add( SimpleTerrain(size) );
			}
	
		}
		
		internal PuzzleMap SingleLineTerrain(int size) {

			var map = PuzzleMapFactory.SingleLineTerrain(size);
			map.Title = "Line-"+size ;

			map.Places[0,0].Flag = FlagEnum.Entry;

			map.Places[size-1,0].Flag = FlagEnum.Exit;

			return map;
		}

		internal PuzzleMap SimpleTerrain(int size) {

			var map = PuzzleMapFactory.SimpleTerrain(size,size);
			map.Title = "Square-"+size ;

			map.Places[0,0].Flag = FlagEnum.Entry;
			map.Places[0,1].Flag = FlagEnum.Entry;
			map.Places[1,0].Flag = FlagEnum.Entry;
			map.Places[1,1].Flag = FlagEnum.Entry;

			int s1 = size-1, s2 = size-2;

			map.Places[s1,s1].Flag = FlagEnum.Exit;
			map.Places[s1,s2].Flag = FlagEnum.Exit;
			map.Places[s2,s1].Flag = FlagEnum.Exit;
			map.Places[s2,s2].Flag = FlagEnum.Exit;

			return map;
		}
	}
}
