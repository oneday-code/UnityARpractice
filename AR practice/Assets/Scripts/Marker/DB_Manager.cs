using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;
//using Random = UnityEngine.Random;


public class DB_Manager : MonoBehaviour
{
    public string db_Url = "https://unitydbproject-default-rtdb.firebaseio.com/";

    public SaveData result;
    public bool isCreate = false;
    public static DB_Manager db;

    string prefabName;
    Vector2 savedLocation;
    public bool searchComplete = false;
    public Text log;
    private void Awake()
    {
        if (db == null)
        {
            db = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        // DB�� �ּҸ� �⺻ �ּ� ������ �����Ѵ�.
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(db_Url);

        //SaveDB();
    }
    void SaveDB()
    {
        // Ŭ���� �ν��Ͻ� ����
        SaveData data1 = new SaveData("Cat", 37.123f, 125.456f, false);
        SaveData data2 = new SaveData("Car", 37.789f, 129.987f, false);

        // Ŭ���� �ν��Ͻ��� Json �������� ��ȯ�Ѵ�.
        string jData1 = JsonUtility.ToJson(data1);
        //print(jData1);
        string jData2 = JsonUtility.ToJson(data2);

        // DB�� �ֻ�� ��Ʈ�� ã�´�.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        // �ֻ�� ��Ʈ ������ Json ������ ���� �߰� or �����Ѵ�.
        reference.Child("Marker").Child("Data1").SetRawJsonValueAsync(jData1);
        reference.Child("Marker").Child("Data2").SetRawJsonValueAsync(jData2);
        reference.Child("Marker").Child("Test").SetValueAsync("�����߽��ϴ�.");

        print("DB�� ���� �Ϸ�!");
    }

    // DB���� �����͸� �о���� �Լ�
    public IEnumerator LoadDB(string imageName, Transform TrackedImage)
    {
        // DB���� �о������ ��Ʈ�� ã�´�.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Marker");

        prefabName = imageName;

        // DB���� �˻� �� �� �о����
        reference.GetValueAsync().ContinueWith(LoadFunc);

        #region
        //�ش� ��Ʈ�� ���� json �������� �о�´�.
        //reference.GetValueAsync().ContinueWith((myTask) =>
        //{
        //    if (myTask.IsCanceled || myTask.IsFaulted)
        //    {
        //        print("�����͸� �дµ� �����߽��ϴ�.");
        //    }
        //    else if (myTask.IsCompleted)
        //    {
        //        // �о�� DB�� �����͸� ������ �����Ѵ�.
        //        DataSnapshot allSnapshot = myTask.Result;

        //        foreach (DataSnapshot snapshot in allSnapshot.Children)
        //        {
        //            string readData = snapshot.GetRawJsonValue();
        //            SaveData parseData = JsonUtility.FromJson<SaveData>(readData);

        //            // �о�� �������� name ���� "Cat"�̸鼭, ���������� ��ȹ���� ���� �����...
        //            if (parseData.name == imageName && parseData.captureState == false)
        //            {
        //                savedLocation = new Vector2(parseData.latitude, parseData.longitude);
        //                break;
        //            }
        //        }

        //        searchComplete = true;
        //    }
        //});
        #endregion

        // �˻��� �Ϸ�� ������ ����Ѵ�.
        while (!searchComplete)
        {
            yield return null;
        }

        // ���� ��ġ�� �˻��� ��ġ�� ���Ѵ�.
        if (savedLocation != Vector2.zero)
        {
            Vector2 curLocation = new Vector2(LocationManager.locManager.latitude,
                                              LocationManager.locManager.longitude);
            float distance = Vector2.Distance(savedLocation, curLocation);
            isCreate = distance < 0.003f;

            if (isCreate && TrackedImage.childCount == 0)
            {
                GameObject prefab = Resources.Load<GameObject>(imageName);
                GameObject go = Instantiate(prefab, TrackedImage.position, TrackedImage.rotation);
                go.transform.SetParent(TrackedImage);
            }
        }
    }

    void LoadFunc(Task<DataSnapshot> myTask)
    {
        searchComplete = false;

        if (myTask.IsCanceled || myTask.IsFaulted)
        {
            print("�����͸� �дµ� �����߽��ϴ�.");
        }
        else if (myTask.IsCompleted)
        {
            // �о�� DB�� �����͸� ������ �����Ѵ�.
            DataSnapshot allSnapshot = myTask.Result;

            foreach (DataSnapshot snapshot in allSnapshot.Children)
            {
                string readData = snapshot.GetRawJsonValue();
                SaveData parseData = JsonUtility.FromJson<SaveData>(readData);

                // �о�� �������� name ���� "Cat"�̸鼭, ���������� ��ȹ���� ���� �����...
                if (parseData.name == prefabName && parseData.captureState == false)
                {
                    savedLocation = new Vector2(parseData.latitude, parseData.longitude);
                    break;
                }
            }

            searchComplete = true;
        }
    }
}


// ������ ����� Ŭ����
[Serializable]
public class SaveData
{
    public string name;
    public float latitude;
    public float longitude;
    public bool captureState;

    public SaveData(string prefabName, float lat, float lon, bool state)
    {
        name = prefabName;
        latitude = lat;
        longitude = lon;
        captureState = state;
    }
}