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
        Sessions = new List<string[]>();
    }

    public void Start()
    {
      BeginNewSession();
        
    }


    //Guarda las respuestas actuales.
    private static string[] CurrentSessionAnswers;

    //Todas las sesiones guardadas hasta el momento.
    private static List<string[]> Sessions;

    //Cuantas preguntas totales
    private int NumQuestions = 25;

    public void BeginNewSession()
    {
        CurrentSessionAnswers  = new string[NumQuestions];
        
        Sessions.Add(CurrentSessionAnswers);

    }

    public static void RegisterAnswer(int Index, string Answer)
    {
        if (Index < CurrentSessionAnswers.Length)
        {
            CurrentSessionAnswers[Index] = Answer;
        }

        //Debug.Log("Las respuesta de la pregunta "+ Index + " fue: " + CurrentSessionAnswers[Index]);
        //Debug.Log("la repuesta 1 fue"+CurrentSessionAnswers[1]);
        
    }

    public static void RegisterAnswer(int Index, int Answer)
    {
        string[] PossibleAnswers = new string[3] { "A", "B", "C" };
        if (Answer < PossibleAnswers.Length)
        {
            RegisterAnswer(Index, PossibleAnswers[Answer]);
        }
    }

  
}



