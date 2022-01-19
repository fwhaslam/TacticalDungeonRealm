//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm {

	using Realm.Puzzle;

	using YamlDotNet.Serialization;

	/// <summary>
	/// Utility supports reading and writing of Save Files.
	/// </summary>
	public class RealmManager {

		static readonly DefaultValuesHandling VALUE_HANDLING =  DefaultValuesHandling.OmitNull |  DefaultValuesHandling.OmitDefaults;

		static readonly ISerializer SERIALIZER = new SerializerBuilder().ConfigureDefaultValuesHandling( VALUE_HANDLING ).Build();

		static readonly IDeserializer DESERIALIZER = new Deserializer();

		static public string DumpLevelMap( PuzzleMap map ) {
			return SERIALIZER.Serialize(map);
		}

		static public PuzzleMap ParseLevelMap( string content ) {
			return DESERIALIZER.Deserialize<PuzzleMap>(content);
		}

	}
}
