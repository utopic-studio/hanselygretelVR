using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuestionManager : MonoBehaviour
{
    public static MyQuestionManager Instance { get; private set; }

    void Awake()
    {
        // Singleton:
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject); // Nota: Esto podria eliminar todo un objeto, con otros componentes
        DontDestroyOnLoad(gameObject);
        // End Singleton
    }

    public void Start()
    {
        for (int i = 0; i < 1; i++) {
            BeginNewSession();
            Debug.Log("Inicie");
        }
    }


    //Guarda las respuestas actuales.
    private static string[] CurrentSessionAnswers;

    //Todas las sesiones guardadas hasta el momento.
    private List<string[]> Sessions;

    //Cuantas preguntas totales
    public int NumQuestions;

    public void BeginNewSession()
    {
        CurrentSessionAnswers  = new string[NumQuestions];
        //Sessions.Add(CurrentSessionAnswers);

    }

    public static void RegisterAnswer(int Index, string Answer)
    {
        if (Index < CurrentSessionAnswers.Length)
        {
            CurrentSessionAnswers[Index] = Answer;
        }
        Debug.Log("La pregunta "+ Index+ " se respondio con " + Answer);
    }

}



