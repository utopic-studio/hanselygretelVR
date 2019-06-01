﻿using UnityEngine;

namespace J
{
    [AddComponentMenu("J/Util/JCameraFade2")]
    public class JCameraFade2 : MonoBehaviour
    {

        [Header("Llama a este script con JAction")]
        [Tooltip("Dejar vacio para que busque el tag MainCamera")]
        [SerializeField] Camera mainCamera;
        [SerializeField] Color opaqueColor = Color.black;
        [SerializeField] Color transparentColor = Color.clear;
        [SerializeField] float fadeinTime = 1f;
        [SerializeField] float fadeoutTime = 1f;
        [SerializeField] bool ignoreTimeScale = false;
        [SerializeField] float distanceFromCamera = 0.05f;
        public bool fadeInAtStart = true;

        private UnityEngine.UI.Image myImage;
        private string JFadeCanvasName = "JFadeCanvas";
        private string JFadeImageName = "JFadeImage";

        private GameObject block_screen_obj;
        private GameObject block_screen_img_obj;

        private void OnValidate()
        {
            if (block_screen_img_obj)
                block_screen_obj.transform.localPosition = new Vector3(0f, 0f, distanceFromCamera);
        }
        private void Start()
        {
            this.CreateFadeCanvasIfNeeded(true);
        }



        public void JFadeIn()
        {
            _Fade(fadeinTime, this.transparentColor);
        }
        public void JFadeOut()
        {
            _Fade(fadeoutTime, this.opaqueColor);
        }
        public void JFadeInInstantly()
        {
            _Fade(0f, this.transparentColor);
        }
        public void JFadeOutInstantly()
        {
            _Fade(0f, this.opaqueColor);
        }




        private void _Fade(float fadeDuration, Color targetColor)
        {
            CreateFadeCanvasIfNeeded(false);

            Color imgColorBeforeLerp = myImage.color;
            J2.Instance.JLerp((x) =>
           {
               myImage.color = Color.Lerp(imgColorBeforeLerp, targetColor, x);
           }, fadeDuration);
        }


        /// <summary>
        /// Crea un canvas que tapa a la cámara si es que aún no existe. Busca la primera cámara con el tag MainCamera
        /// </summary>
        private void CreateFadeCanvasIfNeeded(bool calledAtStart)
        {
            

            GameObject camobj = GameObject.FindGameObjectWithTag("MainCamera");
            if (camobj)
            {
                Camera cam = camobj.GetComponent<Camera>();
                if (cam)
                {
                    mainCamera = cam;
                    if (!mainCamera.transform.Find(JFadeCanvasName))
                    {
                        this.CreateImageInFrontOfCamera(mainCamera, JFadeCanvasName, JFadeImageName, calledAtStart);
                    }
                }
                else
                    Debug.LogWarning("JWarning - JCameraFade: El primero objeto con tag MainCamera encontrado no tiene el componente Camera");
            }
            else
                Debug.LogWarning("JWarning - JCameraFade: No se encuentra el tag MainCamera en la escena");
        }

        private void CreateImageInFrontOfCamera(Camera cam, string canvasName, string imageName, bool calledAtStart)
        {

            block_screen_obj = new GameObject(canvasName);
            block_screen_obj.transform.parent = cam.transform;


            Canvas cv = block_screen_obj.AddComponent<Canvas>();
            cv.renderMode = RenderMode.WorldSpace;
            block_screen_obj.transform.JReset();
            block_screen_obj.transform.localPosition = new Vector3(0f, 0f, distanceFromCamera);
            block_screen_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(2, 2);


            block_screen_img_obj = new GameObject(imageName);
            block_screen_img_obj.transform.parent = block_screen_obj.transform;
            block_screen_img_obj.transform.JReset();
            UnityEngine.UI.Image img_img = block_screen_img_obj.AddComponent<UnityEngine.UI.Image>();
            img_img.raycastTarget = false;

            block_screen_img_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(2, 2);
            
            myImage = img_img;
            myImage.color = opaqueColor;


            // Initial camera state
            if (calledAtStart)
            {
                if (fadeInAtStart)
                    this.JFadeIn();
                else
                    this.JFadeInInstantly();
            }


        }


    }

}