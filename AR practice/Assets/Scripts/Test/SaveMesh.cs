using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveMesh : MonoBehaviour
{
    public string fileName;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Save();
        }
    }

    void Save()
    {
        Mesh myMesh = Instantiate<Mesh>(GetComponent<MeshFilter>().sharedMesh);
        string savePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Scenes/" + fileName + ".asset");
        print(savePath);

        AssetDatabase.CreateAsset(myMesh, savePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
