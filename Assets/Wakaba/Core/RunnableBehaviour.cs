using UnityEngine;
using InvalidOperationException = System.InvalidOperationException;
namespace Wakaba
{
    // C: controls Update() and Start()
    public abstract class RunnableBehaviour : MonoBehaviour, IRunnable
    {
        public bool Enabled { get; set; }

        private bool isSetup = false;

        public void Run(params object[] _params)
        {
            // If the runnable is enabled, run its OnRun function with the passed values.
            if (Enabled) OnRun(_params);
        }

        public void Setup(params object[] _params)
        {
            // If the runnable is already setup, throw an exception.
            if (isSetup) throw new InvalidOperationException("Runnable already setup.");

            // Run the OnSetup function, and flag this as setup.
            OnSetup(_params);
            isSetup = true;
        }

        protected abstract void OnRun(params object[] _params);
        protected abstract void OnSetup(params object[] _params);
    }
}