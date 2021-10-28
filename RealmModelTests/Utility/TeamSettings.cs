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

	/// <summary>
	/// This mechanism is built to mimic the java Properties file, which is dead simple.
	/// </summary>
	public class TeamSettings : Properties {

		static readonly string TeamSettingsPath = "/TeamSettings/{0}.properties";

		public TeamSettings() : base( GetSettingsPath() ) {}

		public string GetPuzzlesFolder() { return this["puzzles.folder"]; }

		static internal string GetSettingsPath() {
			return String.Format( SimpleTools.SolutionDir()+TeamSettingsPath, Environment.UserName );
		}
	}
}
