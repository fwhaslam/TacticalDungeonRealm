
namespace Realm.Analysis {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class WorkshopAnalysisException : SystemException {

		public WorkshopAnalysisException( string msg ) : base( msg ) {}

		public WorkshopAnalysisException( string msg, Exception ex ) : base( msg, ex ) {}
	}
}
