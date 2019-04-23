using System.Globalization;
using Mellis;
using Mellis.Core.Interfaces;

namespace Kjell.AvailableFunctions
{
	public class InputFunction : ClrYieldingFunction
	{
		public InputFunction() : base("input")
		{
		}

		public override void InvokeEnter(params IScriptType[] arguments)
		{
			string inputLabel = "";

			if (arguments.Length > 0)
			{
				if (arguments[0].TryConvert(out double number))
				{
					inputLabel = number.ToString(CultureInfo.InvariantCulture);
				}
				else if (arguments[0].TryConvert(out bool boolean))
				{
					inputLabel = boolean.ToString();
				}
				else if (arguments[0].TryConvert(out string str))
				{
					inputLabel = str;
				}
			}

			IOStream.instance.StartCoroutine(IOStream.instance.TriggerInput(inputLabel));
		}
	}
}
