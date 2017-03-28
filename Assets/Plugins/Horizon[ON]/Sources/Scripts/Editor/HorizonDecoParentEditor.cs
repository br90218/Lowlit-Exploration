using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Horizon
{
    [CustomEditor(typeof(HorizonDecoParent))]
    public class HorizonDecoParentEditor : Editor
    {
        public HorizonDecoParent hdp;

        string installPath;
        string inspectorGUIPath;
        int scrollBarWidth = 36;

        void OnEnable()
        {
            string scriptLocation = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
            installPath = scriptLocation.Replace("/Sources/Scripts/Editor/HorizonDecoParentEditor.cs", "");
            inspectorGUIPath = installPath + "/Sources/Scripts/Editor/InspectorGUI";
        }

        public override void OnInspectorGUI()
        {
            hdp = (HorizonDecoParent)target;

            Rect bgRect = EditorGUILayout.GetControlRect();
            bgRect = new Rect(bgRect.x + 1, bgRect.y - 18, Screen.width - 40, bgRect.height + 1);

            Texture2D bgTex;
            Texture2D logoTex = AssetDatabase.LoadAssetAtPath(inspectorGUIPath + "/Horizon[ON]Inspector_Logo.png", typeof(Texture2D)) as Texture2D;
            if (EditorGUIUtility.isProSkin) { bgTex = AssetDatabase.LoadAssetAtPath(inspectorGUIPath + "/Horizon[ON]Inspector_bgTex_DarkSkin.jpg", typeof(Texture2D)) as Texture2D; }
            else { bgTex = AssetDatabase.LoadAssetAtPath(inspectorGUIPath + "/Horizon[ON]Inspector_bgTex_LightSkin.jpg", typeof(Texture2D)) as Texture2D; }
            EditorGUI.DrawPreviewTexture(bgRect, bgTex);
            GUI.DrawTexture(new Rect((Screen.width / 2) - 110, bgRect.y + 7, 210, 36), logoTex);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            string message;
            if (hdp.objectMode) message = "This is a Deco Painter Object Group, it stores all the neccesary values to convert this group back into editmode. If you are sure you dont need to edit this group anymore, you can remove this component to free up some memory.";
            else message = "This is a Deco Painter Tree Group, it stores all the neccesary values to convert this group back into editmode. If you are sure you dont need to edit this group anymore, you can remove this component to free up some memory.";
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(Screen.width - scrollBarWidth), GUILayout.Height(82));
            EditorGUI.HelpBox(rect, message, MessageType.Info);
        }
    }
}
