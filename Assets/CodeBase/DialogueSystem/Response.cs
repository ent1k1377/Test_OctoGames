﻿using System;
using UnityEngine;

namespace CodeBase.DialogueSystem
{
    [Serializable]
    public class Response
    {
        [SerializeField] private string _responseText;
        [SerializeField] private DialogueObject _dialogueObject;

        public string ResponseText => _responseText;
        public DialogueObject DialogueObject => _dialogueObject;
    }
}