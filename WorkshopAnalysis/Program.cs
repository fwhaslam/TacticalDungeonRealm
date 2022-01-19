
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Realm.Analysis;

public class Program {
	static public void Main(string[] args) {

		Console.Out.WriteLine("Start of Workshop Analysis");
		new AgentPowerAnalyzer().Perform();
		Console.Out.WriteLine("End of Workshop Analysis");

	}
}

