//
//	Copyright 2021 Frederick William Haslam born 1962
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm.Enums {

	public enum ActionTypeEnum {

		Right,		// turn right 1/8
		Left,		// turn left 1/8 
		Forward,	// step forward one space

		Attack,		// attack a damagable target ( agent )
		//Defend,		// take a defensive stance / block
		EndAction,	// agents get two actions per turn
	}
}
