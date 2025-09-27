using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ShopUIData : BaseUIData
{
    //public bool IsPossesed { get; set; }
}
public class ShopUI : BaseUI
{
    // 캐릭터 정보 
    private CharacterModel[] _characterModel;

    // 캐릭터 보유 및 선택 정보
    private UserCharacterData _userCharacterData;

    // 페이지 전환 관련
    [SerializeField] private GameObject CharShopUI;
    [SerializeField] private GameObject GoldShopUI;
    [SerializeField] private TextMeshProUGUI pageText;

    private int currentPageIndex = 0;

    // 각 페이지에 해당하는 캐릭터 정보를 표현하는 변수
    [SerializeField] private Image left_SaleCharacterImage;
    [SerializeField] private Image right_SaleCharacterImage;
    [SerializeField] private TextMeshProUGUI leftCharacterName;
    [SerializeField] private TextMeshProUGUI rightCharacterName;
    [SerializeField] private TextMeshProUGUI leftSkillName;
    [SerializeField] private TextMeshProUGUI rightSkillName;
    [SerializeField] private TextMeshProUGUI leftSkillDetail;
    [SerializeField] private TextMeshProUGUI rightSkillDetail;
    [SerializeField] private TextMeshProUGUI leftCharacterPrice;
    [SerializeField] private TextMeshProUGUI rightCharacterPrice;
    [SerializeField] private Button leftCharacterBuyBtn;
    [SerializeField] private Button rightCharacterBuyBtn;

    // 보유 중인 캐릭터를 사용자에게 보여주기 위한 오브젝트
    [SerializeField] private GameObject leftSoldOutMarker;
    [SerializeField] private GameObject rightSoldOutMarker;

    private void Awake()
    {
        _characterModel = DataTableManager.Instance.GetAllCharacterModelData();
        _userCharacterData = UserDataManager.Instance.GetUserData<UserCharacterData>();

        Debug.Log($"현재 보유한 캐릭터 수 : {_userCharacterData.acuiredChatacter.Count}");
        foreach (var Id in _userCharacterData.acuiredChatacter)
        {
            Debug.Log($"보유 캐릭터 : {Id}");
        }
        Debug.Log($"현재 선택된 캐릭터 ID : {_userCharacterData.CharacterID_InUse}");

        UpdatePageData(currentPageIndex);
        UpdatePageNumText();
        Debug.Log($"현재 저장된 캐릭터 정보 수는 {_characterModel.Length} 입니다.");

        UserDataManager.Instance.GetUserData<UserCharacterData>().SoldOut.AddListener(UpdateCurrentPageData);
    }

    public void OnClickLeftPageBtn()
    {
        Debug.Log($"현재 CurrentPageIndex : {currentPageIndex}");
        if (currentPageIndex == _characterModel.Length / 2)
        {
            Debug.Log("현재 페이지는 골드 페이지 입니다. 캐릭터 페이지로 전환합니다.");
            GoldShopUI.SetActive(false);
            CharShopUI.SetActive(true);

        }
        else if (currentPageIndex <= 0)
        {
            Debug.Log("최소 페이지 입니다.");
            return;
        }
        else
        {
            UpdatePageData(currentPageIndex-1);
        }

        currentPageIndex--;
        UpdatePageNumText();
    }

    public void OnClickRightPageBtn()
    {
        if (currentPageIndex >= _characterModel.Length / 2)
        {
            Debug.Log("최대 페이지 입니다.");
            return;
        }
        else if (currentPageIndex >= (_characterModel.Length / 2) -1)
        {
            Debug.Log("현재 페이지는 캐릭터 페이지입니다. 골드 페이지로 전환합니다.");
            CharShopUI.SetActive(false);
            GoldShopUI.SetActive(true);
        }
        else
        {
            UpdatePageData(currentPageIndex+1);
        }

        currentPageIndex++; 
        UpdatePageNumText();
    }

    private void UpdatePageNumText()
    {
        pageText.text = currentPageIndex.ToString();
    }

    public void UpdateCurrentPageData()
    {
        UpdatePageData(currentPageIndex);
    }

    public void UpdatePageData(int currentPageIndex)
    {
        
        int index = currentPageIndex * 2;

        Debug.Log($"좌측 캐릭터 ID : {_characterModel[index].ID}");
        Debug.Log($"우측 캐릭터 ID : {_characterModel[index+1].ID}");

        left_SaleCharacterImage.sprite = Resources.Load<Sprite>($"Textures/{_characterModel[index].ID}");
        right_SaleCharacterImage.sprite = Resources.Load<Sprite>($"Textures/{_characterModel[index + 1].ID}");

        leftCharacterName.text = _characterModel[index].Name;
        rightCharacterName.text = _characterModel[index + 1].Name;

        leftSkillName.text = _characterModel[index].SkillName;
        rightSkillName.text = _characterModel[index + 1].SkillName;

        leftSkillDetail.text = _characterModel[index].SkillDescription;
        rightSkillDetail.text = _characterModel[index + 1].SkillDescription;

        leftCharacterBuyBtn.GetComponent<BuyBtnOfChar>().Char_Id = _characterModel[index].ID;
        rightCharacterBuyBtn.GetComponent<BuyBtnOfChar>().Char_Id = _characterModel[index+1].ID;

        // 구매 버튼에 들어갈 색상, 텍스트 설정
        Color color = new Color(1, 1, 1, 0.3f);
        string leftText = "";
        string rightText = "";

        if (_userCharacterData.CharacterID_InUse == _characterModel[index].ID)
        {
            leftCharacterBuyBtn.interactable = false;
            leftCharacterBuyBtn.image.color = color;
            leftSoldOutMarker.SetActive(true);
            leftText = "선택중";
        }
        else if (_userCharacterData.acuiredChatacter.Contains(_characterModel[index].ID))
        {
            leftCharacterBuyBtn.interactable = true;
            leftSoldOutMarker.SetActive(true);
            leftCharacterBuyBtn.image.color = Color.white;
            leftText = "보유중";
        }
        else
        {
            leftCharacterBuyBtn.interactable = true;
            leftCharacterBuyBtn.image.color = Color.white;
            leftSoldOutMarker.SetActive(false);
            leftText = _characterModel[index].Price.ToString();
        }
        leftCharacterPrice.text = leftText;


        if (_userCharacterData.CharacterID_InUse == _characterModel[index + 1].ID)
        {
            rightCharacterBuyBtn.interactable = false;
            rightCharacterBuyBtn.image.color = color;
            rightSoldOutMarker.SetActive(true);
            rightText = "선택중";
        }
        else if (_userCharacterData.acuiredChatacter.Contains(_characterModel[index + 1].ID))
        {
            rightCharacterBuyBtn.interactable = true;
            rightSoldOutMarker.SetActive(true);
            rightCharacterBuyBtn.image.color = Color.white;
            rightText = "보유중";
        }
        else
        {
            rightCharacterBuyBtn.interactable = true;
            rightCharacterBuyBtn.image.color = Color.white;
            rightSoldOutMarker.SetActive(false);
            rightText = _characterModel[index + 1].Price.ToString();
        }
        rightCharacterPrice.text = rightText;
    }

}
