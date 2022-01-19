//
//	Copyright 2021 Frederick William Haslam born 1962
//

namespace Realm.Enums {

	using System;
	using System.Collections.Generic;

	public class FlagEnumTraits {


		static internal readonly Dictionary<char,FlagEnum> FromSymbolMap = new Dictionary<char, FlagEnum>() {

			{ '.', FlagEnum.None },

			{ 'E', FlagEnum.Entry },
			{ 'X', FlagEnum.Exit },

			{ 'D', FlagEnum.Door },
			{ 'H', FlagEnum.Hostage },
			{ 'C', FlagEnum.Chest },
			{ 'S', FlagEnum.Sack },

			{ 'L', FlagEnum.Lever },
			{ 'T', FlagEnum.Switch },
			{ 'G', FlagEnum.Gears },
			{ 'P', FlagEnum.Pitfall },
			{ 'M', FlagEnum.Masher },

			{ '-', FlagEnum.Lower },
			{ '+', FlagEnum.Raise },
			{ 'K', FlagEnum.Spikes },
			{ 'A', FlagEnum.Arrows },
			{ 'B', FlagEnum.Boulder },
		};

		static internal readonly Dictionary<FlagEnum,char> ToSymbolMap = new Dictionary<FlagEnum,char>();

		static FlagEnumTraits() {
			foreach (KeyValuePair<char, FlagEnum> entry in FromSymbolMap) {
				ToSymbolMap[ entry.Value ] = entry.Key;
			}
		}

		static public int Count() { return Enum.GetNames(typeof(FlagEnum)).Length; }

		static public char Symbol(FlagEnum flag) {
			return ToSymbolMap[flag];
		}

		static public FlagEnum FromSymbol( char symbol) {
			return FromSymbolMap[symbol];
		}
	}

}
