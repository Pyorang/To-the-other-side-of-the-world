using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class BlockDictionaryUI : MonoBehaviour
{
    [SerializeField] private GameObject _contentPosition;
    [SerializeField] private GameObject _blockInfoBtnPrefab;

    [Header("블록 세부설명 요소")]
    [SerializeField] private Image blockTypeImage; // 블록이 위험한 블록인지 아닌지에 따라 배경 색이 빨간색 <-> 회색으로 변경
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI blockDescription;
    [SerializeField] private TextMeshProUGUI blockName;

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
            blockInfoBtn.setDescription.AddListener((blockInfoData, btnImage) => OnClickShowBlockInfo(blockInfoData, btnImage));
            blockInfoBtn.SetContent(blockData.ID);


            // 블록 사전을 처음 열 때 첫 번째 버튼이 눌려있는 채로 시작하기 때문에, 눌려있는 버튼 이미지를 저장함
            if (blockData.ID == "B_1")
            clickedBtnImage = blockInfoBtn.blockTypeColorImage;
        }
    }

    public void OnClickShowBlockInfo(BlockInfoModel blockInfoData, Image btnImage)
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        Debug.Log("블록 세부 설명 출력");

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
