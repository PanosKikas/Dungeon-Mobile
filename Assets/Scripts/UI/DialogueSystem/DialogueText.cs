using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Interaction/Dialogue", fileName ="DialogueSample")]
public class DialogueText : ScriptableObject
{
    [TextArea]
    public List<string> Text;
}
