//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm {

	using Realm.Puzzle;

	using YamlDotNet.Serialization;

	public class RealmManager {

		static public string DumpLevelMap( PuzzleMap map ) {
			return new Serializer().Serialize(map);
		}

		static public PuzzleMap ParseLevelMap( string content ) {
			return new Deserializer().Deserialize<PuzzleMap>(content);
		}

	}
}
