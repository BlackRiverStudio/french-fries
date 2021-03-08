using UnityEngine;
using InvalidOperationException = System.InvalidOperationException;
using NullReferenceException = System.NullReferenceException;
namespace Wakaba.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        // Has the mobile input system been initialised?
        public static bool Initialised => instance != null;

        // Singleton reference instance.
        private static MobileInput instance = null;

        /// <summary>If the system isn't already set-up, this will instansiate the Mobile Input Prefab and assign the static references.</summary>
        public static void Initialise()
        {
            // If the Mobile Input Prefab is already initialised, thow an Exception to tell the user they done the dumb.
            if (Initialised) throw new InvalidOperationException("Mobile Input already initialised!");

            // Load the Mobile Input Prefab and instantiate it, setting the instance.
            MobileInput prefabInstance = Resources.Load<MobileInput>("MobileInputPrefab");
            instance = Instantiate(prefabInstance);

            if (SystemInfo.supportsGyroscope) Input.gyro.enabled = true;

            // Change the instantiated object's name and mark it to not be destroyed.
            instance.gameObject.name = "Mobile Input";
            DontDestroyOnLoad(instance.gameObject);
        }

        #region Swipe
        [SerializeField] private SwipeInput swipeInput;
        /// <summary>Attempts to retrieve the relevent swipe information relating to the passed I.D..</summary>
        /// <param name="_index">The fingerId we are attempting to get the swipe for.</param>
        /// <returns>The corresponding swipe if it exists, otherwise null.</returns>
        public static SwipeInput.Swipe GetSwipe(int _index)
        {
            // If the Mobile Input Prefab isn't initialised. throw an InvalidOperationException.
            if (!Initialised) throw new InvalidOperationException("Mobile Input not initialised.");

            // If the swipe input module isn't set, thow a NullReferenceException.
            if (instance.swipeInput == null) throw new NullReferenceException("Swipe Input reference not set.");

            // Retrieve the swipe for this index from the swipe input manager.
            return instance.swipeInput.GetSwipe(_index);
        }

        public static void GetFlickData(out float _flickPower, out Vector2 _flickDirection)
        {
            // If the Mobile Input Prefab isn't initialised. throw an InvalidOperationException.
            if (!Initialised) throw new InvalidOperationException("Mobile Input not initialised.");

            // If the swipe input module isn't set, thow a NullReferenceException.
            if (instance.swipeInput == null) throw new NullReferenceException("Swipe Input reference not set.");

            // Set the out parameters to their corresponding values in the swipe input class.
            _flickPower = instance.swipeInput.FlickPower;
            _flickDirection = instance.swipeInput.FlickDirection;
        }
        #endregion

        #region Joystick
        [SerializeField] private JoystickInput joystickInput;
        /// <summary>Returns the value of the joystick from the joystick module if it is valid.</summary>
        /// <param name="_axis">The axis to get the input from, Horizontal = x; Vertical = y</param>
        public static float GetJoystickAxis(JoystickAxis _axis)
        {
            // If the Mobile Input Prefab isn't initialised. throw an InvalidOperationException.
            if (!Initialised) throw new InvalidOperationException("Mobile Input not initialised.");

            // If the joystic input module isn't set, thow a NullReferenceException.
            if (instance.joystickInput == null) throw new NullReferenceException("Joystick Input reference not set.");

            // Switch on the passed axis and return the appropriate value.
            return _axis switch
            {
                JoystickAxis.Horizontal => instance.joystickInput.Axis.x,
                JoystickAxis.Vertical => instance.joystickInput.Axis.y,
                _ => 0,
            };
        }
        #endregion
    }
}