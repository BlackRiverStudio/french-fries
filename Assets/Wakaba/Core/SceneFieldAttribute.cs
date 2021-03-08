using UnityEngine;
namespace Wakaba
{
    public class SceneFieldAttribute : PropertyAttribute
    {
        /// <summary>Converts a full path to a SceneManager friendly one for scene loading.</summary>
        public static string LoadableName(string _path)
        {
            // The pieces of the path we are looking to ignore.
            string start = "Assets/";
            string end = ".unity";
            
            // Test if the path contains 'start' data, if so, remove it.
            if (_path.StartsWith(start)) _path = _path.Substring(start.Length);

            // Test if the path contains 'end' data, if so, remove it.
            if (_path.EndsWith(end)) _path = _path.Substring(0, _path.LastIndexOf(end));

            // Return the newly edited string.
            return _path;
        }
    }
}