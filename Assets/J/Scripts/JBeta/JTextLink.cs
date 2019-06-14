using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTextLink : MonoBehaviour
{
    [Header("Este texto se pone sobre uno o ambos objetos referenciados")]
    [TextAreaAttribute]
    public string content;
    public UnityEngine.UI.Text text;
    public TMPro.TextMeshProUGUI textPro;

    void OnValidate()
    {
        
        if (text)
            text.text = content;
        if (textPro)
            textPro.text = content;
    }
}
