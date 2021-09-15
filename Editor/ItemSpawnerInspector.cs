using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace ItemSpawner.Editor
{
    [CustomEditor(typeof(ItemSpawn))]
    public class ItemSpawnInspector : UnityEditor.Editor
    {
        private ItemSpawn _itemSpawn;

        private void OnEnable()
        {_itemSpawn = (ItemSpawn)target;} //selecciona la database actual

        public override void OnInspectorGUI()
        {
            
            //Definicion de estilo de etiqueta
            var tagStyle = new GUIStyle(GUI.skin.label);
            tagStyle.wordWrap = true;
            tagStyle.alignment = TextAnchor.MiddleLeft;
            tagStyle.fixedWidth = 100;

            //Definicion de estilo de etiqueta
            var labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.wordWrap = true;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            
            if (!_itemSpawn) return;
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.BeginHorizontal();//Organizacion Horizontal
            GUILayout.Label("Mobs creados: ", tagStyle); GUILayout.Label(_itemSpawn.itemsSpawned.ToString(CultureInfo.InvariantCulture), labelStyle); //Conteo de Mobs creados
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Mobs en escena: ", tagStyle); GUILayout.Label( _itemSpawn.itemsOnScene.ToString(CultureInfo.InvariantCulture), labelStyle); //Conteo de Mobs creados
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            DrawDefaultInspector(); //Dibujo de inspector por default

        }

    }
}

