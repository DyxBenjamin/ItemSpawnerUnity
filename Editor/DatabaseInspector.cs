using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace ItemSpawner.Editor
{
    [CustomEditor(typeof(Database))]
    public class DatabaseInspector : UnityEditor.Editor
    {
        private Database _dataBase;
        private string _searchString;
        private bool _shouldSearch;

        private void OnEnable()
        {_dataBase = (Database)target;} //selecciona la database actual

        public override void OnInspectorGUI()
        {
            //base.DrawDefaultInspector(); dibujo de inspector por default
            if (!_dataBase) return;
            
            //Conteo de items
            EditorGUILayout.BeginHorizontal("box");
            GUILayout.Label("Items guardados: " + _dataBase.items.Count);
            EditorGUILayout.EndHorizontal();

            //Caja de busqueda
            if (_dataBase.items.Count > 0) {
                EditorGUILayout.BeginHorizontal("box");
                GUILayout.Label("Search: "); _searchString = GUILayout.TextField(_searchString);
                EditorGUILayout.EndHorizontal(); }

            if (GUILayout.Button("Nuevo item")){ItemWindow.ShowEmptyWindow(_dataBase); //boton de nuevo item
            }

            //busqueda de items
            _shouldSearch = string.IsNullOrEmpty(_searchString) != true;

            foreach (var item in _dataBase.items)
            {if (_shouldSearch){
                    if (item.name == _searchString || item.name.Contains(_searchString) || item.id.ToString() == _searchString)
                    {DisplayItem(item);}}
                else{DisplayItem(item);}}

            if (_deletedItem != null) {_dataBase.items.Remove(_deletedItem);} //Eliminar item
        }

        private Database.Item _deletedItem;

        public void DisplayItem(Database.Item item){
            
            //Definicion de estilo de etiqueta
            var tagStyle = new GUIStyle(GUI.skin.label);
            tagStyle.wordWrap = true;
            tagStyle.alignment = TextAnchor.MiddleLeft;
            tagStyle.fixedWidth = 100;

            //Definicion de estilo de etiqueta
            var labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.wordWrap = true;
            labelStyle.alignment = TextAnchor.MiddleLeft;
         
            //Definicion de estilo de etiqueta de referencias
            var valueStyle = new GUIStyle(GUI.skin.label);
            valueStyle.wordWrap = true;
            valueStyle.alignment = TextAnchor.MiddleLeft;
            //valueStyle.margin = new RectOffset(0 ,50 ,0 ,0);
            
            //Definicion de estilo de botones
            var butonStyle = new GUIStyle(GUI.skin.button);
            butonStyle.wordWrap = true;
            butonStyle.alignment = TextAnchor.MiddleCenter;
            butonStyle.margin = new RectOffset(0 ,0 ,5 ,5);
            EditorGUILayout.BeginVertical("box"); //Organizacion Vertical, "Box" dibuja una caja

            //Campo de id
            EditorGUILayout.BeginHorizontal();//Organizacion Horizontal
            GUILayout.Label("id: ", tagStyle); GUILayout.Label(item.id.ToString(), valueStyle);
            EditorGUILayout.EndHorizontal();

            //Campo de nombre
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Nombre: ", tagStyle); GUILayout.Label(item.name, labelStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        
            //Campo de descripcion
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Descripcion: ", tagStyle); GUILayout.Label(item.description, labelStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        
            // Campo de objeto
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab: ", tagStyle); GUILayout.Label(item.prefab.ToString(), labelStyle);
            EditorGUILayout.EndHorizontal(); 
        
            //Campo de probabilidad
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Probabilidad: ", tagStyle); GUILayout.Label(item.probability.ToString(CultureInfo.CurrentCulture), valueStyle);
            EditorGUILayout.EndHorizontal();
        
            EditorGUILayout.Space();
        
            //Botones de accion
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Editar", butonStyle)){ItemEditWindow.ShowItemWindow(_dataBase, item);} //Boton de edicion  
            if (GUILayout.Button("Borrar", butonStyle)){_deletedItem = item;} //Boton de borrado
            else {_deletedItem = null;}
            if (GUILayout.Button("Duplicar", butonStyle)){ItemEditWindow.ShowItemWindow(_dataBase, item);}
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();

        }
    }
}
