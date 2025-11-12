using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockDictionaryUI : MonoBehaviour
{
    [SerializeField] private GameObject _contentPosition;
    [SerializeField] private GameObject _blockInfoBtnPrefab;

    [Header("Block Description UI")]
    [SerializeField] private Image blockTypeImage; // 블록이 위험한 블록인지 아닌지에 따라 배경 색이 빨간색 <-> 회색으로 변경
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI blockDescription;
    [SerializeField] private TextMeshProUGUI blockName;

    // 블록도감을 다시 활성화 할 때 첫 버튼이 눌린 채로 나오기 위해 사용되는 변수
    private BlockInfoBtn firstBtn;

    // 클릭한 버튼을 표시하기 위한 요소
    private Image clickedBtnImage;
    private readonly Color clickedColor = new Color(1, 1, 1, 0);
    private readonly Color unClickedColor = Color.white;

    private void Awake()
    {
        foreach(var blockData in DataTableManager.Instance.GetAllBlockInfoData())
        {
            var blockInfoBtnObj = Instantiate(_blockInfoBtnPrefab, _contentPosition.transform);
            var blockInfoBtn = blockInfoBtnObj.GetComponent<BlockInfoBtn>();
            blockInfoBtn.setDescription.AddListener((id, btnImage) => OnClickShowBlockInfo(id, btnImage));
            blockInfoBtn.SetContent(blockData.ID);


            // 블록 사전을 처음 열 때 첫 번째 버튼이 눌려있는 채로 시작하기 위한 작업
            if (blockData.ID == "B_1")
            {
                firstBtn = blockInfoBtn;
                firstBtn.OnClickShowDescription();
            }
            
        }
    }

    private void OnEnable()
    {
        firstBtn.OnClickShowDescription();
    }

    public void OnClickShowBlockInfo(string id, Image btnImage)
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        Debug.Log("블록 세부 설명 출력");

        BlockInfoModel blockInfoData = DataTableManager.Instance.GetBlockInfoData(id);
        if (clickedBtnImage != null)
        {
            clickedBtnImage.color = unClickedColor;
        }

        clickedBtnImage = btnImage;
        clickedBtnImage.color = clickedColor;


        blockTypeImage.sprite = Resources.Load<Sprite>($"Textures/BlockUI/Icon/{blockInfoData.Type}");
        blockImage.sprite = Resources.Load<Sprite>($"Textures/BlockUI/Icon/{blockInfoData.ID}");
        blockDescription.text = blockInfoData.Description;
        blockName.text = blockInfoData.Name;
    }
}
