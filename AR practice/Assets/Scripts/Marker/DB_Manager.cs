using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;

public class DB_Manager: MonoBehaviour
{
    public string db_Url = "";

    public SaveData result;

    // Start is called before the first frame update
    void Start()
    {
        //FirebaseApp.Defaultlnstance.Options.DatabaseUrl = new Uri(db_Url);

        //SaveDB();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            StartCoroutine(LodeDB());
        }    
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
            
            
    }

    //DB에서 데이터를 읽어오는 함수
    IEnumerator LodeDB()
    {
        //DB에서 읽어오려는 루트를 찾음
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Marker").Child("Data2");

        //해당 루트의 값을 json 형식으로 읽어옴
        reference.GetValueAsync().ContinueWith((myTask) =>
        {
            
            if (myTask.IsCanceled || myTask.IsFaulted)
            {
                print(" 데이터를 읽는데 실패했음");

            }
            //읽어오는데 성공을 했다면
            else if (myTask.IsCompleted)
            {
                print("데이터를 읽어옴");
                // 읽어오는 데이터의 상태를 변수에 저장
                DataSnapshot snapshot = myTask.Result;

                //데이터의 해당 루트의 자식 노드들을 전부 Json 형식으로 읽어옴
                string db_data = snapshot.GetRawJsonValue();
                //print("읽어온 데이터 : " + db_data);

                //Json 데이터를 SaveData 인스턴스 형태로 변환
                result = JsonUtility.FromJson<SaveData>(db_data);

                //foreach (DataSnapshot snap in snapshot.Children)
                //for(int i = 0; i < snapshot.ChildrenCount; i++)
                //{   

                //}
            }
        
        });

        yield return null;

    }

}

//데이터 저장용 클래스
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