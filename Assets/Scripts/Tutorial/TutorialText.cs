using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class TutorialText : MonoBehaviour
{
   public static List<List <String>> tutorialText = new List<List<String>>();

    void Awake()
    {
        print("tutorial text");

        List<string> a0 = new List<string>();
        //a0
        a0.Add("브레이모의 캐릭터는 직접 조작할 필요가 없습니다. \n캐릭터는 만들어진 AI에 따라 자동으로 움직이게 됩니다.");
        a0.Add("아래에 있는 것은 내 캐릭터이고 위에 있는 것은 적입니다. ");
        tutorialText.Add(a0);

        //a1
        List<string> a1 = new List<string>();
        a1.Add("내 캐릭터는 이와 같은 AI로 작동하게 됩니다.");
        tutorialText.Add(a1);

        List<string> a2 = new List<string>();
        a2.Add("축하합니다. 적을 모두 죽였습니다. 보상으로 무기와 방어구를 드리겠습니다.");
        tutorialText.Add(a2);

        List<string> a3 = new List<string>();
        a3.Add("이곳은 홈 화면입니다. 캐릭터 정보, 다양한 게임모드, 퀘스트, 우편함 등을 확인할 수 있습니다.");
        a3.Add("오른쪽 아래의 캐릭터룸을 클릭해보세요.");
        tutorialText.Add(a3);

        List<string> a4 = new List<string>();
        a4.Add("이곳은 캐릭터룸 입니다. \n이곳에서는 캐릭터에게 장비와 아이템을 장착/해제 할 수 있습니다. ");
        a4.Add("아이템을 클릭하여 장착하고 캐릭터의 AI를 만드는 코드에디터 창으로 가보겠습니다. \n왼쪽의 아래의 코드수정을 눌러보세요");
        tutorialText.Add(a4);

        List<string> a5 = new List<string>();
        a5.Add("");
        tutorialText.Add(a5);

        List<string> a6 = new List<string>();
        a6.Add("이곳은 캐릭터의 AI를 만들 수 있는 코드에디터 창입니다. ");
        a6.Add("중간의 책은 내 캐릭터를 나타냅니다.");
        a6.Add("다음으로 있는 초록색 네모들은 레이블을 나타냅니다. \n 레이블에는 공격/이동/아이템이 있습니다.");
        a6.Add("레이블인 초록색 공격을 눌러볼까요?");
        tutorialText.Add(a6);


        List<string> a7 = new List<string>();
        a7.Add("레이블을 클릭하면 사용가능한 액션이 오른쪽에 나타납니다. ");
        a7.Add("오른쪽에 있는 무기사용 액션을 드래그 앤 드롭하여 화면에 가져오세요.");
        tutorialText.Add(a7);


        List<string> a8 = new List<string>();
        a8.Add("이것은 적을 공격하는 액션입니다. \n내 캐릭터에 이 액션이 있으면 자동으로 상대방을 공격하게 됩니다. ");
        a8.Add("이제 어떤 상황에서 공격할 것인지를 정하는 조건을 추가해보겠습니다. ");
        a8.Add(" 보라색 동그라미의 칼 모양인 공격하기 액션을 클릭한 후 오른쪽 조건에서 체력이 25%이상일 경우를 클릭하여 드래그 앤 드롭을 하세요. ");
        tutorialText.Add(a8);

        List<string> a9 = new List<string>();
        a9.Add("이제 하나의 액션-조건이 완성되었습니다. 이제부터 내 캐릭터는 체력이 25% 이상일 경우 언제든 공격을 하게 됩니다 ");
        a9.Add("이제 적을 무찌르러 가볼까요? \n 뒤로 가기 버튼을 눌러 홈화면으로 가세요. ");
        tutorialText.Add(a9);

        List<string> a10 = new List<string>();
        a10.Add("뒤로가기버튼을 눌러주세요.");
        tutorialText.Add(a10);

        List<string> a11 = new List<string>();
        a11.Add("스토리모드를 클릭해주세요");
        tutorialText.Add(a11);

        List<string> a12 = new List<string>();
        a12.Add("이곳은 시대를 선택하는 곳입니다. 스테이지를 진행 할수록 새로운 시대로 여행을 떠날 수 있습니다. ");
        a12.Add("19세기 유럽으로 입장해주세요.");
        tutorialText.Add(a12);

        List<string> a13 = new List<string>();
        a13.Add("이곳은 스테이지를 선택하는 곳입니다.");
        a13.Add("스테이지를 선택하면 받을 수 있는 보상 및 적을 확인할 수 있습니다.");
        a13.Add("게임 시작하기를 눌러주세요.");
        tutorialText.Add(a13);

        List<string> a14 = new List<string>();
        a14.Add("축하합니다. Stage1을 클리어했습니다. 얻은 장비를 장착하고 코드에디터 창으로 가보세요.");
        tutorialText.Add(a14);

        List<string> a15 = new List<string>();
        a15.Add("현재 내 캐릭터는 적이 범위 안에 있을 때만 공격할 수 있습니다. \n적이 내 범위보다 멀리 있을 경우 적에게 다가갈 수 없습니다. ");
        a15.Add("따라서 다음으로는 내 캐릭터를 적에게 다가가게 해보겠습니다.  ");
        a15.Add("앞으로 가는 것은 이동을 하는 것이니 초락색의 네모난 이동 레이블을 클릭하세요. ");
        tutorialText.Add(a15);

        List<string> a16 = new List<string>();
        a16.Add("오른쪽의 액션 목록 중 앞으로 가기를 클릭하여 드래그 앤 드롭을 하세요. ");
        tutorialText.Add(a16);

        List<string> a17 = new List<string>();
        a17.Add("이제 앞으로 가기에 적합한 조건을 달아줘야 합니다. \n 앞으로 가기 액션을 클릭한 후 주변에 적이 없을 경우를 드래그 앤 드롭을 해주세요 ");
        tutorialText.Add(a17);

        List<string> a18 = new List<string>();
        a18.Add("완료되었습니다. \n이제 주변에 적이 없을 경우 적에게 다가가게 됩니다. \n스테이지 2를 클리어하러 가볼까요? ");
        tutorialText.Add(a18);


        List<string> a19 = new List<string>();
        a19.Add("");
        tutorialText.Add(a19);


        /*
        List<string> a14 = new List<string>();
        a14.Add("축하합니다. 이제 마지막 단계입니다. 코드에디터 창으로 오세요.");
        tutorialText.Add(a14);

        List<string> a15 = new List<string>();
        a15.Add("스테이지3의 적은 나보다 강력하여 아이템이 필요합니다. 따라서 포션을 이용하여야 합니다. 내 캐릭터가 포션을 사용하도록 적절한 액션과 조건을 추가해주세요");
        a15.Add("참고로 포션은 아이템에 해당합니다. ");
        tutorialText.Add(a15);


        List<string> a16 = new List<string>();
        a16.Add("축하합니다. 튜토리얼을 완료하였습니다. 마지막으로 엠블럼에 대해 소개하겠습니다.");
        a16.Add("캐릭터창으로 오세요.");
        tutorialText.Add(a16);

        List<string> a17 = new List<string>();
        a17.Add("튜툐리얼을 완료하여 엠블럼을 획득했습니다. 엠블럼은 캐릭터에 특수한 능력을 더해줍니다. 엠블럼을 장착하여 볼까요?");
        tutorialText.Add(a17);


        List<string> a18 = new List<string>();
        a18.Add("잘하였습니다. 엠블럼을 통해 캐릭터는 은신, 사거리 증가 등 다양한 특수한 능력을 얻을 수 있습니다. 앞으로 모험을 하며 다양한 엠블럼을 모아보세요.");
        tutorialText.Add(a18);
        */


        /*
        foreach (var array in tutorialText)
        {
            Debug.Log(array);

            foreach (var item in array)
            {
                Debug.Log(" ");
                Debug.Log(item);
            }
        }*/

    }
}

    