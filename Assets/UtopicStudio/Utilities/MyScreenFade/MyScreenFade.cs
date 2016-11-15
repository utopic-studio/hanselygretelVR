using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityStandardAssets.ImageEffects.ScreenOverlay))]
public class MyScreenFade : MonoBehaviour {

	enum State {FadeIn, Normal, Covered, FadeOut}

	[SerializeField] State startMode = State.FadeIn;
	public float timeFadeInScreen = 2.5f;
	public float timeFadeOutScreen = 2.5f;
	public float uncoveredIntensity = 2f;
	public float coveredIntensity = 0f;
	public bool useWhite = false;

	private UnityStandardAssets.ImageEffects.ScreenOverlay m_screenOverlay;
	private float m_lerpVar;
	private State state;

	private string guid;

	// Use this for initialization
	void Start () {
		m_screenOverlay = GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay> ();
		m_screenOverlay.blendMode = UnityStandardAssets.ImageEffects.ScreenOverlay.OverlayBlendMode.Multiply;

		if (useWhite) {
			uncoveredIntensity = 2f;
			coveredIntensity = 6f;
		}

		/* Este codigo asignaba un Texture2D. Pero me di cuenta que no era necesario.
		 * Lo dejo comentado por si despues lo tengo que volver a usar.
		 * 
		 * if (!m_screenOverlay.texture) {
			m_screenOverlay.texture = Texture2D.blackTexture;
			for (int y = 0; y < m_screenOverlay.texture.height; y++) {
				for (int x = 0; x < m_screenOverlay.texture.width; x++) {
					Color color = Color.black;
					m_screenOverlay.texture.SetPixel(x, y, color);
				}
			}
			m_screenOverlay.texture.Resize (2000, 2000);
			m_screenOverlay.texture.Apply();
		}*/
		
		if (!m_screenOverlay.overlayShader) {
			/*
			string[] ans = "asd";//TODO //UnityEditor.AssetDatabase.FindAssets ("BlendModesOverlay", new string[] { "Assets/Standard Assets" });
			if (ans.Length > 0)
				guid = ans [0];
			string path = "asd";//TODO //UnityEditor.AssetDatabase.GUIDToAssetPath (guid);
			//TODO //Shader shader = (Shader)UnityEditor.AssetDatabase.LoadAssetAtPath (path, typeof(Shader));
			//TODO //m_screenOverlay.overlayShader = shader;
			*/
		}

		m_screenOverlay.enabled = true;

		switch (startMode) {
		case State.FadeIn:
			fadeInScreen();
			break;
		case State.FadeOut:
			fadeOutScreen ();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.FadeIn:
			if (m_lerpVar < 1)
				m_lerpVar += Time.deltaTime / timeFadeInScreen;
			else
				state = State.Normal;
			m_screenOverlay.intensity = Mathf.Lerp (coveredIntensity, uncoveredIntensity, m_lerpVar);
			break;
		case State.FadeOut:
			if (m_lerpVar < 1)
				m_lerpVar += Time.deltaTime / timeFadeOutScreen;
			else
				state = State.Covered;
			m_screenOverlay.intensity = Mathf.Lerp (uncoveredIntensity, coveredIntensity, m_lerpVar);
			break;
		}
	}

	public void fadeInScreen() {
		m_lerpVar = 0f;
		state = State.FadeIn;
	}
	public void fadeOutScreen() {
		m_lerpVar = 0f;
		state = State.FadeOut;
	}
	public void fadeInScreenInstantly() {
		m_screenOverlay.intensity = uncoveredIntensity;
		state = State.Normal;
	}
	public void fadeOutScreenInstantly() {
		m_screenOverlay.intensity = coveredIntensity;
		state = State.Covered;
	}
}
