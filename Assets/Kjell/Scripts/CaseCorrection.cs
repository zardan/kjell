using System;
using System.Collections;
using System.Collections.Generic;
using PM;
using UnityEngine;

namespace Kjell
{
	public class CaseCorrection : MonoBehaviour, IPMCaseSwitched, IPMTimeToCorrectCase, IPMCompilerStarted
	{
		static string errorMessage;
		public static bool hasTestDefined;

		static int inputIndex;

		static List<string> inputs;
		static int outputIndex;
		static List<string> outputs;

		public void OnPMCaseSwitched(int caseNumber)
		{
			Level currentLevel = PMWrapper.currentLevel;

			if (currentLevel.cases != null &&
			    caseNumber < currentLevel.cases.Count &&
			    currentLevel.cases[caseNumber].caseDefinition != null &&
			    ((KjellCaseDefinition)currentLevel.cases[caseNumber].caseDefinition).test != null)
			{
				var caseDefinition = (KjellCaseDefinition)currentLevel.cases[caseNumber].caseDefinition;

				hasTestDefined = true;

				inputIndex = 0;
				inputs = caseDefinition.test.input;

				outputIndex = 0;
				outputs = caseDefinition.test.output;

				if (outputs == null || outputs.Count == 0)
				{
					throw new InvalidOperationException("There is a test defined but no output defined.");
				}
			}
			else
			{
				hasTestDefined = false;
				inputs = null;
				outputs = null;
				inputIndex = 0;
				outputIndex = 0;
			}
		}

		public void OnPMCompilerStarted()
		{
			errorMessage = "";
		}

		public void OnPMTimeToCorrectCase()
		{
			CheckTooFewInputs();

			if (!string.IsNullOrEmpty(errorMessage))
			{
				PMWrapper.RaiseTaskError(errorMessage);
			}
			else
			{
				PMWrapper.SetCaseCompleted();
			}
		}

		public static IEnumerator NextInput(GameObject inputValueObject)
		{
			yield return new WaitForSeconds((1 - PMWrapper.speedMultiplier) * 2);

			if (hasTestDefined && PMWrapper.levelMode == LevelMode.Case)
			{
				if (inputs == null || inputs.Count == 0)
				{
					PMWrapper.RaiseTaskError(
						"Jag förväntade mig inga inmatningar, så nu vet jag inte vad jag ska mata in.");
				}
				else if (inputIndex > inputs.Count - 1)
				{
					PMWrapper.RaiseTaskError("För många inmatningar. Jag förväntade mig bara " + inputs.Count +
					                         " inmatningar.");
				}
				else
				{
					string nextInput = inputs[inputIndex];
					IOStream.instance.StartCoroutine(inputValueObject.GetComponent<InputValue>()
						.StartInputAnimation(nextInput));
					inputIndex++;
				}
			}
		}

		public static void NextOutput(string output)
		{
			if (hasTestDefined)
			{
				if (outputIndex > outputs.Count - 1)
				{
					errorMessage = "För många utskrifter. Jag förväntade mig bara " + outputs.Count + " utskrifter.";
				}
				else if (output != outputs[outputIndex] && string.IsNullOrEmpty(errorMessage))
				{
					errorMessage = "Fel i den " + StringifyNumber(outputIndex + 1) +
					               " utskriften. Programmet skrev ut <b>" + output + "</b> men jag förväntade mig <b>" +
					               outputs[outputIndex] + "</b>.";
				}

				outputIndex++;
			}
		}

		static string StringifyNumber(int number)
		{
			var first20 = new List<string> {
				"första",
				"andra",
				"tredje",
				"fjärde",
				"femte",
				"sjätte",
				"sjunde",
				"åttonde",
				"nionde",
				"tionde",
				"elfte",
				"tolfte",
				"trettonde",
				"fjortonde",
				"femtonde",
				"sextonde",
				"sjuttonde",
				"artonde",
				"nittonde"
			};
			var tensStart = new List<string> {
				"tjugo",
				"trettio",
				"fyrtio",
				"femtio",
				"sextio",
				"sjuttio",
				"åttio",
				"nittio"
			};
			const string tensEnd = "nde";

			if (number < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(number), "Argument must be greater than 0");
			}

			if (number < 20)
			{
				return first20[number - 1];
			}

			if (number < 100)
			{
				if (number % 10 == 0)
				{
					return tensStart[number / 10 - 2] + tensEnd;
				}

				return tensStart[number / 10 - 2] + first20[number % 10 - 1];
			}

			return number.ToString();
		}

		static void CheckTooFewInputs()
		{
			if (inputs != null && inputIndex < inputs.Count)
			{
				errorMessage = "För få inmatningar. Jag förväntade mig " + inputs.Count + " inmatningar.";
			}
			else if (outputs != null && outputIndex < outputs.Count)
			{
				errorMessage = "För få utskrifter. Jag förväntade mig " + outputs.Count + " utskrifter.";
			}
		}
	}
}
