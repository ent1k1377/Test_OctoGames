using System.Collections.Generic;
using CodeBase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.DialogueSystem
{
    [RequireComponent(typeof(DialogueUI))]
    public class ResponseHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform _responseBox;
        [SerializeField] private RectTransform _responseContainer;
        [SerializeField] private RectTransform _responseButtonTemplate;
        
        private DialogueUI _dialogueUI;
        private readonly List<GameObject> _tempResponseButtons = new();
        private ResponseEvent[] _responseEvents;

        private void Awake() => 
            _dialogueUI = GetComponent<DialogueUI>();

        public void AddResponseEvents(ResponseEvent[] responseEvents) => 
            _responseEvents = responseEvents;

        public void ShowResponses(Response[] responses)
        {
            var responseBoxHeight = 0f;

            for (var i = 0; i < responses.Length; i++)
            {
                var response = responses[i];
                var responseIndex = i;
                
                var responseButton = Instantiate(_responseButtonTemplate.gameObject, _responseContainer);
                responseButton.Activate();
                responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
                responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));
                _tempResponseButtons.Add(responseButton);
                
                responseBoxHeight += _responseButtonTemplate.sizeDelta.y;
            }

            _responseBox.sizeDelta = new Vector2(_responseBox.sizeDelta.x, responseBoxHeight);
            _responseBox.gameObject.Activate();
        }

        private void OnPickedResponse(Response response, int responseIndex)
        {
            _responseBox.gameObject.Deactivate();

            foreach (var responseButton in _tempResponseButtons)
                Destroy(responseButton);
            
            _tempResponseButtons.Clear();
            
            if (_responseEvents != null && responseIndex <= _responseEvents.Length)
                _responseEvents[responseIndex].OnPickedResponse?.Invoke();

            _responseEvents = null;

            if (response.DialogueObject)
                _dialogueUI.ShowDialogue(response.DialogueObject);
            else
                _dialogueUI.CloseDialogueBox();
            
        }
    }
}