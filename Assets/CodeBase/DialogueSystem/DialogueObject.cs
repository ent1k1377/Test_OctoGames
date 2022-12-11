using UnityEngine;

namespace CodeBase.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueObject", menuName = "Dialogue/DialogueObject")]
    public class DialogueObject : ScriptableObject
    {
        [SerializeField] [TextArea] private string[] _dialogue;
        [SerializeField] private Response[] _responses;

        public string[] Dialogue => _dialogue;
        public Response[] Responses => _responses;
        public bool HasResponses => Responses != null && Responses.Length > 0;
    }
}