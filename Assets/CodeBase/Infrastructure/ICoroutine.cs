using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface ICoroutine
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}