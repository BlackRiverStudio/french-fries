using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Wakaba
{
    // Abstract is because this particular class does nothing.
    public abstract class Trigger : MonoBehaviour
    {
        public UnityEvent OnTriggered { get => onTriggered; set => onTriggered = value; }

        [SerializeField, Min(0)] protected float delay = 0;
        [SerializeField] protected bool oneTimeUse = false;
        [SerializeField] protected UnityEvent onTriggered = new UnityEvent();

        private bool triggered = false;

        public void Fire()
        {
            // If the trigger can only run once and has already run, ignore this fire.
            if (triggered && oneTimeUse) return;

            triggered = true;
            StartCoroutine(Trigger_CR());
        }

        private IEnumerator Trigger_CR()
        {
            yield return new WaitForSeconds(delay);
            onTriggered.Invoke();
        }
    }
}