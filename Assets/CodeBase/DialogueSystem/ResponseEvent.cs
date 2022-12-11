using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.DialogueSystem
{
    [Serializable]
    public class ResponseEvent
    {
        [HideInInspector] public string Name;
        [SerializeField] private UnityEvent _onPickedResponse;

        public UnityEvent OnPickedResponse => _onPickedResponse;
    }
}