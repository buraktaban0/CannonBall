using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace UnityCommon.Variables
{
	[CreateAssetMenu(menuName = "Variables/Int Variable", fileName = "New Int Variable")]
	public class IntVariable : Variable<int>
	{

	}

	[Serializable]
	public class IntReference : Reference<int, IntVariable>
	{

	}



}
