using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EditorTrix : MonoBehaviour
{
    [System.Serializable]
    public struct Data
    {
        public float Value1;
        public float Value2;
    }

    public bool OnOff;

    public List<Data> DataLsit;

#if UNITY_EDITOR
    [MenuItem("Tools/Change prefab value")]
    static void ApplyThing()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            var obj = Selection.gameObjects[i];
            if (obj.GetComponent<EditorTrix>() == null)
                continue;
            
            var prefabPath = AssetDatabase.GetAssetPath(obj);
            var openPrefab = PrefabUtility.LoadPrefabContents(prefabPath);
            var comp = openPrefab.GetComponent<EditorTrix>();

            if (comp.OnOff && comp.transform.childCount == 0)
            {
                var child = GameObject.CreatePrimitive(PrimitiveType.Cube);
                child.transform.SetParent(openPrefab.transform, false);
                child.transform.localPosition = new Vector3(0, 2, 1);
                
                PrefabUtility.SaveAsPrefabAsset(openPrefab, prefabPath);
                PrefabUtility.UnloadPrefabContents(openPrefab);
            }
        }
    }
#endif
}
