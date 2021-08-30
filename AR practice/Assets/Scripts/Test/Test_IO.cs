using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Test_IO : MonoBehaviour
{
    public string fileSavePath;
    public string fileName;

    public Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        string currentPath = Directory.GetCurrentDirectory();
        //print(currentPath);

        string currentPath2 = Application.dataPath;
        //print(currentPath2);

        //����� ��Ʈ ���� ����
        string currentPath3 = Application.streamingAssetsPath;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveTextFile();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadTextFile();
        }

    }

    public void SaveTextFile()
    {
        //��Ʈ���� ����
        FileStream fs = new FileStream(Path.Combine(fileSavePath, fileName), FileMode.Create, FileAccess.Write);

        //������ ��Ʈ�����κ��� ������ ���� ���� �غ� ��
        StreamWriter sw = new StreamWriter(fs);

        //������ ��Ʈ��(����)���� ����
        sw.Write(uiText.text);

        //������� ��Ʈ���� ����
        sw.Close();
        fs.Close();


    }

    void ReadTextFile()
    {
        string fullPath = Path.Combine(fileSavePath, fileName);

        //���� ��Ʈ��(�б�)�� ��
        //FileStream fs = new FileStream(Path.Combine(fullPath, FileMode.Open, FileAccess.Read));
        
        //��Ʈ������ �б� ���� �غ� ��
        //StreamReader sr = new StreamReader(fs);

        //������ �о UI�� ��
        //sr.Read();

    }

}
