using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
		
	[AddComponentMenu("J/JManager")]
	public class J : MonoBehaviour {

		//this class is a singleton)
		public static J instance {get; private set;}

		protected Transform parent;

		void Awake() {
			// Singleton:
			if (instance == null)
				instance = this;
			//else if (instance != this)
				//Destroy (this.gameObject); // Nota: Esto podria eliminar todo un objeto, con otros componentes
			// End Singleton


		}
		void Start() {
			parent = transform.JGetParent();
		}

		/*
		 * Used for doing Lerp or non-linear interpolation.
		 * The first parameter is a function that does something with a float value
		 * that changes between 0 and param_'amplitude'
		 */
		public void followCurve(JFollowCurve.CurveDelegate d , float duration = 1, float amplitude = 1, int repeat = 0, CurveType type = CurveType.Linear, bool reverse = false) {
			GameObject go = new GameObject ("JFollowCurve");

			GameObject curvesParent = JUtil.JCreateEmptyGameObjectNoDuplicates ("_followCurves_", this.parent);
				
			JFollowCurve followCurve = go.AddComponent<JFollowCurve> ();
			go.transform.parent = curvesParent.transform;
			followCurve.begin( d, duration, amplitude, repeat, type, reverse);
			Destroy (go, duration);
		}
	}


















}