using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Trent
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AgentFactoryBehaviour))]
    public class InspectorAgentFactory : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mytarget = target as AgentFactoryBehaviour;

            GUILayout.Space(40);
            if (GUILayout.Button("ADD"))
            {
                mytarget.Create(mytarget.SpawnPosition);
            }

            GUILayout.Space(10);
            if (GUILayout.Button("ADD RANGE"))
            {
                mytarget.Create(mytarget.SpawnCount);
            }

            GUILayout.Space(10);
            if (GUILayout.Button("REMOVE ALL"))
            {
                mytarget.DestroyAll();
            }
        }
    }
#endif
}