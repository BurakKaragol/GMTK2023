using XNode;

namespace MrLule.Managers.DialogueMan.Nodes
{
    public class StartDialogueNode : Node, IDialogueNode
    {
        [Output] public string start;

        public NodePort GetOutput()
        {
            return GetOutputPort("start");
        }
    }
}