using UnityEngine;
using System.Collections;

// Question UI: has a text and buttons we call options.
// It starts deactivated.
// public: Show(), Hide(), ShowInstantly(), HideInstantly().
public class Question : MonoBehaviour {

	public enum QType {Inferencia, ExtraccionDeInfo, InterpretacionDeSentido}
	public enum QDifficulty {Basico, Intermedio, Avanzado}

	[HeaderAttribute("set Question Number to 1 or more")]
	public int questionNumber;
	public QType type;
	public QDifficulty difficulty;
	[Space(4)]
	public UIAlpha childText;
	public UIAlpha childOptions;
	public UIAlpha childButton;
	public AudioSource textSound;
	public AudioSource optionsSound;
	public AudioSource correctSound;
	public AudioSource incorrectSound;
	public AudioSource buttonSound;
	public float timeOffsetForOptions = 2f;
	public float timeOffsetForTransition = 1f;
	public TextUtopic asociatedText;
	[Space(4)]
	public GameObject[] options;
	public UnityEngine.Events.UnityEvent onFinishAnswering;

	// Json variables
	[HideInInspector] public bool userAnsweredCorrectly;
	[HideInInspector] public int realAnswer;
	[HideInInspector] public int userTries;
	[HideInInspector] public float playerPoints;
	[HideInInspector] public int questionType;
	[HideInInspector] public string questionTypeDescription;
	[HideInInspector] public int questionDifficulty;
	[HideInInspector] public string questionDifficultyDescription;
	[HideInInspector] public int questionTotalPoints;
	[HideInInspector] public string questionText;
	[HideInInspector] public string question;
	[HideInInspector] public string[] optionTexts;
	[HideInInspector] public string optionsType;


	private UnityEngine.UI.GraphicRaycaster[] optionsRaycasters;

	void Start() {
		//updateJsonVariables (); //TODO: comentado temporalmente. descomentar
		Invoke ("addMe", 0.2f);

		optionsRaycasters = new UnityEngine.UI.GraphicRaycaster[options.Length];
		int i = 0;
		foreach (var obj in options) {
			
			optionsRaycasters [i] = obj.GetComponent<UnityEngine.UI.GraphicRaycaster> ();
			i++;
		}
		setIfButtonsAreInteractable (false);
	}
	void addMe() {
		QuestionManager.Instance.addQuestion (this, this.questionNumber-1);
	}

	public void Show() {
		StartCoroutine (ShowPrivate ());
	}
	private IEnumerator ShowPrivate() {
		childText.Show ();
		childButton.Show ();
		textSound.Play ();
		yield return new WaitForSeconds (timeOffsetForOptions);
		childOptions.Show ();
		optionsSound.Play();
		yield return new WaitForSeconds (0.5f); //wait a bit of fade in for the buttons
		setIfButtonsAreInteractable(true);
	}
	public void Hide() {
		childText.Hide ();
		childOptions.Hide ();
		childButton.Hide ();
		setIfButtonsAreInteractable (false);
	}
	public void ShowInstantly() {
		childText.ShowInstantly ();
		childButton.ShowInstantly ();
		childOptions.ShowInstantly ();
		textSound.Play ();
		setIfButtonsAreInteractable (true);
	}
	public void HideInstantly() {
		childText.HideInstantly ();
		childButton.HideInstantly ();
		childOptions.HideInstantly ();
		setIfButtonsAreInteractable (false);
	}
	private void setIfButtonsAreInteractable(bool param) {
		for (int i = 0; i < optionsRaycasters.Length; i++) {
			optionsRaycasters [i].enabled = param;
		}
	}



	public void TransitionToText() {
		StartCoroutine (TransitionToTextPrivate ());
	}
	private IEnumerator TransitionToTextPrivate() {
		this.Hide ();
		buttonSound.Play ();
		yield return new WaitForSeconds (timeOffsetForTransition);
		if (asociatedText != null) {
			asociatedText.Show ();
		}
	}

	private void updateJsonVariables() {
		QuestionManager q = QuestionManager.Instance;
		userAnsweredCorrectly = q.playerAnswers [questionNumber - 1];
		realAnswer = q.realAnswers [questionNumber - 1];
		userTries = q.tries [questionNumber - 1];
		playerPoints = userAnsweredCorrectly ? questionTotalPoints*(q.maxChancesPerQuestion-userTries+1)/q.maxChancesPerQuestion : 0;
		questionType = (int)type;
		questionTypeDescription = questionType == 0 ? "Inferencia" : questionType == 1 ? "Extraer información" : "Interpretar el sentido";
		questionDifficulty = (int)difficulty;
		questionDifficultyDescription = questionDifficulty == 0 ? "Básico" : questionDifficulty == 1 ? "Intermedio" : "Avanzado";
		questionTotalPoints = q.points [questionNumber - 1];
		questionText = q.questionTexts1 [questionNumber - 1];
		question = q.questionTexts2 [questionNumber - 1];
		//print (optionTexts.Length);
		//print (q.optionTexts.Length);
		//print (q.optionTexts [questionNumber - 1].Split ('/').Length);
		optionTexts = new string[q.optionTexts [questionNumber - 1].Split ('/').Length];

		if (q.optionTexts [questionNumber - 1].Trim() != "") {
			string[] split = q.optionTexts [questionNumber - 1].Split ('/');
			print ( split[0] );
			print ( q.optionTexts [questionNumber - 1].Split ('/')[0] );

			optionTexts [0] = q.optionTexts [questionNumber - 1].Split ('/') [0];
			optionTexts [1] = q.optionTexts [questionNumber - 1].Split ('/') [1];
			optionTexts [2] = q.optionTexts [questionNumber - 1].Split ('/') [2];
			optionsType = "text";
		} else {
			optionTexts = null;
			optionsType = "image";
		}
		addMe ();
	}

}
