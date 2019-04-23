using System;
using System.Collections;
using System.Collections.Generic;
using PM;
using UnityEngine;
using UnityEngine.UI;


namespace Kjell
{
	public class IOStream : MonoBehaviour, IPMCompilerStarted, IPMLevelChanged, IPMCompilerStopped, IPMCaseSwitched, IPMSwitchedToSandbox
	{
		public string LatestReadInput;

		public GameObject PrintPrefab;
		public GameObject LabelPrefab;
		public GameObject ValuePrefab;

		public Sprite InputLabelPop;
		public Sprite InputValuePop;
		public Sprite InputLabelPlain;
		public Sprite InputValuePlain;

		private GameObject labelObject;
		private GameObject valueObject;

		public Dictionary<int, string> LinesWithInput;

		public static IOStream Instance;

		private void Start()
		{
			if (Instance == null)
				Instance = this;
		}

		public void Print(string message)
		{
			CaseCorrection.NextOutput(message);

			var outputObject = Instantiate(PrintPrefab);
			outputObject.transform.SetParent(gameObject.transform, false);
			var output = outputObject.GetComponent<Output>();
			message = message.Replace("\\n", "\n");
			output.Text.text = message;
			outputObject.GetComponent<Container>().SetWidth(message.Length);
		}

		public IEnumerator TriggerInput(string message)
		{
			labelObject = Instantiate(LabelPrefab);
			labelObject.transform.SetParent(gameObject.transform, false);
			labelObject.GetComponent<InputLabel>().Text.text = message;
			labelObject.GetComponent<InputLabel>().BubbleImage.sprite = InputLabelPop;
			labelObject.GetComponent<Container>().SetWidth(message.Length);

			yield return new WaitForSeconds(2 * (1 - PMWrapper.speedMultiplier));
			
			valueObject = Instantiate(ValuePrefab);
			valueObject.transform.SetParent(gameObject.transform, false);
			valueObject.GetComponent<InputValue>().BubbleImage.sprite = InputValuePop;
			valueObject.GetComponent<InputValue>().InputFieldBase.GetComponent<InputField>().Select();

			StartCoroutine(CaseCorrection.NextInput(valueObject));
		}

		public void InputSubmitted(string submitedText)
		{
			LatestReadInput = submitedText;

			if (labelObject != null)
				labelObject.GetComponent<InputLabel>().BubbleImage.sprite = InputLabelPlain;

			valueObject.GetComponent<InputValue>().BubbleImage.sprite = InputValuePlain;

			CodeWalker.SubmitInput.Invoke(submitedText, CodeWalker.CurrentScope);
			PMWrapper.UnpauseWalker();
		}

		private void Clear()
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}
		}

		private void DeactivateLastInput()
		{
			if (gameObject.transform.childCount > 0)
			{
				var inputValue = gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<InputValue>();
				if (inputValue != null)
					inputValue.DeactivateInputValue();
			}
		}

		public void OnPMCompilerStarted()
		{
			Clear();
		}

		public void OnPMLevelChanged()
		{
			DeactivateLastInput();
			Clear();
		}

		public void OnPMCompilerStopped(HelloCompiler.StopStatus status)
		{
			DeactivateLastInput();
			StopAllCoroutines();
		}

		public void OnPMCaseSwitched(int caseNumber)
		{
			DeactivateLastInput();
			Clear();
		}

		public void OnPMSwitchedToSandbox()
		{
			Clear();
		}
	}
}
