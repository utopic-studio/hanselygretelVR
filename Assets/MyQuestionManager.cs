using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using UnityEngine.SceneManagement;

[System.Serializable]
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

    public string resetingScene = "01 Bosque";

    private JSONObject json;

    public void Start()
    {
        BeginNewSession();
        if (SceneManager.GetActiveScene().name == resetingScene)
            Debug.Log("Comenzando Escena " + resetingScene);
        json = new JSONObject(JSONObject.Type.OBJECT);
    }


    //Guarda las respuestas actuales.
    public static string[] CurrentSessionAnswers;

    //Todas las sesiones guardadas hasta el momento.
    private static List<string[]> Sessions;

    //Cuantas preguntas totales
    public static int NumQuestions = 26;

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
        Save();
    }

    public static void RegisterAnswer(int Index, int Answer)
    {
        string[] PossibleAnswers = new string[3] { "A", "B", "C" };
        if (Answer < PossibleAnswers.Length)
        {
            RegisterAnswer(Index, PossibleAnswers[Answer]);
        }
    }


    public static void Save()
    {
        //JSONObject j2 = new JSONObject(JSONObject.Type.ARRAY);
        //Datos
        JSONObject Respuestas = new JSONObject(JSONObject.Type.OBJECT);
        for (int i = 1; i < NumQuestions; i++)
        {
            Respuestas.AddField("\n Respuesta " + i, CurrentSessionAnswers[i]);
           
        }




        //j2.Add(j3);

        //Debug
        //Debug.Log(j3.ToString());


        //GuardaArchivo
        string path = Application.persistentDataPath + "/DatosGuardados.json";
        File.WriteAllText(path, Respuestas.ToString());

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S)) Save(); 
    }

}



