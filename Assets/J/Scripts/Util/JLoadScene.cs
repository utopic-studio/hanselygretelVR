using UnityEngine;

namespace J
{
	
	[AddComponentMenu("J/Util/JLoadScene")]
	public class JLoadScene : MonoBehaviour {

        
		[SerializeField]	string sceneName;
		[SerializeField]	float delay = 0f;
        [SerializeField]    bool cameraFade = true;

        private JCameraFade2 jCameraFade;
        private void OnValidate()
        {
            _checkFade();
        }
        private void Reset()
        {
            _checkFade();
        }
        private void _checkFade()
        {
            // Intentar objener el componente que realiza el fade
            if (!jCameraFade)
                jCameraFade = transform.GetComponent<JCameraFade2>();
            if (!jCameraFade)
                jCameraFade = GameObject.FindObjectOfType<JCameraFade2>();

        }
        public void JLoadLevel()
        {
            this.LoadTheLevel();
        }
        public void LoadTheLevel () {
            float _delay = delay;
            if (jCameraFade)
            {
                jCameraFade.JFadeOut();
                _delay = delay + jCameraFade.fadeoutTime;
            }
            Invoke("LoadTheLevelPrivate", _delay);
		}
        private void LoadTheLevelPrivate () {
			sceneName = sceneName.Trim ();
			if (sceneName != null && sceneName != "") {
				UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
			}
		}
	}

}