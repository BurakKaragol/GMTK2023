using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace MrLule.Managers.DialogueMan.Nodes
{
    public class DialogueNode : Node, IDialogueNode
    {
        [Input]
        public string from;
        public string talker;
        [TextArea(5, 10)] 
        public string text;
        public float typeSpeed = 0.05f;
        public string soundName;
        public Sprite sprite;
        [Output(dynamicPortList = true)]
        [TextArea(1, 10)]
        public List<string> answers;

        public List<string> GetAnswers()
        {
            return answers;
        }
    }
}
