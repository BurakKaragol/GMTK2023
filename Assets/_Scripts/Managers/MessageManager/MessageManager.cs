using UnityEngine;
using System.Collections.Generic;
using MrLule.ExtensionMethods;

namespace MrLule.Managers.MessageMan
{
    public class MessageManager : Manager
    {
        [SerializeField] private Transform startPosition;
        [SerializeField] private float space = 0f;
        [SerializeField] private GameObject messagePrefab;

        private List<MessagePanel> messages = new List<MessagePanel>();

        public void NewMessage(string title, string content, Sprite sprite = null)
        {
            Vector3 nextPosition = (messages.Count == 0 ? startPosition.position : messages[messages.Count - 1].GetNextPosition()).SetX(startPosition.position.x);
            GameObject newMessage = Instantiate(messagePrefab, nextPosition, Quaternion.identity, transform);
            MessagePanel messagePanel = newMessage.GetComponent<MessagePanel>();
            messages.Add(messagePanel);
            messagePanel.Initialize(title, content, sprite);
            messagePanel.ShowMessage();
        }

        public void CloseMessage(MessagePanel message)
        {
            messages.Remove(message);
            Vector3 position = startPosition.position.SetX(200);
            for (int i = 0; i < messages.Count; i++)
            {
                messages[i].SetTargetPosition(position);
                position += messages[i].GetHeight() * Vector3.down;
                position += space * Vector3.down;
            }
        }

        public override void OnEnable()
        {
            messageManager = this;
        }

        public override void OnDisable()
        {
            messageManager = null;
        }
    }
}
