using System.Linq;
using Compiler;
using Kjell;

public class PrintFunction : Function
{
	public PrintFunction()
	{
		name = "print";
		buttonText = "print()";
		pauseWalker = false;
		hasReturnVariable = false;

		for (int i = 0; i < 20; i++)
			inputParameterAmount.Add(i);
	}

	public override Variable runFunction(Scope currentScope, Variable[] inputParas, int lineNumber)
	{
		var message = "";

		var index = 0;
		foreach (var parameter in inputParas)
		{
			if (parameter.variableType == VariableTypes.textString)
				message += parameter.getString();
			else if (parameter.variableType == VariableTypes.number)
				message += parameter.getNumber();
			else if (parameter.variableType == VariableTypes.boolean)
				message += parameter.getBool();

			if (index < inputParas.Length-1)
				message += " ";

			index++;
		}

		IOStream.Instance.Print(message);

		return new Variable();
	}
}
