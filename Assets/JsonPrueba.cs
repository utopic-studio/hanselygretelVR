using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonPrueba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Answers answers = new Answers();
        answers.Respuestas = "X";

        string json = JsonUtility.ToJson(answers);
        Debug.Log(json);
    }


    private class Answers
    {
        
        public string Respuestas;
    }
}
