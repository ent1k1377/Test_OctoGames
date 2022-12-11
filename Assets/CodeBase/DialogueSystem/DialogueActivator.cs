using UnityEngine;

namespace CodeBase.DialogueSystem
{
    public class DialogueActivator : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueObject _dialogueObject;

        public void UpdateDialogueObject(DialogueObject dialogueObject) =>
            _dialogueObject = dialogueObject;

        public void Interact(Player.Player player)
        {
            foreach (var responseEvent in GetComponents<DialogueResponseEvents>())
            {
                if (responseEvent.DialogueObject != _dialogueObject) 
                    continue;
                player.DialogueUI.AddResponseEvents(responseEvent.Events);
                break;
            }
            
            player.DialogueUI.ShowDialogue(_dialogueObject);
        }
    }
}