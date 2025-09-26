using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIData : BaseUIData
{
    //public bool IsPossesed { get; set; }
}
public class ShopUI : BaseUI
{
    // 캐릭터 정보 
    private CharacterModel[] _characterModel;

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



    private void Awake()
    {
        _characterModel = DataTableManager.Instance.GetAllCharacterModelData();
       
        UpdatePageData(currentPageIndex);
        UpdatePageNumText();
        Debug.Log($"현재 저장된 캐릭터 정보 수는 {_characterModel.Length} 입니다.");
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

    private void UpdatePageData(int currentPageIndex)
    {
        int index = currentPageIndex * 2;

        left_SaleCharacterImage.sprite = Resources.Load<Sprite>($"Textures/{_characterModel[index].ID}");
        right_SaleCharacterImage.sprite = Resources.Load<Sprite>($"Textures/{_characterModel[index + 1].ID}");

        leftCharacterName.text = _characterModel[index].Name;
        rightCharacterName.text = _characterModel[index + 1].Name;

        leftSkillName.text = _characterModel[index].SkillName;
        rightSkillName.text = _characterModel[index + 1].SkillName;

        leftSkillDetail.text = _characterModel[index].SkillDescription;
        rightSkillDetail.text = _characterModel[index + 1].SkillDescription;

        leftCharacterPrice.text = _characterModel[index].Price.ToString();
        rightCharacterPrice.text = _characterModel[index + 1].Price.ToString();

    }

}
