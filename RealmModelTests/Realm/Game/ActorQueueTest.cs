//
//	Copyright 2021 Frederick William Haslam born 1962 in the USA
//

namespace Realm.Game {

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Realm.Puzzle;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
	using static Realm.Tools.YamlTools;
	using static Verbose.Utility.VerboseAsserts;

	[TestClass]
	public class ActorQueueTest {

		static internal List<Agent> ListSample() {

			var who1 = new Agent();
			who1.Ident = 1;
			who1.Faction = 2;

			var who2 = new Agent();
			who2.Ident = 5;
			who2.Faction = 3;

			return  new List<Agent>() { who1, who2 };
		}

		static public ActorQueue Sample() {
			return new ActorQueue( ListSample() );
		}

//======================================================================================================================


		[TestMethod]
		public void ActorRef_construct_getters_setters() {
			
			var result = new ActorRef( 1, 2 );

			AreEqual( 1, result.AgentId );
			AreEqual( 2, result.Faction );
			IsTrue( result.Ready );
		}

		[TestMethod]
		public void Constructor() {

			// invocation
			var result = new ActorQueue();

			// assertions
			AreEqual( 0, result.Actors.Count );
			AreEqual( 0, result.Faction );
			AreEqual( 0, result.MaxFaction );
		}

		[TestMethod]
		public void Constructor_agentList() {

			var agents = ListSample();

			// invocation
			var result = new ActorQueue( agents );

			// assertions
			AreEqual( 2, result.Actors.Count );
			AreEqual( 0, result.Faction );
			AreEqual( 3, result.MaxFaction );

			StringsAreEqual( "- name: Peasant\n"+
					"  face: North\n"+
					"  status: Alert\n"+
					"  faction: 2\n"+
					"- name: Peasant\n"+
					"  face: North\n"+
					"  status: Alert\n"+
					"  faction: 3\n"+
					"", ToYamlString( agents ) );
		}
		
		[TestMethod]
		public void Faction_MaxFaction_NextFaction() {

			var work = Sample();
			AreEqual( 3, work.MaxFaction );
			AreEqual( 0, work.Faction );

			work.NextFaction();
			AreEqual( 3, work.MaxFaction );
			AreEqual( 1, work.Faction );

			work.NextFaction();
			AreEqual( 3, work.MaxFaction );
			AreEqual( 2, work.Faction );

			work.NextFaction();
			AreEqual( 3, work.MaxFaction );
			AreEqual( 3, work.Faction );

			work.NextFaction();
			AreEqual( 3, work.MaxFaction );
			AreEqual( 0, work.Faction );

		}

		[TestMethod]
		public void Actors() {

			var que = new ActorQueue();
			var aref = new ActorRef( 1, 2 );

			// invocation
			que.Actors.Add( aref );

			// assertions
			AreEqual( 1, que.Actors.Count );
			AreSame( aref, que.Actors[0] );
		}

		[TestMethod]
		public void AddActor() {

			var que = new ActorQueue();
			var who = new Agent();
			who.Ident = 123;
			who.Faction = 3;

			// invocation
			que.AddActor( who );

			// assertions
			AreEqual( 1, que.Actors.Count );
			AreEqual( 123, que.Actors[0].AgentId );
			AreEqual( 3, que.Actors[0].Faction );
			IsTrue( que.Actors[0].Ready );

			AreEqual( 3, que.MaxFaction );
		}

		[TestMethod]
		public void Peek_Pop() {

			// prepare, invoke, assert
			var work = Sample();
			AreEqual( ActorQueue.FACTION_ADVANCE, work.Peek() );

			work.Pop();
			AreEqual( ActorQueue.FACTION_ADVANCE, work.Peek() );

			work.Pop();
			AreEqual( 1, work.Peek() );
			work.Pop();
			AreEqual( ActorQueue.FACTION_ADVANCE, work.Peek() );

			work.Pop();
			AreEqual( 5, work.Peek() );
			work.Pop();
			AreEqual( ActorQueue.ROUND_ADVANCE, work.Peek() );

		}

		[TestMethod]
		public void Find() {

			// prepare, invoke, assert
			var work = Sample();	// faction=0
			var result = work.Find();
			IsNull( result );

			// prepare, invoke, assert
			work.NextFaction();		// faction=1
			result = work.Find();
			IsNull( result );

			// prepare, invoke, assert
			work.NextFaction();		// faction=2
			result = work.Find();
			AreEqual( 1, result.AgentId );
			AreEqual( 2, result.Faction );

			// prepare, invoke, assert
			work.NextFaction();		// faction=3
			result = work.Find();
			AreEqual( 5, result.AgentId );
			AreEqual( 3, result.Faction );
		
		}

	}
}
