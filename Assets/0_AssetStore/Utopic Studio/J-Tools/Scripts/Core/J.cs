﻿using UnityEngine;

namespace J
{
    /// <summary>
    /// Main manager for JTools
    /// </summary>
    [AddComponentMenu("J/J")]
    public class J : JBase
    {
        //this class is a singleton)
        public static J Instance { get; private set; }

        /// <summary>
        /// GameObject that represents the player (checked via TAG)
        /// </summary>
        public GameObject PlayerGameObject { get; private set; }

        private void Awake()
        {
            this.Singleton();
        }

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnLevelLoaded;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject); // Nota: Esto podria eliminar todo un objeto, con otros componentes
            }
        }

        // Runs each time a scene is loaded, even when script is using DontDestroyOnLoad().
        // Finds a GameObject tagged with 'Player'. Needs to find exactly one.
        void OnLevelLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            PlayerGameObject = null;
            GameObject[] goArr = GameObject.FindGameObjectsWithTag("Player");
            if (goArr.Length > 0)
            {
                PlayerGameObject = goArr[0];
            }
        }

        /// <summary>
        /// Crea un sonido (no depende de el espacio o distancia 3D)
        /// </summary>
        /// <param name="audioClip">Clip de audio</param>
        /// <param name="volume">Volumen</param>
        public void PlaySound(AudioClip audioClip, float volume = 1f)
        {
            if (audioClip)
            {
                GameObject go = null;
                AudioSource audioSource = null;

                go = new GameObject("_j_audio_");
                audioSource = go.AddComponent<AudioSource>();
                audioSource.clip = audioClip;

                audioSource.volume = volume;
                audioSource.Play();

                Destroy(go, audioClip.length + 0.05f);
            }
            else
            {
                Debug.Log("No sound given.");
            }
        }

        /// <summary>
        /// Lerp automático que no requiere de código adentro de la función Update()
        /// </summary>
        /// <param name="deleg">Función que se llama con un parámetro float correspondiente
        /// al valor de la curva (normalmente es de 0 a 1)</param>
        /// <param name="duration">Duración de la transición de 0 a 1</param>
        /// <param name="amplitude">Aumento de amplitud por M (parámetro irá de 0 a M) </param>
        /// <param name="repeat">Cantidad de veces que se hace el Lerp</param>
        /// <param name="type">Tipo de Lerp (lineal, sinusoidal, cuadrático, logaritmico, exponencial, etc)</param>
        /// <param name="reverse">Para hacer Lerp de 1 a 0</param>
        /// <param name="callingScript">usar keyword 'this' aca. sirve para visualizar
        /// cual objeto/script llamó a JLerp</param>
        public void Lerp(JFollowCurve.CurveDelegate deleg, float duration = 1, float amplitude = 1, int repeat = 1, CurveType type = CurveType.Linear, bool reverse = false, MonoBehaviour callingScript = null)
        {
            GameObject go = new GameObject("_j_followCurve " + (int)duration + "s" + " (x" + repeat + ")");

            JFollowCurve followCurve = go.AddComponent<JFollowCurve>();
            go.transform.parent = this.transform;
            followCurve.beginFollowCurve(deleg, duration, amplitude, repeat, type, reverse, callingScript);
            Destroy(go, duration);
        }

        //@DEPRECATED - Maintained for older scripts
        public void followCurve(JFollowCurve.CurveDelegate d, float duration = 1, float amplitude = 1, int repeat = 0, CurveType type = CurveType.Linear, bool reverse = false)
        {
            Lerp(d, duration, amplitude, repeat, type, reverse);
        }
    }
}