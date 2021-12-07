//
//	Copyright 2021 Frederick William Haslam born 1962
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Enums {

	public class ActionTypeEnumTrait {
	}

	public class ActionTypeTraits {

		static readonly public List<ActionTypeEnum> ComplexTraits = new List<ActionTypeEnum> { 
			ActionTypeEnum.Attack
		};

		static public bool IsComplex( ActionTypeEnum type) {
			return ComplexTraits.Contains( type );
		}
	}

}

