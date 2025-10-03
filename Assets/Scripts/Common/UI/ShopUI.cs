using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct CharacterSalePanel
{
    //판매 캐릭터 정보
    public Image saleCharacterImage;
    [Space(10f)]
    public TextMeshProUGUI characterName;
    [Space(10f)]
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDetail;
    [Space(10f)]
    public TextMeshProUGUI characterPrice;
    [Space(10f)]
    public Button characterBuyBtn;
    [Space(10f)]
    // 구매 완료 표시 오브젝트
    public GameObject soldOutMarker;

}

public class ShopUI : BaseUI
{
    // 캐릭터 정보 
    private CharacterModel[] _characterModel;

    // 캐릭터 보유 및 선택 정보
    private UserCharacterData _userCharacterData;

    // 페이지 전환 관련
    [Header("Page Change UI")]
    [SerializeField] private GameObject CharShopUI;
    [SerializeField] private GameObject GoldShopUI;

    [Header("PageIndex Text")]
    [SerializeField] private TextMeshProUGUI pageText;

    private int currentPageIndex = 0;
    private int maxPageIndex;

    [Header("Left Character Sale Panel")]
    public CharacterSalePanel leftCSPanel;

    [Header("Right Character Sale Panel")]
    public CharacterSalePanel rightCSPanel;

    [Header("SkillDescriptionArea")]
    [SerializeField] ScrollRect leftSkillView;
    [SerializeField] ScrollRect rightSkillView;

    [Space]
    [SerializeField] private float descriptionPosition = 1f;
    [SerializeField] private float descriptionSpeed = 0.5f;
    [SerializeField] private float descriptionDelayTime = 0.5f;
    private float descriptionTime = 0f;

    private void Awake()
    {
        _characterModel = DataTableManager.Instance.GetAllCharacterModelData();
        _userCharacterData = UserDataManager.Instance.GetUserData<UserCharacterData>();

        maxPageIndex = _characterModel.Length / 2;

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

    private void Update()
    {
        MoveSkillDescription();
    }
    public void ShowGoldPage()
    {
        currentPageIndex = maxPageIndex;
        GoldShopUI.SetActive(true);
        CharShopUI.SetActive(false);
        UpdatePageNumText();
    }

    public void OnClickLeftPageBtn()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        Debug.Log($"현재 CurrentPageIndex : {currentPageIndex}");
        if (currentPageIndex ==  maxPageIndex)
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

        descriptionTime = 0f;
        leftSkillView.verticalNormalizedPosition = 1;
        rightSkillView.verticalNormalizedPosition = 1;

        UpdatePageData(currentPageIndex - 1);
        currentPageIndex--;
        UpdatePageNumText();
    }

    public void OnClickRightPageBtn()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        if (currentPageIndex >= maxPageIndex)
        {
            Debug.Log("최대 페이지 입니다.");
            return;
        }
        else if (currentPageIndex >= (maxPageIndex) -1)
        {
            Debug.Log("현재 페이지는 캐릭터 페이지입니다. 골드 페이지로 전환합니다.");
            CharShopUI.SetActive(false);
            GoldShopUI.SetActive(true);
        }
        else
        {
            UpdatePageData(currentPageIndex + 1);
        }

        descriptionTime = 0f;
        leftSkillView.verticalNormalizedPosition = 1;
        rightSkillView.verticalNormalizedPosition = 1;

        currentPageIndex++; 

        UpdatePageNumText();
    }

    private void UpdatePageNumText()
    {
        pageText.text = (currentPageIndex + 1).ToString();
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

        // left Character Sale panel
        leftCSPanel.saleCharacterImage.sprite = Resources.Load<Sprite>($"Textures/{_characterModel[index].ID}");
        leftCSPanel.characterName.text = _characterModel[index].Name;
        leftCSPanel.skillName.text = _characterModel[index].SkillName;
        leftCSPanel.skillDetail.text = _characterModel[index].SkillDescription;
        leftCSPanel.characterBuyBtn.GetComponent<BuyBtnOfChar>().Char_Id = _characterModel[index].ID;

        //right Character Sale panel
        rightCSPanel.saleCharacterImage.sprite = Resources.Load<Sprite>($"Textures/{_characterModel[index + 1].ID}");
        rightCSPanel.characterName.text = _characterModel[index + 1].Name;
        rightCSPanel.skillName.text = _characterModel[index + 1].SkillName;
        rightCSPanel.skillDetail.text = _characterModel[index + 1].SkillDescription;
        rightCSPanel.characterBuyBtn.GetComponent<BuyBtnOfChar>().Char_Id = _characterModel[index + 1].ID;


        // 구매 버튼에 들어갈 색상, 텍스트 설정
        Color color = new Color(1, 1, 1, 0.3f);

        if (_userCharacterData.CharacterID_InUse == _characterModel[index].ID)
        {
            leftCSPanel.characterBuyBtn.interactable = false;
            leftCSPanel.characterBuyBtn.image.color = color;
            leftCSPanel.soldOutMarker.SetActive(true);

            leftCSPanel.characterPrice.text = "선택중";
        }
        else if (_userCharacterData.acuiredChatacter.Contains(_characterModel[index].ID))
        {
            leftCSPanel.characterBuyBtn.interactable = true;
            leftCSPanel.soldOutMarker.SetActive(true);
            leftCSPanel.characterBuyBtn.image.color = Color.white;

            leftCSPanel.characterPrice.text = "보유중";
        }
        else
        {
            leftCSPanel.characterBuyBtn.interactable = true;
            leftCSPanel.characterBuyBtn.image.color = Color.white;
            leftCSPanel.soldOutMarker.SetActive(false);
            leftCSPanel.characterPrice.text = _characterModel[index].Price.ToString();
        }


        if (_userCharacterData.CharacterID_InUse == _characterModel[index + 1].ID)
        {
            rightCSPanel.characterBuyBtn.interactable = false;
            rightCSPanel.characterBuyBtn.image.color = color;
            rightCSPanel.soldOutMarker.SetActive(true);

            rightCSPanel.characterPrice.text = "선택중";
        }
        else if (_userCharacterData.acuiredChatacter.Contains(_characterModel[index + 1].ID))
        {
            rightCSPanel.characterBuyBtn.interactable = true;
            rightCSPanel.soldOutMarker.SetActive(true);
            rightCSPanel.characterBuyBtn.image.color = Color.white;

            rightCSPanel.characterPrice.text = "보유중";
        }
        else
        {
            rightCSPanel.characterBuyBtn.interactable = true;
            rightCSPanel.characterBuyBtn.image.color = Color.white;
            rightCSPanel.soldOutMarker.SetActive(false);

            rightCSPanel.characterPrice.text = _characterModel[index + 1].Price.ToString();
        }

        leftCSPanel.saleCharacterImage.SetNativeSize();
        rightCSPanel.saleCharacterImage.SetNativeSize();
    }

    public float LadderWave(float time ,float speed, float pause)
    {
        float period = 2f / speed + pause * 2f;
        float t = time % period;

        float riseTime = 1f / speed;
        if (t < pause) return 1f;
        if (t < pause + riseTime) return 1f - (t - pause) / riseTime;
        if (t < 2 * pause + riseTime) return 0;
        return (t - riseTime - 2 * pause) / riseTime;
    }
    private void MoveSkillDescription()
    {
        if (!CharShopUI.activeSelf)
        {
            descriptionTime = 0f;
            leftSkillView.verticalNormalizedPosition = 1;
            rightSkillView.verticalNormalizedPosition = 1;
            return;
        }

        descriptionTime++;
        descriptionPosition = LadderWave(descriptionTime, descriptionSpeed, descriptionDelayTime);

        leftSkillView.verticalNormalizedPosition = descriptionPosition;
        rightSkillView.verticalNormalizedPosition = descriptionPosition;
    }

}
