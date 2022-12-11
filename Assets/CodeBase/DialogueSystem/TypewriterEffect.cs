using System.Collections;
using System.Collections.Generic;
using CodeBase.Extensions;
using TMPro;
using UnityEngine;

namespace CodeBase.DialogueSystem
{
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField] private float _typewriterSpeed = 50f;
        
        public bool IsRunning { get; private set; }
        
        private readonly List<Punctuation> _punctuations = new()
        {
            new Punctuation(new HashSet<char> { '.', '!', '?' }, 0.6f),
            new Punctuation(new HashSet<char> { ',', ';', ':' }, 0.3f),
        };

        private Coroutine _typingCoroutine;

        public void  Run(string textToType, TMP_Text textLabel) => 
            _typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));

        public void Stop()
        {
            StopCoroutine(_typingCoroutine);
            IsRunning = false;
        }
        
        private IEnumerator TypeText(string textToType, TMP_Text textLabel)
        {
            IsRunning = true;
            textLabel.Clear();
            
            var time = 0f;
            var charIndex = 0;

            while (charIndex < textToType.Length)
            {
                var lastCharIndex = charIndex;
                
                time += _typewriterSpeed * Time.deltaTime;
                charIndex = Mathf.FloorToInt(time);
                charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

                for (var i = lastCharIndex; i < charIndex; i++)
                {
                    var isLast = i >= textToType.Length - 1;
                    textLabel.text = textToType.Substring(0, i + 1);
                    if (IsPunctuation(textToType[i], out var waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                        yield return new WaitForSeconds(waitTime);
                }
                
                yield return null;
            }
            
            IsRunning = true;
        }

        private bool IsPunctuation(char character, out float waitTime)
        {
            foreach (var punctuationCategory in _punctuations)
            {
                if (punctuationCategory.Punctuations.Contains(character))
                {
                    waitTime = punctuationCategory.WaitTime;
                    return true;
                }
            }

            waitTime = default;
            return false;
        }
    }

    public readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}