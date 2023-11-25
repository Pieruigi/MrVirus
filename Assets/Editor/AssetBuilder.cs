using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Virus.Scriptables;

namespace Virus.Editor
{
    public class AssetBuilder : MonoBehaviour
    {

        [MenuItem("Assets/Create/Virus/Floors")]
        public static void CreateFloorAsset()
        {
            FloorAsset asset = ScriptableObject.CreateInstance<FloorAsset>();

            string name = "Floor.asset";

            string folder = FloorAsset.ResourceFolder;

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }

}
