//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Analysis {

	using Realm.Brain;
	using Realm.Game;
	using Realm.Puzzle;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// First handler will run single combats to determine relative power of agents.
	/// This would be a grid of type-v-type over multiple arenas and minds.
	/// 
	/// Since we are focused on the value of agent-v-agent power:
	///		
	///		* fighters do not battle copies of themselves.
	///		
	///		* both sides use the same mind
	///		
	///		* since arenas may not be symetrical, they are used from both directions.
	///		  fighters are placed in both first and second position.
	/// 
	/// </summary>
	public class AgentPowerAnalyzer {

		AnalysisCentral central;

		/// <summary>
		///  The results of battles are stored into a grid.
		///  The grid is an array of fighter-v-fighter.
		///  Each metrics element accumulates results from one pairing over all minds and arenas.
		///  
		/// There are no results for fighter-v-self.  
		/// 
		/// Full metrics combine the symettric results (eg.  metrics[0,1] + metrics[1,0] = full results for zero-vs-one
		/// </summary>
		BattleResultMetrics[,] metrics;

		public AgentPowerAnalyzer() { 
		
			central = new AnalysisCentral();

			int fighterCount = central.Fighters.Singles.Count;

			metrics = new BattleResultMetrics[ fighterCount, fighterCount ];
		}

		public void Perform() {

			var fighters = central.Fighters.Singles;

			// build metrics
			for (int row = 0; row< metrics.GetLength(0); row++ ) {
				for (int col = 0; col< metrics.GetLength(1); col++ ) {
					if (row==col) continue;
					string info = fighters[row].Name +" v "+fighters[col].Name;

					var result = new BattleResultMetrics( info, row, col );
					RunBattles( result );
					metrics[row,col] = result;
//if (true) return;
				}
			}

			Summarize();
			
		}

		internal void RunBattles( BattleResultMetrics result ) {

						
			var arenas = central.Arenas.Singles;
			var minds = central.Minds.Minds;

			foreach ( var arena in arenas ) {

				foreach ( var mind in minds ) {

					float[] success = RunOneBattle( result, arena, mind, 10 );

					result.Record( success[0], success[1] );
				}
			}
		}

		/// <summary>
		/// Returns succcess metric for each side
		/// </summary>
		/// <param name="result"></param>
		/// <param name="arena"></param>
		/// <param name="mind"></param>
		/// <returns></returns>
		internal float[] RunOneBattle( BattleResultMetrics result, PuzzleMap arena, BrainIf mind, int roundLimit ) { 

			var driver = new BattleDriver( arena );

			AgentType[] firstList =  { central.Fighters.Singles[result.First] };
			driver.FillFaction( 0, mind, firstList );

			AgentType[] secondList =  { central.Fighters.Singles[result.Second] };
			driver.FillFaction( 1, mind, secondList );

			driver.Drive( roundLimit );
			return driver.Success;
		}


		public void Summarize() {

			Console.Out.WriteLine("\n\nSUMMARY :::::\n");


			for (int col=0; col< metrics.GetLength(1); col++ ) {

				for (int row =col+1; row < metrics.GetLength(0); row++ ) {

					var metric1 = metrics[col,row];
					var metric2 = metrics[row,col];

					float[] average = SumOfRecords( metric1.Results, metric2.Results );

					Console.Out.WriteLine("{0}   avg[0]={1:0.##}   avg[1]={2:0.##}  WinDrawLoss={3:0.#}/{4:0.#}/{5:0.#}\n",
						metric1.Info, average[0], average[1], average[2], average[3], average[4]);
					//Console.Out.WriteLine("WinDrawLoss={0:0.##}/{1:0.##}/{5.0.##}\n", 
					//	average[2], average[3], average[4] );
				}
			}
		}

		internal float[] SumOfRecords( List<BattleResult> forward, List<BattleResult> reverse ) {

			float num = forward.Count + reverse.Count;
			float first = 0f;
			float second = 0f;
			int wins = 0;
			int draw = 0;
			int loss = 0;

			foreach (var result in forward) {
				first += result.First;
				second += result.Second;
				if (result.First<1f && result.Second<1f) draw++;
				else if (result.First==1f) wins++;
				else loss++;
			}

			foreach (var result in reverse) {
				second += result.First;
				first += result.Second;
				if (result.First<1f && result.Second<1f) draw++;
				else if (result.Second==1f) wins++;
				else loss++;
			}

			float[] sum = {  first/num, second/num, wins/num, draw/num, loss/num };
			return sum;
		}
	}
}
