using UnityEngine;
using System.Collections;
using System;

namespace BattleOfHeroes.Showcase.Helpers
{
    public class CoroutineHelper : MonoBehaviour
    {
        // Use this for initialization
        private static CoroutineHelper _singleton;

        public static CoroutineHelper Singleton
        {
            get
            {
                if (_singleton is null)
                {
                    var helper = FindObjectOfType<CoroutineHelper>();
                    if (helper is null)
                    {
                        var go = new GameObject(nameof(CoroutineHelper));
                        return go.AddComponent<CoroutineHelper>();
                    }
                    return helper;
                }
                else
                {
                    return _singleton;
                }

            }

        }

        private void Awake()
        {
            if (_singleton == null)
            {
                _singleton = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void StartRoutine(IEnumerator callback)
        {
            StartCoroutine(callback);
        }

    }
}
