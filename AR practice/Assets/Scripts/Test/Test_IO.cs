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

        //모바일 루트 저장 폴더
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
        //스트림을 생성
        FileStream fs = new FileStream(Path.Combine(fileSavePath, fileName), FileMode.Create, FileAccess.Write);

        //생성된 스트림으로부터 파일을 쓰기 위한 준비를 함
        StreamWriter sw = new StreamWriter(fs);

        //파일을 스트림(쓰기)으로 전송
        sw.Write(uiText.text);

        //열어놨던 스트림을 닫음
        sw.Close();
        fs.Close();


    }

    void ReadTextFile()
    {
        string fullPath = Path.Combine(fileSavePath, fileName);

        //파일 스트림(읽기)를 염
        //FileStream fs = new FileStream(Path.Combine(fullPath, FileMode.Open, FileAccess.Read));
        
        //스트림에서 읽기 위한 준비를 함
        //StreamReader sr = new StreamReader(fs);

        //파일을 읽어서 UI에 씀
        //sr.Read();

    }

}
