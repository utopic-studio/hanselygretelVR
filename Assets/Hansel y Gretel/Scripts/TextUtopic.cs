using UnityEngine;
using System.Collections;

// It starts deactivated.
// public: Show(), Hide(), ShowInstantly(), HideInstantly().
public class TextUtopic : MonoBehaviour {

	public Question asociatedQuestion;
	public UIAlpha childText;
	public UIAlpha childButton;
	public AudioSource textSound;
	public AudioSource buttonSound;
	public float timeOffsetForTransition = 1f;
	[Space(4)]
	public GameObject canvasWithOkButton;

	private UnityEngine.UI.GraphicRaycaster OkButtonRaycaster;

	void Start() {
		OkButtonRaycaster = canvasWithOkButton.GetComponent<UnityEngine.UI.GraphicRaycaster> ();
		setIfButtonsAreInteractable (false);
	}

	public void Show() {
		StartCoroutine (ShowPrivate ());
	}
	private IEnumerator ShowPrivate() {
		childText.Show ();
		childButton.Show ();
		textSound.Play ();
		yield return new WaitForSeconds (0.5f); //wait a bit of fade in for the buttons
		setIfButtonsAreInteractable(true);
	}
	public void TransitionToQuestion() {
		StartCoroutine (TransitionToQuestionPrivate ());
	}
	private IEnumerator TransitionToQuestionPrivate() {
		this.Hide ();
		buttonSound.Play ();
		yield return new WaitForSeconds (timeOffsetForTransition);
		if (asociatedQuestion != null) {
			asociatedQuestion.Show ();
		}
	}
	public void Hide() {
		childText.Hide ();
		childButton.Hide ();
		setIfButtonsAreInteractable (false);
	}
	public void ShowInstantly() {
		childText.ShowInstantly ();
		childButton.ShowInstantly ();
		textSound.Play ();
		setIfButtonsAreInteractable (true);
	}
	public void HideInstantly() {
		childText.HideInstantly ();
		childButton.HideInstantly ();
		setIfButtonsAreInteractable (false);
	}

	private void setIfButtonsAreInteractable(bool param) {
		OkButtonRaycaster.enabled = param;
	}
}
