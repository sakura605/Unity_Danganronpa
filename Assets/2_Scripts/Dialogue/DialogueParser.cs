using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //대사 리스트 생성.
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);    //csv파일 가져옴.

        string[] data = csvData.text.Split(new char[] { '\n' });  //엔터 단위로 한줄씩 쪼갬 //new char[] { '\n' }공식같은거
                                                                  //data[0] 엔 첫줄 data[1]엔 둘째줄

        //첫번째 줄엔 별 내용이 없기 때문에 1부터 시작
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); //콤마 단위로 쪼갬

            Dialogue dialogue = new Dialogue(); //대사 리스트 생성

            dialogue.name = row[1]; //이름을 넣어줌
            //Debug.Log(row[1]);
            List<string> contextList = new List<string>();
            List<string> spriteList = new List<string>();
            do
            {
                contextList.Add(row[2]);
                spriteList.Add(row[3]);
                //Debug.Log(row[2]);
                if (++i < data.Length)
                    row = data[i].Split(new char[] { ',' });
                else
                    break;
            } while (row[0].ToString() == "");  //다음 로우에 아이디가 여백이라면

            dialogue.contexts = contextList.ToArray();
            dialogue.spriteName = spriteList.ToArray();
            dialogueList.Add(dialogue); //주인공의 이름과 대사를 한셋트로 묶어서 리스트에 추가
        }

        return dialogueList.ToArray();
    }

    void Start()
    {
        Parse("Ingame");
    }

}
