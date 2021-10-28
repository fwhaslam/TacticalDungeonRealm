using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Utility {

	using System.Text;
using System.Threading.Tasks;

	public class Properties : Dictionary<string,string> {

		/// <summary>
		/// Create empty properties.
		/// </summary>
		public Properties() { }

		/// <summary>
		/// Load lines from file path, then parse.
		/// </summary>
		/// <param name="path"></param>
		public Properties( string path ) {

			var content = File.ReadAllText( path );

			Parse( content );
		}

		/// <summary>
		/// Read lines as if from a properties file.
		/// </summary>
		/// <param name="content"></param>
		public void Parse( string content ) {

			var lines = content.Replace("\r","").Split('\n');

			foreach ( var line in lines ) {
				if (line.StartsWith("!") || line.StartsWith("#")) continue;
				var cut = line.IndexOf("=");
				if (cut<0) continue;
				var key = line.Substring(0,cut).Trim();
				var value = line.Substring(1+cut).Trim();
				this[key] = value;
			}
		}
	}
}
