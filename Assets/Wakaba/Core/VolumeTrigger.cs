using UnityEngine;
namespace Wakaba
{
    [RequireComponent(typeof(Rigidbody))]
    public class VolumeTrigger : Trigger
    {
        private void Start()
        {
            // Validate that there is a collider, if non present, add a BoxCollider.
            Collider collider = gameObject.GetComponent<Collider>();
            if (collider == null) collider = gameObject.AddComponent<BoxCollider>();

            // Force the collider to be a trigger.
            collider.isTrigger = true;

            // Prevent this rigidbody from moving at all using physics.
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void OnTriggerEnter(Collider _collider) => Fire();
    }
}