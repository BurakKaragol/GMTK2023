using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using XNode;

namespace MrLule.Managers.DialogueMan.Nodes
{
    public class LocalizedDialogueNode : Node, IDialogueNode
    {
        [Input] 
        public LocalizedString from;
        public LocalizedString talker;
        public LocalizedString text;
        public float typeSpeed = 0.05f;
        public string soundName;
        public Sprite sprite;
        [Output(dynamicPortList = true)]
        public List<LocalizedString> answers;

        public List<LocalizedString> GetAnswers()
        {
            return answers;
        }
    }
}
