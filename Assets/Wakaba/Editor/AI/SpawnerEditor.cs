using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
namespace Wakaba.AI
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        private Spawner spawner;

        private SerializedProperty pSize;
        private SerializedProperty pCentre;

        private SerializedProperty pFloorYPosition;
        private SerializedProperty pSpawnRate;

        private SerializedProperty pShouldSpawnBoss;
        private SerializedProperty pBossSpawnChance;
        private SerializedProperty pBossPrefab;
        private SerializedProperty pEnemyPrefab;

        private AnimBool shouldSpawnBoss = new AnimBool();

        private BoxBoundsHandle handle;

        private void OnEnable()
        {
            spawner = target as Spawner;

            pSize = serializedObject.FindProperty("size");
            pCentre = serializedObject.FindProperty("centre");

            pFloorYPosition = serializedObject.FindProperty("floorYPosition");
            pSpawnRate = serializedObject.FindProperty("spawnRate");
            
            pShouldSpawnBoss = serializedObject.FindProperty("shouldSpawnBoss");
            pBossSpawnChance = serializedObject.FindProperty("bossSpawnChance");
            pBossPrefab = serializedObject.FindProperty("bossPrefab");
            pEnemyPrefab = serializedObject.FindProperty("enemyPrefab");

            shouldSpawnBoss.value = pShouldSpawnBoss.boolValue;
            shouldSpawnBoss.valueChanged.AddListener(Repaint);

            handle = new BoxBoundsHandle();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Create a vertical layout group visualised as a box.
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                // Draw the centre and size properties, exactly as Unity would.
                EditorGUILayout.PropertyField(pCentre);
                EditorGUILayout.PropertyField(pSize);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pFloorYPosition);

                // Cache the original value of spawn rate and create the label.
                Vector2 spawnRate = pSpawnRate.vector2Value;
                string label = $"Spawn Rate Bounds ({spawnRate.x.ToString("0.00")}s - {spawnRate.y.ToString("0.00")}s)";

                // Render the spawn rate as a min max slider and reset the Vector2 value.
                EditorGUILayout.MinMaxSlider(label, ref spawnRate.x, ref spawnRate.y, 0, 3);
                pSpawnRate.vector2Value = spawnRate;

                // Applying spacing between the lines.
                EditorGUILayout.Space();

                // Render the enemyPrefab and shouldSpawnBoss as normal.
                EditorGUILayout.PropertyField(pEnemyPrefab);
                EditorGUILayout.PropertyField(pShouldSpawnBoss);

                // Attempt to fade the next variables in and out.
                shouldSpawnBoss.target = pShouldSpawnBoss.boolValue;
                if (EditorGUILayout.BeginFadeGroup(shouldSpawnBoss.faded))
                {
                    // Only visable when 'pShouldSpawnBoss' is true.
                    EditorGUI.indentLevel++;

                    // Draw boss spawn chance and boss prefab as normal.
                    EditorGUILayout.PropertyField(pBossSpawnChance);
                    EditorGUILayout.PropertyField(pBossPrefab);

                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            // Make the handle's colour green.
            Handles.color = Color.green;
            
            // Store the default matrix.
            Matrix4x4 baseMatrix = Handles.matrix;

            // Make the handles use the object(s)'s matrix.
            Matrix4x4 rotationMatrix = spawner.transform.localToWorldMatrix;
            Handles.matrix = rotationMatrix;

            // Setup the box bounds handle with the spawner's values.
            handle.center = spawner.centre;
            handle.size = spawner.size;

            // Begin listening for changes to the handle, then draw it.
            EditorGUI.BeginChangeCheck();
            handle.DrawHandle();

            // Check if any changes were detected.
            if (EditorGUI.EndChangeCheck())
            {
                // Reset the spawner values to the new handle values.
                spawner.size = handle.size;
                spawner.centre = handle.center;
            }

            // Reset the hande's matrix back to default.
            Handles.matrix = baseMatrix;
        }
    }
}