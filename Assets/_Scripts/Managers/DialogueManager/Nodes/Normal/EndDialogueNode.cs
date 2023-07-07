using UnityEngine;
using XNode;

namespace MrLule.Managers.DialogueMan.Nodes
{
    public class EndDialogueNode : Node, IDialogueNode
    {
        [Input(connectionType = ConnectionType.Multiple)] public string end;
    }
}
