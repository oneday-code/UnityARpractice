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
        // DB의 주소를 기본 주소 값으로 설정한다.
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(db_Url);

        //SaveDB();
    }
    void SaveDB()
    {
        // 클래스 인스턴스 생성
        SaveData data1 = new SaveData("Cat", 37.123f, 125.456f, false);
        SaveData data2 = new SaveData("Car", 37.789f, 129.987f, false);

        // 클래스 인스턴스를 Json 형식으로 변환한다.
        string jData1 = JsonUtility.ToJson(data1);
        //print(jData1);
        string jData2 = JsonUtility.ToJson(data2);

        // DB의 최상단 루트를 찾는다.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 최상단 루트 하위에 Json 형식의 값을 추가 or 변경한다.
        reference.Child("Marker").Child("Data1").SetRawJsonValueAsync(jData1);
        reference.Child("Marker").Child("Data2").SetRawJsonValueAsync(jData2);
        reference.Child("Marker").Child("Test").SetValueAsync("변경했습니다.");

        print("DB에 저장 완료!");
    }

    // DB에서 데이터를 읽어오는 함수
    public IEnumerator LoadDB(string imageName, Transform TrackedImage)
    {
        // DB에서 읽어오려는 루트를 찾는다.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Marker");

        prefabName = imageName;

        // DB에서 검색 및 값 읽어오기
        reference.GetValueAsync().ContinueWith(LoadFunc);

        #region
        //해당 루트의 값을 json 형식으로 읽어온다.
        //reference.GetValueAsync().ContinueWith((myTask) =>
        //{
        //    if (myTask.IsCanceled || myTask.IsFaulted)
        //    {
        //        print("데이터를 읽는데 실패했습니다.");
        //    }
        //    else if (myTask.IsCompleted)
        //    {
        //        // 읽어온 DB의 데이터를 변수에 저장한다.
        //        DataSnapshot allSnapshot = myTask.Result;

        //        foreach (DataSnapshot snapshot in allSnapshot.Children)
        //        {
        //            string readData = snapshot.GetRawJsonValue();
        //            SaveData parseData = JsonUtility.FromJson<SaveData>(readData);

        //            // 읽어온 데이터의 name 값이 "Cat"이면서, 누군가에게 포획되지 않은 경우라면...
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

        // 검색이 완료될 때까지 대기한다.
        while (!searchComplete)
        {
            yield return null;
        }

        // 현재 위치와 검색된 위치를 비교한다.
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
            print("데이터를 읽는데 실패했습니다.");
        }
        else if (myTask.IsCompleted)
        {
            // 읽어온 DB의 데이터를 변수에 저장한다.
            DataSnapshot allSnapshot = myTask.Result;

            foreach (DataSnapshot snapshot in allSnapshot.Children)
            {
                string readData = snapshot.GetRawJsonValue();
                SaveData parseData = JsonUtility.FromJson<SaveData>(readData);

                // 읽어온 데이터의 name 값이 "Cat"이면서, 누군가에게 포획되지 않은 경우라면...
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


// 데이터 저장용 클래스
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