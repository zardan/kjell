using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Kjell
{
	public class InputValue : MonoBehaviour
	{
		private bool hasBeenSubmitted;
		public Text SubmittedText;

		public InputField InputField;

		public GameObject InputFieldBase;
		public GameObject SendButton;

		public Image BubbleImage;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
			{
				if(!hasBeenSubmitted)
					SubmitInput();
			}
		}

		public void SubmitInput()
		{
			hasBeenSubmitted = true;

			if (!string.IsNullOrEmpty(InputField.text))
				SubmittedText.text = InputField.text;

            InputFieldBase.SetActive(false);
            SendButton.SetActive(false);
            SubmittedText.gameObject.SetActive(true);

			GetComponent<Container>().SetWidth(InputField.text.Length);

            IOStream.Instance.InputSubmitted(SubmittedText.text);
        }

		public IEnumerator StartInputAnimation(string message)
		{
			foreach (var character in message)
			{
				InputField.text += character;
				InputField.caretPosition = message.Length;

				yield return new WaitForSeconds((1 - PMWrapper.speedMultiplier) * 0.4f);
			}

			yield return new WaitForSeconds((1 - PMWrapper.speedMultiplier) * 2);

			SubmitInput();
		}

		public void DeactivateInputValue()
		{
			if (!string.IsNullOrEmpty(InputField.text))
				SubmittedText.text = InputField.text;

			InputFieldBase.SetActive(false);
			SendButton.SetActive(false);
			SubmittedText.gameObject.SetActive(true);
		}
    }

    
}
