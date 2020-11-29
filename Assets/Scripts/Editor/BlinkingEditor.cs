using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BlinkingObject)), CanEditMultipleObjects]
    public class BlinkingEditor : UnityEditor.Editor
    {
        private BlinkingObject b;
        private void OnEnable()
        {
            b = (BlinkingObject) target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Blink"))
            {
                b.Blink();
            }
            base.OnInspectorGUI();
        }
    }
}