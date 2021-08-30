using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text;

public class CSV_Reader : MonoBehaviour
{
    public string filePath;
    public string fileName;
    public Text uiText;

    //�����͸� ��Ƴ��� ����
    List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

    // Start is called before the first frame update
    void Start()
    {
        //Read();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Dictionary<string, object>> Read(string fileFullPath)
    {
        //��ȯ�� �ӽ� ������ ����
        List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();

        //string fullPath = Path.Combine(Application.dataPath, "TeatSave", fileName);
        using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("euc-kr")))
            {
                //��ü �����͸� �о��
                string readData = sr.ReadToEnd();

                //�ٿ� ���� ����
                string[] lineData = readData.Split('\n');

                for (int i = 0; i < lineData.Length; i++)
                {
                    //��� 1 - Split  �Լ��� �̿��� ��
                    string[] tempData = lineData[i].Split('\r');
                    lineData[i] = tempData[0];

                    //��� 2 - ������ ���ڸ� ����
                    //string�� �迭�� �׷��� 0���� �����ϴ� �迭�� ���̸� ��� ���ؼ� ����� 1���� �� ���ʹ� �޸� 1�� ���ִ� ����
                    lineData[i] = lineData[i].Remove(lineData[i].Length - 1);

                    //��� 3 - ���� ���ڸ� �� ���ڷ� ��ü
                    lineData[i] = lineData[i].Replace("\r", "");
                
                }
                //ù ���� Ű �迭�� ����
                string[] dataKey = lineData[0].Split(',');
                //��° ���� �ڷ��� �迭�� ����
                string[] dataType = lineData[1].Split(',');

                //����� ��ųʸ� ������ ����
                Dictionary<string, object> tempDict = new Dictionary<string, object>();

                //���κ��� ��ȸ
                for(int i = 2; i < lineData.Length; i++)
                {
                    string[] values = lineData[i].Split(',');

                    //�÷����� ��ȸ
                    for(int j = 0; j < dataKey.Length; j++)
                    {
                        //���� ���� ���� ���� ��쿡�� �б⸦ �ߴ�
                        //for (values[j] == "" || values[j] == null)
                        //{
                        //    print("�� ����, Ȯ�� ���");
                        //    break;
                        //}

                        tempDict.Add(dataKey[j], values[j]);
                    }
                    //�ش� ���� ��ųʸ��� ����Ʈ�� �߰�
                    data.Add(tempDict);

                }

                print("�Ͼ���� �Ϸ�");
                return tempList;

            }
            

        }
        
    }

}
