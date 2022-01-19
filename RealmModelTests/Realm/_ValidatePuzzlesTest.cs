//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm {
	
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using System;
	using System.IO;
	using Utility;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Utility.SimpleTools;

	/// <summary>
	/// Load al puzzles from Assets/Puzzles to enesure they all parse.
	/// </summary>
	[TestClass]
	public class _ValidatePuzzlesTest {

		[TestMethod]
		public void ParseAllPuzzles() {

			var puzzlePath = new TeamSettings().GetPuzzlesFolder();
			DirectoryInfo puzzleDir = new DirectoryInfo( puzzlePath );
Console.Out.WriteLine(" FILE PATH = "+puzzleDir.FullName );

			IsTrue( puzzleDir.Exists );
			FileInfo[] files = puzzleDir.GetFiles();

			int count = 0;
			foreach ( var file in files ) {
				if (!file.Name.EndsWith("_dctpzl.yml")) continue;
				var content = File.ReadAllText( file.FullName );
				Console.Out.WriteLine("Parsing File Named ["+file.Name+"]");
				RealmManager.ParseLevelMap( content );
				Console.Out.WriteLine("Parsing Complete!");
				count++;
			}

			IsTrue( count > 0 );
		}

	}
}
