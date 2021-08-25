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
        reference.Child("Marker").Child("Test").SetRawJsonValueAsync("���� �Ϸ�");

        print("DB�� ���� �Ϸ�");
    }

    //DB���� �����͸� �о���� �Լ�
    public IEnumerator LoadDB(string imageName, Transform TrackedImage)
    {
        //DB���� �о������ ��Ʈ�� ã��
       // DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Marker").("Marker");

        prefabName = imageName;

        //reference.GetValueAsync().ContinueWith(LoadFunc);

        //�˻��� �Ϸ�� �� ���� ���
        while (!searchComplete)
        {
            yield return null;
        }

        //���� ��ġ�� �˻��� ��ġ�� ��
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
    //        //�������� �ش� ��Ʈ�� �ڽ� ������ ���� Json �������� �о��
    //        string db_data = snapshot.GetRawJsonValue();
    //        //print("�о�� ������ : " + db_data);

    //        SaveData parseData = JsonUtility.

    //                //Json �����͸� SaveData �ν��Ͻ� ���·� ��ȯ
    //        result = JsonUtility.FromJson<SaveData>(db_data);

    //        //for(int i = 0; i < snapshot.ChildrenCount; i++)
    //        //{   

    //        //}
    //    }
    //}

}

//������ ����� Ŭ����
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