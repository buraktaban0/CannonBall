using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace UnityCommon.Variables
{
	[CreateAssetMenu(menuName = "Variables/Float Variable", fileName = "New Float Variable")]
	public class FloatVariable : Variable<float>
	{

	}

	[Serializable]
	public class FloatReference : Reference<float, FloatVariable>
	{

	}



}
