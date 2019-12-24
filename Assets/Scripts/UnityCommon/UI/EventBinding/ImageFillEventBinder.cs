using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Events;
using UnityCommon.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCommon.UI.EventBinding
{
	[ExecuteInEditMode]
	public class ImageFillEventBinder : UIEventBinder<Image, FloatReference>
	{

		public FloatReference maxValue;


		public override void OnEventFired()
		{
			element.fillAmount = Mathf.Clamp01(variable.Value / maxValue.Value);
		}

	}
}
