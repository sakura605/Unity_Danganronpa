using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager dbinstance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>(); //인덱스역할, 데이터 역할

    public static bool isFinish = false;    //데이터 파싱을 해서 파싱된 데이터가 전부 저장이 되었느냐 안되었느냐

    private void Awake()
    {
        if (dbinstance == null)
        {
            dbinstance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName); //Array를 리턴해서 가져와서 dialogues에 모든 데이터가 담김

            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]); //첫번쨰 대사를 0번쨰 대사로 시작하면 직관적이지 않아 1부터 시작하게 만듬
            }
            isFinish = true;
        }
    }

    public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0; i <= _EndNum - _StartNum; i++) //1~3번째 대사까지 꺼내온다치면 i는 0부터시작하기때문에 0,1,2를해야 3개가 받아와짐
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialogueList.ToArray();
    }
}
