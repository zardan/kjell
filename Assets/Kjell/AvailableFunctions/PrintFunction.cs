using System.Globalization;
using System.Text;
using Mellis;
using Mellis.Core.Interfaces;

namespace Kjell.AvailableFunctions
{
	public class PrintFunction : ClrFunction
	{
		public PrintFunction() : base("print")
		{
		}

		public override IScriptType Invoke(params IScriptType[] arguments)
		{
			var builder = new StringBuilder();

			foreach (IScriptType arg in arguments)
			{
				if (builder.Length > 0)
				{
					builder.Append(' ');
				}

				if (arg.TryConvert(out string s))
				{
					builder.Append(s);
				}
				else if (arg.TryConvert(out double d))
				{
					builder.Append(d.ToString(CultureInfo.InvariantCulture));
				}
				else if (arg.TryConvert(out bool b))
				{
					builder.Append(b);
				}
			}

			IOStream.instance.Print(builder.ToString());

			return null;
		}
	}
}
