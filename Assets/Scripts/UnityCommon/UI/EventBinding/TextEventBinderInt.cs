using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Variables;
using UnityEngine;

namespace UnityCommon.UI.EventBinding
{
	public class TextEventBinderInt : TextEventBinder<IntReference>
	{
		[TextArea]
		public string format = "";

		public override void OnEventFired()
		{
			Debug.Log("Text: " + variable.Value);

			var format = this.format.Trim();

			if (format == null || format.Length < 1)
			{
				element.text = variable.Value.ToString();
				return;
			}

			element.text = string.Format(format, variable.Value.ToString());

		}
	}
}
