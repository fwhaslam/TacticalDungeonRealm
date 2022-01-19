//
//	Copyright 2021 Frederick William Haslam born 1962
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;

namespace Realm.Tools {

	public class YamlTools {

		static readonly DefaultValuesHandling VALUE_HANDLING =  DefaultValuesHandling.OmitNull |  DefaultValuesHandling.OmitDefaults;

		static readonly ISerializer SERIALIZER = new SerializerBuilder().ConfigureDefaultValuesHandling( VALUE_HANDLING ).Build();

		static public string ToYamlString( object what ) {

			return SERIALIZER.Serialize( what );
		}
	}
}
