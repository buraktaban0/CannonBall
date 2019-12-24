using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Variables;
using UnityEngine;

namespace UnityCommon.UI.EventBinding
{
	public class TextEventBinderFloat : TextEventBinder<FloatReference>
	{
		[TextArea]
		public string format = "";

		public string floatFormat = "";

		public override void OnEventFired()
		{
			var format = this.format.Trim();

			if (format == null || format.Length < 1)
			{
				element.text = variable.Value.ToString(floatFormat);
				return;
			}

			element.text = string.Format(format, variable.Value.ToString(floatFormat));

		}
	}
}
