using System;
using UnityEngine;

namespace CodeBase.DialogueSystem
{
    public class DialogueResponseEvents : MonoBehaviour
    {
        [SerializeField] private DialogueObject _dialogueObject;
        [SerializeField] private ResponseEvent[] _events;

        public DialogueObject DialogueObject => _dialogueObject;
        public ResponseEvent[] Events => _events;

        public void OnValidate()
        {
            if (_dialogueObject == null)
                return;
            if (_dialogueObject.Responses == null)
                return;
            if (_events != null && _events.Length == _dialogueObject.Responses.Length)
                return;

            if (_events == null)
                _events = new ResponseEvent[_dialogueObject.Responses.Length];
            else
                Array.Resize(ref _events, _dialogueObject.Responses.Length);

            for (var i = 0; i < _dialogueObject.Responses.Length; i++)
            {
                var response = _dialogueObject.Responses[i];

                if (_events[i] != null)
                {
                    _events[i].Name = response.ResponseText;
                    continue;
                }

                _events[i] = new ResponseEvent { Name = response.ResponseText };
            }
        }
    }
}