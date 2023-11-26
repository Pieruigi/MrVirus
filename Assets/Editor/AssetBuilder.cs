using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Virus.Scriptables;

namespace Virus.Editor
{
    public class AssetBuilder : MonoBehaviour
    {

        [MenuItem("Assets/Create/Virus/Floor")]
        public static void CreateFloorAsset()
        {
            FloorAsset asset = ScriptableObject.CreateInstance<FloorAsset>();

            string name = "Floor.asset";

            string folder = System.IO.Path.Combine("Assets/Resources", FloorAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Create/Virus/Elevator")]
        public static void CreateElevatorAsset()
        {
            ElevatorAsset asset = ScriptableObject.CreateInstance<ElevatorAsset>();

            string name = "Elevator.asset";

            string folder = System.IO.Path.Combine("Assets/Resources", ElevatorAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }

}
