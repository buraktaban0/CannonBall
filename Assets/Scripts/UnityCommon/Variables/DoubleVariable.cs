using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace UnityCommon.Variables
{
	[CreateAssetMenu(menuName = "Variables/Double Variable", fileName = "New Double Variable")]
	public class DoubleVariable : Variable<double>
	{

	}

	[Serializable]
	public class DoubleReference : Reference<double, DoubleVariable>
	{

	}



}
