using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterPick;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CharacterPick
{
    public class PickedCharacterPanel : MonoBehaviour
    {
        [SerializeField] GameObject pickedSlot;
        public static int characterNum;
        public static PickedCharacter[] pickedCharacter;
        //[SerializeField] GameObject innerSlot;
        private Transform previousAnimatedOne;
        public static int index;
        public bool isFull;
        public int pickedCharacterNum;
        _StageInformation _stageInfo;
        StageInformation stageInfo;
        void Start()
        {
            // index = -1;
            // isFull = false;

            // if (GameObject.Find("PassStageInfoBetweenScenes_dontDestroy") != null)
            // {
            //     //passData = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<passDataBetweenScene>();
            //     _stageInfo = GameObject.Find("PassStageInfoBetweenScenes_dontDestroy").GetComponent<_StageInformation>();
            // }
            // else
            // {
            //     stageInfo = GameObject.Find("CharacterPickUIManager").GetComponent<LoadCharInfo>().stageInfo;
            //     Debug.Log("NO dontdestroy");
            // }

            // DOTween.Init();
            // setSlots();
            // checkTargetSlot();
        }

        public void loadInfo(_StageInformation _stageInfo)
        {
            index = -1;
            isFull = false;

            this._stageInfo = _stageInfo;

            DOTween.Init();
            setSlots();
            checkTargetSlot();
        }

        public void setSlots()
        {
            // TextAsset textData = Resources.Load("GamePlay/gameStageInfo") as TextAsset;
            // string stageJsonData = textData.text;
            // StageInformation stageInfo = JsonConvert.DeserializeObject<StageInformation>(stageJsonData);


            pickedCharacterNum = _stageInfo.charNumber;
            characterNum = pickedCharacterNum;
            pickedCharacter = new PickedCharacter[pickedCharacterNum];

            for (int i = 0; i < _stageInfo.charNumber; i++)
            {
                var newOuterSlot = Instantiate(pickedSlot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                newOuterSlot.transform.SetParent(transform);
                newOuterSlot.transform.localScale = new Vector3(1, 1, 1);

                pickedCharacter[i] = newOuterSlot.GetComponent<PickedCharacter>();
            }

            //instantiate하게 수정해야 함
            //pickedCharacter = this.transform.GetComponentsInChildren<PickedCharacter>();
        }

        public void checkTargetSlot()
        {
            int i;
            for (i = 0; i < characterNum; i++)
            {
                if (pickedCharacter[i].characterImage.sprite == null)
                {
                    isFull = false;
                    break;
                }
                if (i == characterNum - 1)
                {
                    isFull = true;
                    // DOTween.KillAll(previousAnimatedOne);
                    // previousAnimatedOne.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    // previousAnimatedOne = null;
                    index = -1;
                }
            }
            if (previousAnimatedOne != null)
            {
                // DOTween.KillAll(previousAnimatedOne);
                // previousAnimatedOne.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            if (i < characterNum)
            {
                index = i;
                // previousAnimatedOne = pickedCharacter[index].characterImage.transform;
                // DOTween.To(() => pickedCharacter[index].Image.color, x => pickedCharacter[index].characterImage.color = x, new Color32(255, 255, 255, 130), 0.5f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
            }
        }

    }
}
