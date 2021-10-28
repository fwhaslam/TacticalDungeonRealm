//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility {

	public class SimpleTools {

		static readonly string BINARY_FOLDER = "bin";

		static public string FixFilePath( string path ) {
			return path.Replace( '/', Path.DirectorySeparatorChar );
		}

		/// <summary>
		/// The directory containing the compiled code.
		/// </summary>
		/// <returns></returns>
		static public string WorkingDir() {
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}

		/// <summary>
		/// The project is one folder above the 'bin 'folder;
		/// </summary>
		/// <returns></returns>
		static public string ProjectDir() {
			DirectoryInfo info = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			while (!info.Name.Equals(BINARY_FOLDER)) info = info.Parent;
			return info.Parent.FullName;
		}

		/// <summary>
		/// The solution is two folders above the 'bin' folder.
		/// </summary>
		/// <returns></returns>
		static public string SolutionDir() {
			DirectoryInfo info = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			while (!info.Name.Equals(BINARY_FOLDER)) info = info.Parent;
			return info.Parent.Parent.FullName;
		}

	}
}
