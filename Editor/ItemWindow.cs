using UnityEditor;
using UnityEngine;

namespace ItemSpawner.Editor
{
    public class ItemWindow : EditorWindow
    {
        private static EditorWindow window;
        private static Database database;
        private static Database.Item newItem;
        private GUILayoutOption[] options = {GUILayout.MaxWidth(150.0f), GUILayout.MinWidth(150.0f) };
        private static int counter = 0;

        public static void ShowEmptyWindow(Database db) {
            database = db;
            window = GetWindow<ItemWindow>();
            window.maxSize = new Vector2(300, 350);
            window.minSize = new Vector2(300, 350);
            newItem = new Database.Item();
            counter = database.Items.Count;
        }
        public void OnGUI()
        { DisplayItem(newItem);
            if (GUILayout.Button("Nuevo item"))
            {
                addItem();
            }
            if (GUILayout.Button("Cancelar"))
            {
                window.Close();
            }
        }

        private void addItem() {
            Undo.RecordObject(database, "Item add");

            database.Items.Add(newItem);
            EditorUtility.SetDirty(database);
            window.Close();

        }


        public void DisplayItem(Database.Item item)
        {

            GUIStyle TextAreaStyle = new GUIStyle(GUI.skin.textArea);
            TextAreaStyle.wordWrap = true;

            GUIStyle ValueStyle = new GUIStyle(GUI.skin.label);
            ValueStyle.wordWrap = true;
            ValueStyle.alignment = TextAnchor.MiddleLeft;
            ValueStyle.fixedWidth = 50;
            ValueStyle.margin = new RectOffset(0, 50, 0, 0);

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("id: ");
            item.id = counter;
            GUILayout.Label((counter).ToString(), ValueStyle);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Nombre: ");
            item.Nombre = GUILayout.TextField(item.Nombre, TextAreaStyle,options);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab: ");
            item.prefab = EditorGUILayout.ObjectField(item.prefab, typeof(GameObject),false,options);
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Descripcion: ");
            item.Descripcion = EditorGUILayout.TextArea(item.Descripcion, TextAreaStyle, GUILayout.MinHeight(100));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Probabilidad: ");
            item.Probabilidad = EditorGUILayout.FloatField(item.Probabilidad,TextAreaStyle, options);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

        }

    }
}

