using Mellis;
using Mellis.Core.Interfaces;
using UnityEngine;

namespace Kjell.AvailableFunctions
{
	public class RandintFunction : ClrFunction
	{
		public RandintFunction() : base("randint")
		{
		}

		public override IScriptType Invoke(params IScriptType[] arguments)
		{
			if (arguments.Length < 2)
			{
				PMWrapper.RaiseError("Fel datatyp. Parametrarna till randint får bara vara heltal.");
			}

			if (!arguments[0].TryConvert(out int start))
			{
				PMWrapper.RaiseError("Fel datatyp. Parametrarna till randint får bara vara heltal.");
			}

			if (!arguments[1].TryConvert(out int end))
			{
				PMWrapper.RaiseError("Fel datatyp. Parametrarna till randint får bara vara heltal.");
			}

			if (start > end)
			{
				PMWrapper.RaiseError("Fel i parametrarna till randint. Första parametern ska vara mindre än den andra");
			}

			int randomNumber = Random.Range(start, end + 1);

			return Processor.Factory.Create(randomNumber);
		}
	}
}
