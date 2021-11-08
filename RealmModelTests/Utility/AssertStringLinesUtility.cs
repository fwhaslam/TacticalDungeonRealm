//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Utility {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


	/// <summary>
	/// Compare strings based on internal line structure.  
	/// All end of line structures are discarded and are considered equivalent.
	/// </summary>
	public class AssertStringLinesUtility {

		static public void StringLinesAreEqual( string[] expect, string[] actual ) {
			string reason = CompareStringLines( expect, actual );
			if (reason!=null) Fail( reason );
		}

		static public void StringLinesAreEqual( ICollection<string> expect, ICollection<string> actual ) {
			string[] expectAry = expect.ToArray<string>();
			string[] actualAry = actual.ToArray<string>();
			StringLinesAreEqual( expectAry, actualAry );
		}

		static public void StringLinesAreEqual( string expect, string actual ) {

			string[] expectAry = expect.Replace("\r","").Split( '\n' );
			string[] actualAry = actual.Replace("\r","").Split( '\n' );
			StringLinesAreEqual( expectAry, actualAry );
		}

//======================================================================================================================

		static internal string CompareStringLines( string[] expect, string[] actual ) {

			DumpToConsole(actual);

			int min = Math.Min(expect.Length,actual.Length);
			int max = Math.Max(expect.Length,actual.Length);

			for (int ix=0;ix<min;ix++) {
				var first = expect[ix];
				var second = actual[ix];
				if (!first.Equals( second ) )
						return "Strings do not match at line ["+ix+"]\n[["+first+"]]\n[["+second+"]]";
			}

			if (min<max) 
				return "Strings do not match at line["+min+"]\nOne space is larger than the other";

			return null;
		}

		static internal void DumpToConsole(string[] actual) {

			var buf = new StringBuilder();

			buf.Append( "[[\"" );
			buf.Append( String.Join( "\\n\"+\n\t\t\"", actual ) );
			buf.Append( "\"]]" );

			Console.Out.WriteLine( buf.ToString() );
		}

	}
}
