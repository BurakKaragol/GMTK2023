using MrLule.General;
using MrLule.Managers.DialogueMan.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
using XNode;

namespace MrLule.Managers.DialogueMan
{
    /// <summary>
    /// Localization table implementation
    /// </summary>
    public class DialogueManager : Manager
    {
        [Header("Localization:")]
        [SerializeField] private bool useLocalization = false;

        [Header("UI Elements:")]
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI sentenceText;
        [Tooltip("Maximum 3!")]
        [SerializeField] private TextMeshProUGUI[] answerButtonTexts;

        [Header("Animation:")]
        [SerializeField] private bool useAnimation;
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private string dialogueTriggerBool;
        [SerializeField] private float animationTime = 1f;

        [Header("Text Animation:")]
        [SerializeField] private bool canSkip = true;

        private Animator dialogueBoxAnimator;
        private List<Node> nodes = new List<Node>();
        private List<string> answers = new List<string>();
        private List<LocalizedString> localizedAnswers = new List<LocalizedString>();
        private Image[] images;
        private StartDialogueNode startNode = null;
        private LocalizedStartDialogueNode localizedStartNode = null;
        private DialogueNode currentNode = null;
        private LocalizedDialogueNode currentLocalizedNode = null;
        private bool isWriting = false;
        private bool complete = false;

        private void Start()
        {
            dialogueBoxAnimator = dialogueBox.GetComponent<Animator>();
        }

        public void ImportAndStartDialogue(DialogueNodeGraph dialogueGraph)
        {
            complete = false;
            nodes = dialogueGraph.nodes;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (startNode != null)
                {
                    continue;
                }
                if (nodes[i].GetType() == typeof(StartDialogueNode))
                {
                    if (useLocalization)
                    {
                        localizedStartNode = nodes[i] as LocalizedStartDialogueNode;
                    }
                    else
                    {
                        startNode = nodes[i] as StartDialogueNode;
                    }
                }
            }

            if (nameText == null)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot display name (Name UI is null)");
                return;
            }

            if (sentenceText == null)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot display sentence (Sentence UI is null)");
                return;
            }

            if (useLocalization)
            {
                NodePort port = localizedStartNode.GetOutputPort("to").Connection;
                currentLocalizedNode = port.node as LocalizedDialogueNode;

                string soundName = currentLocalizedNode.soundName;
                if (!(soundName == "" || soundName == null))
                {
                    audioManager.Play(soundName);
                }

                if (currentNode.sprite != null)
                {
                    image.sprite = currentLocalizedNode.sprite;
                }

                DisplayLocalizedNodeData();

                localizedAnswers = currentLocalizedNode.answers;
            }
            else
            {
                NodePort port = startNode.GetOutputPort("to").Connection;
                currentNode = port.node as DialogueNode;

                string soundName = currentNode.soundName;
                if (!(soundName == "" || soundName == null))
                {
                    audioManager.Play(soundName);
                }

                if (currentNode.sprite != null)
                {
                    image.sprite = currentNode.sprite;
                }

                DisplayNodeData();

                answers = currentNode.answers;
            }

            PrepareAnswer(answers.Count);
            for (int i = 0; i < answers.Count; i++)
            {
                answerButtonTexts[i].SetText(answers[i]);
            }

            StartCoroutine(StartDialogue());
        }

        private void PrepareAnswer(int count)
        {
            switch (count)
            {
                case 0:
                    answerButtonTexts[0].transform.parent.gameObject.SetActive(false);
                    answerButtonTexts[1].transform.parent.gameObject.SetActive(false);
                    answerButtonTexts[2].transform.parent.gameObject.SetActive(false);
                    break;
                case 1:
                    answerButtonTexts[0].transform.parent.gameObject.SetActive(true);
                    answerButtonTexts[1].transform.parent.gameObject.SetActive(false);
                    answerButtonTexts[2].transform.parent.gameObject.SetActive(false);
                    break;
                case 2:
                    answerButtonTexts[0].transform.parent.gameObject.SetActive(true);
                    answerButtonTexts[1].transform.parent.gameObject.SetActive(true);
                    answerButtonTexts[2].transform.parent.gameObject.SetActive(false);
                    break;
                case 3:
                    answerButtonTexts[0].transform.parent.gameObject.SetActive(true);
                    answerButtonTexts[1].transform.parent.gameObject.SetActive(true);
                    answerButtonTexts[2].transform.parent.gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log($"Maximum count exceeded! {count}");
                    break;
            }
        }

        public void Answer(int answerIndex)
        {
            if (isWriting)
            {
                if (canSkip)
                {
                    complete = true;
                    return;
                }
                else
                {
                    return;
                }
            }

            if (useLocalization)
            {
                string givenAnswer = localizedAnswers[answerIndex].GetLocalizedString();
                NodePort port = currentLocalizedNode.DynamicPorts.ToList<NodePort>()[answerIndex].Connection;

                if (port.node.GetType() == typeof(LocalizedDialogueNode))
                {
                    currentLocalizedNode = port.node as LocalizedDialogueNode;
                    DisplayLocalizedNodeData();
                }
                else
                {
                    StartCoroutine(EndDialogue());
                }
            }
            else
            {
                string givenAnswer = answers[answerIndex];
                NodePort port = currentNode.DynamicPorts.ToList<NodePort>()[answerIndex].Connection;

                if (port.node.GetType() == typeof(DialogueNode))
                {
                    currentNode = port.node as DialogueNode;
                    DisplayNodeData();
                }
                else
                {
                    StartCoroutine(EndDialogue());
                }
            }
        }

        private void DisplayNodeData()
        {
            nameText.SetText(currentNode.talker);

            image.sprite = currentNode.sprite;

            answers = currentNode.answers;
            PrepareAnswer(answers.Count);
            for (int i = 0; i < answers.Count; i++)
            {
                answerButtonTexts[i].SetText(answers[i]);
            }

            StartCoroutine(WriteSentence(currentNode.text));
        }

        private void DisplayLocalizedNodeData()
        {
            nameText.SetText(currentLocalizedNode.talker.GetLocalizedString());

            image.sprite = currentLocalizedNode.sprite;

            localizedAnswers = currentLocalizedNode.answers;
            PrepareAnswer(localizedAnswers.Count);
            for (int i = 0; i < localizedAnswers.Count; i++)
            {
                answerButtonTexts[i].SetText(localizedAnswers[i].GetLocalizedString());
            }

            StartCoroutine(WriteSentence(currentLocalizedNode.text.GetLocalizedString()));
        }

        IEnumerator WriteSentence(string sentence)
        {
            isWriting = true;
            if (!(currentNode.soundName == null || currentNode.soundName == ""))
            {
                audioManager.Play(currentNode.soundName);
            }
            string dialogue = "";
            sentenceText.SetText(dialogue);
            foreach (char letter in sentence.ToCharArray())
            {
                if (!complete)
                {
                    dialogue += letter;
                    sentenceText.SetText(dialogue);
                    yield return new WaitForSeconds(currentNode.typeSpeed);
                }
                else
                {
                    dialogue += letter;
                    sentenceText.SetText(dialogue);
                }
            }
            complete = false;
            isWriting = false;
        }

        IEnumerator StartDialogue()
        {
            dialogueBox.SetActive(true);
            if (useAnimation)
            {
                dialogueBoxAnimator.SetBool(dialogueTriggerBool, true);
                yield return new WaitForSeconds(animationTime);
            }
        }

        IEnumerator EndDialogue()
        {
            if (useAnimation)
            {
                dialogueBoxAnimator.SetBool(dialogueTriggerBool, false);
                yield return new WaitForSeconds(animationTime);
            }
            dialogueBox.SetActive(false);
        }

        public override void OnEnable()
        {
            dialogueManager = this;
        }

        public override void OnDisable()
        {
            dialogueManager = null;
        }
    }
}
