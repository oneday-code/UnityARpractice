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
        //Ŭ���� �ν��Ͻ� ����
        SaveData data1 = new SaveData("Cat", 37.123f, 125.456f, false);
        SaveData data2 = new SaveData("Car", 37.789f, 123.357f, false);

        //Ŭ���� �ν��Ͻ��� Json �������� ��ȯ
        string jData1 = JsonUtility.ToJson(data1);
        print(jData1);
        string jData2 = JsonUtility.ToJson(data2);

        //DB�� �ֻ�� ��Ʈ�� ã��
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        //�ֻ�� ��Ʈ ������ Json ������ ���� �߰� or ������
        reference.Child("Marker").Child("Data1").SetRawJsonValueAsync(jData1);
        reference.Child("Marker").Child("Data2").SetRawJsonValueAsync(jData2);
            
            
    }

    //DB���� �����͸� �о���� �Լ�
    IEnumerator LodeDB()
    {
        //DB���� �о������ ��Ʈ�� ã��
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Marker").Child("Data2");

        //�ش� ��Ʈ�� ���� json �������� �о��
        reference.GetValueAsync().ContinueWith((myTask) =>
        {
            
            if (myTask.IsCanceled || myTask.IsFaulted)
            {
                print(" �����͸� �дµ� ��������");

            }
            //�о���µ� ������ �ߴٸ�
            else if (myTask.IsCompleted)
            {
                print("�����͸� �о��");
                // �о���� �������� ���¸� ������ ����
                DataSnapshot snapshot = myTask.Result;

                //�������� �ش� ��Ʈ�� �ڽ� ������ ���� Json �������� �о��
                string db_data = snapshot.GetRawJsonValue();
                //print("�о�� ������ : " + db_data);

                //Json �����͸� SaveData �ν��Ͻ� ���·� ��ȯ
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

//������ ����� Ŭ����
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