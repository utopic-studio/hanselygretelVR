using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOrderController : MonoBehaviour
{
    public TextVR[] texts;
    
    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Previous()
    {
        texts[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex - 1) % texts.Length;
        texts[currentIndex].gameObject.SetActive(true);
    }
    public void Next()
    {
        texts[currentIndex].gameObject.SetActive(false);
        string msg = "desactivado: " + texts[currentIndex];

        currentIndex = (currentIndex + 1) % texts.Length;
        texts[currentIndex].gameObject.SetActive(true);
        msg += "\tactivado: " + texts[currentIndex];

        Debug.Log(msg);
    }
}
