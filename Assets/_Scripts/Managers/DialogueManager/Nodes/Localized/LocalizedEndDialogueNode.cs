using UnityEngine;
using UnityEngine.Localization;
using XNode;

namespace MrLule.Managers.DialogueMan.Nodes
{
    public class LocalizedEndDialogueNode : Node, IDialogueNode
    {
        [Input(connectionType = ConnectionType.Multiple)] public LocalizedString end;
    }
}
