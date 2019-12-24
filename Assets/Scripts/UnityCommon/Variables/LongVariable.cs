using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace UnityCommon.Variables
{
	[CreateAssetMenu(menuName = "Variables/Long Variable", fileName = "New Long Variable")]
	public class LongVariable : Variable<long>
	{

	}

	[Serializable]
	public class LongReference : Reference<long, LongVariable>
	{

	}



}
