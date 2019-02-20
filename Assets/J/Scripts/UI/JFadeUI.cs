﻿using UnityEngine;

namespace J
{

	[AddComponentMenu("J/UI/JFadeUI")]
	public class JFadeUI : MonoBehaviour {

		enum UIAlphaStartMode {invisible, visible, visibleFadeIn, invisibleFadeOut}

        [Tooltip("If empty it uses this object's CanvasGroup")]
        [SerializeField]    CanvasGroup[] canvasGroup;
		[SerializeField]	UIAlphaStartMode startVisibility = UIAlphaStartMode.visible;
		[SerializeField]	float fadeInTime = 1f;
		[SerializeField]	float fadeOutTime = 1f;
        [SerializeField] UnityEngine.Events.UnityEvent[] OnFadeInStarting;
        [SerializeField] UnityEngine.Events.UnityEvent[] OnFadeOutEnded;


        void Start () {

			switch (startVisibility) {
			case UIAlphaStartMode.invisible:
				HideInstantly ();
				break;
			case UIAlphaStartMode.visible:
				ShowInstantly ();
				break;
			case UIAlphaStartMode.visibleFadeIn:
				Show ();
				break;
			case UIAlphaStartMode.invisibleFadeOut:
				Hide ();
				break;
			default:
				break;
			}
		}

		public void Show() {
            Invoke("CallOnFadeInStarting", 0f);
            foreach (var g in canvasGroup)
			    J.instance.followCurve (x => g.alpha = x, duration: fadeInTime, repeat: 1, type: CurveType.Linear);
        }
		public void Hide() {
            foreach (var g in canvasGroup)
			    J.instance.followCurve (x => g.alpha = x, duration: fadeOutTime, repeat: 1, type: CurveType.Linear, reverse: true);
            Invoke("CallOnFadeOutEnded", fadeOutTime);
        }
		public void ShowInstantly() {
            foreach (var g in canvasGroup)
                g.alpha = 1f;
            Invoke("CallOnFadeInStarting", 0f);
        }
		public void HideInstantly() {
            foreach (var g in canvasGroup)
                g.alpha = 0f;
            Invoke("CallOnFadeOutEnded", 0f);
        }

		public void SetFadeInTime(float f) {
			this.fadeInTime = f;
		}
		public void SetFadeOutTime(float f) {
			this.fadeOutTime = f;
		}
        public void CallOnFadeInStarting()
        {
            foreach (var item in OnFadeInStarting)
                item.Invoke();
        }
        public void CallOnFadeOutEnded()
        {
            foreach (var item in OnFadeOutEnded)
                item.Invoke();
        }

    }

}