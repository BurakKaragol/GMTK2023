using UnityEngine.Localization;
using XNode;

namespace MrLule.Managers.DialogueMan.Nodes
{
    public class LocalizedStartDialogueNode : Node, IDialogueNode
    {
        [Output] public LocalizedString start;

        public NodePort GetOutput()
        {
            return GetOutputPort("start");
        }
    }
}
