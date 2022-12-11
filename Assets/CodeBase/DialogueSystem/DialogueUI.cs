using System.Collections;
using CodeBase.Extensions;
using CodeBase.GeneralSystems;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.DialogueSystem
{
    [RequireComponent(typeof(TypewriterEffect), typeof(ResponseHandler))]
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueBox;
        [SerializeField] private TMP_Text _textLabel;
        [SerializeField] private DialogueObject _dialogueObject;
        
        public bool IsOpen { get; private set; }
        
        [Inject] private CursorSystem _cursor;
        private TypewriterEffect _typewriterEffect;
        private ResponseHandler _responseHandler;

        private void Awake()
        {
            _typewriterEffect = GetComponent<TypewriterEffect>();
            _responseHandler = GetComponent<ResponseHandler>();
        }

        private void Start() => 
            CloseDialogueBox();

        public void ShowDialogue(DialogueObject dialogueObject)
        {
            IsOpen = true;
            _cursor.ActivateDialogueMode();
            _dialogueBox.Activate();
            StartCoroutine(StepThroughDialogue(dialogueObject));
        }

        public void AddResponseEvents(ResponseEvent[] responseEvents) =>
            _responseHandler.AddResponseEvents(responseEvents);
        
        private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
        {
            for (var i = 0; i < dialogueObject.Dialogue.Length; i++)
            {
                var dialogue = dialogueObject.Dialogue[i];
                
                yield return RunTypingEffect(dialogue);

                _textLabel.text = dialogue;
                
                if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
                    break;
                
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }

            if (dialogueObject.HasResponses)
                _responseHandler.ShowResponses(dialogueObject.Responses);
            else
                CloseDialogueBox();
        }

        private IEnumerator RunTypingEffect(string dialogue)
        {
            _typewriterEffect.Run(dialogue, _textLabel);

            while (_typewriterEffect.IsRunning)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Space))
                    _typewriterEffect.Stop();   
            }
        }

        public void CloseDialogueBox()
        {
            IsOpen = false;
            _cursor.ActivateGameMode();
            _dialogueBox.Deactivate();
            _textLabel.Clear();
        }
    }
}
