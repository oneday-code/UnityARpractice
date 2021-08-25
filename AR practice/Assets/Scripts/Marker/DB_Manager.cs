using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.IO;
using System.Text;

public class DB_Manager: MonoBehaviour
{
    public string db_Url = "https://unitydbproject-default-rtdb.firebaseio.com/";

    public SaveData result;
    public bool isCreate = false;
    public static DB_Manager db;

    string prefabName;
    Vector2 savedLocation;
    public bool searchComplete = false;



    private void Awake()
    {
        if(db == null)
        {
            db = this;
        }
        else
        {
            Destroy(gameObject);

        }
    
    }

    // Start is called before the first frame update
    void Start()
    {
        //FirebaseApp.Defaultlnstance.Options.DatabaseUrl = new Uri(db_Url);

        //SaveDB();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void SaveDB()
    {
        //클래스 인스턴스 생성
        SaveData data1 = new SaveData("Cat", 37.123f, 125.456f, false);
        SaveData data2 = new SaveData("Car", 37.789f, 123.357f, false);

        //클래스 인스턴스를 Json 형식으로 변환
        string jData1 = JsonUtility.ToJson(data1);
        print(jData1);
        string jData2 = JsonUtility.ToJson(data2);

        //DB의 최상단 루트를 찾음
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        //최상단 루트 하위에 Json 형식의 값을 추가 or 변경함
        reference.Child("Marker").Child("Data1").SetRawJsonValueAsync(jData1);
        reference.Child("Marker").Child("Data2").SetRawJsonValueAsync(jData2);
        reference.Child("Marker").Child("Test").SetRawJsonValueAsync("변경 완료");

        print("DB에 저장 완료");
    }

    //DB에서 데이터를 읽어오는 함수
    public IEnumerator LoadDB(string imageName, Transform TrackedImage)
    {
        //DB에서 읽어오려는 루트를 찾음
       // DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Marker").("Marker");

        prefabName = imageName;

        //reference.GetValueAsync().ContinueWith(LoadFunc);

        //검색이 완료될 때 까지 대기
        while (!searchComplete)
        {
            yield return null;
        }

        //형재 위치와 검색된 위치를 비교
        if(savedLocation != Vector2.zero)
        {
            //Vector2 curLocation = new Vector2(LocationManager.IocManager.latitude, LocationManager.locManager.longitude);

            //float distance = Vector2.Distance(savedLocation, curLocation);
            //isCreate = distance < 0.003f;

            if(isCreate && TrackedImage.childCount == 0)
            {
                GameObject prefab = Resources.Load<GameObject>(imageName);
                GameObject go = Instantiate(prefab, TrackedImage.position, TrackedImage.rotation);
                go.transform.SetParent(TrackedImage);

            }
            //if(distance < 0.001f)
            //{
            //    isCreate = true;
            //}
            //else
            //{
            //    isCreate = false;
            //}

            //isCreate = distance < 0.001f;
        }

    }

    //void LoadFunc(Task<DataSnapshot> myTask)
    //{
    //    searchComplete = false;

    //    foreach (DataSnapshot snap in snapshot.Children)
    //    {
    //        //데이터의 해당 루트의 자식 노드들을 전부 Json 형식으로 읽어옴
    //        string db_data = snapshot.GetRawJsonValue();
    //        //print("읽어온 데이터 : " + db_data);

    //        SaveData parseData = JsonUtility.

    //                //Json 데이터를 SaveData 인스턴스 형태로 변환
    //        result = JsonUtility.FromJson<SaveData>(db_data);

    //        //for(int i = 0; i < snapshot.ChildrenCount; i++)
    //        //{   

    //        //}
    //    }
    //}

}

//데이터 저장용 클래스
//[Serializable]
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