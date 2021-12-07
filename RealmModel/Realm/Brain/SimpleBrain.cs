

using Realm.Views;

namespace Realm.Brain {

	using Realm.Enums;
	using Realm.Game;
	using Realm.Puzzle;
	using Realm.Views;
	using Realm.Views.Stepper;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Action = Realm.Game.Action;
	using static Realm.Enums.DirEnum;

	/// <summary>
	/// Move directly towards enemies and strike when possible.
	/// Weakest act first.
	/// </summary>
	public class SimpleBrain : BrainIf {

		static readonly int DIR_COUNT = DirEnumTraits.Count;

		static readonly DirEnum[] LOOK_ORDER = { North, NorthEast, NorthWest, East, West, SouthEast, SouthWest, South };

		/// <summary>
		/// Simple logic.  Move towards closest foe, hit them.
		/// </summary>
		/// <param name="map"></param>
		/// <param name="actor"></param>
		/// <returns></returns>
		public ActionChain ChooseAction( PuzzleMap map, Agent actor ) {

			var pathView = new MoveManager().GetAgentPaths( map, actor );

			var chain = ActionFactory.EmptyChain(actor);

			if ( actor.IsActive() ) BuildActionChain( chain, map, pathView, actor );

			return chain;
		}

		/// <summary>
		/// Using an agent path view, choose movement which takes us closer to foes.
		/// </summary>
		/// <param name="chain"></param>
		/// <param name="map"></param>
		/// <param name="pathView"></param>
		/// <param name="who"></param>
		internal void BuildActionChain( ActionChain chain, PuzzleMap map, FloodFillView pathView, Agent who ) {

			var start = who.Where;
			var spot = pathView.View[start];
			var lowVal = spot.Value;
			var face = (int)who.Face;
			var target = (Agent)null;

			// Select steps :: stop for foe
			int steps = 0;
			while ( steps < who.Type.Steps ) {

//Console.Out.WriteLine("Loop Start val="+lowVal );
				var lowDir = -1;
				var lowSpot = spot;
				foreach ( var dir in LOOK_ORDER ) { 

					// rotate around agent starting with forward, right, left
					var dirIx = (face + (int)dir) % DIR_COUNT;
//Console.Out.WriteLine("DIRIX="+dirIx);

					var nextCtx = spot[dirIx];
					if (nextCtx==null) continue;	// cannot go off-grid

					var nextSpot = nextCtx.There;
					if (nextSpot.Value<0) continue;	// cannot enter negative

					var nextAgent = nextSpot.Here.Agent;
					if (nextAgent!=null) {
						if (who.IsFoe(nextAgent)) {	// stepped next to enemy, stop moving
							target = nextAgent;
							break;
						}
					}

					var nextVal = nextSpot.Value;
					if (nextVal<lowVal) {
//Console.Out.WriteLine("next="+nextVal+"  DIR="+dirIx+"  Where="+nextSpot.Here.Where );
						lowSpot = nextSpot;
						lowDir = dirIx;
						lowVal = nextVal;
					}
				}

				// found enemy, attack
				if (target!=null) {
					int damage = ActionLogic.DamageTo( map, who, target );
					ActionFactory.AddAttack( chain, target.Ident, damage );
					break;
				}

				if (lowSpot==spot) break;	// no progress

				// apply the step
				ActionFactory.AddTurns( chain, face, lowDir );
				face = lowDir;

				steps += DirEnumTraits.Steps(lowDir);
				ActionFactory.AddForward( chain );

				spot = lowSpot;
			}
		}
	}
}
