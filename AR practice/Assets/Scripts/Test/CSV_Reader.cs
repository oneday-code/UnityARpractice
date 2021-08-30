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

    //데이터를 담아놓을 변수
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
        //반환할 임시 변수를 만듦
        List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();

        //string fullPath = Path.Combine(Application.dataPath, "TeatSave", fileName);
        using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("euc-kr")))
            {
                //전체 데이터를 읽어옴
                string readData = sr.ReadToEnd();

                //줄에 따라 나눔
                string[] lineData = readData.Split('\n');

                for (int i = 0; i < lineData.Length; i++)
                {
                    //방식 1 - Split  함수를 이용할 때
                    string[] tempData = lineData[i].Split('\r');
                    lineData[i] = tempData[0];

                    //방식 2 - 마지막 글자를 지움
                    //string은 배열임 그래서 0부터 시작하는 배열의 길이를 재기 위해서 사람이 1부터 샐 때와는 달리 1을 빼주는 것임
                    lineData[i] = lineData[i].Remove(lineData[i].Length - 1);

                    //방식 3 - 기행 문자를 빈 문자로 교체
                    lineData[i] = lineData[i].Replace("\r", "");
                
                }
                //첫 줄은 키 배열로 저장
                string[] dataKey = lineData[0].Split(',');
                //둘째 줄은 자료형 배열로 저장
                string[] dataType = lineData[1].Split(',');

                //저장용 딕셔너리 변수를 선언
                Dictionary<string, object> tempDict = new Dictionary<string, object>();

                //라인별로 순회
                for(int i = 2; i < lineData.Length; i++)
                {
                    string[] values = lineData[i].Split(',');

                    //컬럼별로 순회
                    for(int j = 0; j < dataKey.Length; j++)
                    {
                        //값이 없는 셀을 만난 경우에는 읽기를 중단
                        //for (values[j] == "" || values[j] == null)
                        //{
                        //    print("빈값 있음, 확인 요망");
                        //    break;
                        //}

                        tempDict.Add(dataKey[j], values[j]);
                    }
                    //해당 줄의 딕셔너리를 리스트에 추가
                    data.Add(tempDict);

                }

                print("일어오기 완료");
                return tempList;

            }
            

        }
        
    }

}
