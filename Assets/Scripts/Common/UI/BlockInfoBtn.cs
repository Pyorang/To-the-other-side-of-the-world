using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BlockInfoBtn : MonoBehaviour
{
    [Header("버튼 요소")]
    public Image blockTypeColorImage;
    [SerializeField] private Image blockImage;

    public UnityEvent<BlockInfoModel, Image> setDescription;

    private BlockInfoModel blockInfoData;

    private readonly Color clickedColor = new Color(1, 1, 1, 0);

    public void SetContent(string id)
    {
        blockInfoData = DataTableManager.Instance.GetBlockInfoData(id);
        blockTypeColorImage.sprite = Resources.Load<Sprite>($"Textures/BlockUI/Icon/{blockInfoData.Type}");
        blockImage.sprite = Resources.Load<Sprite>($"Textures/BlockUI/Icon/{blockInfoData.ID}");

        // 처음에 블록 사전을 열 때 첫 블록에 대한 정보가 보여지기 때문에 첫 버튼이 눌려있는거나 마찬가지므로 눌려있다는 표현을 해주는 동작
        if (id == "B_1") blockTypeColorImage.color = clickedColor;
        
    }

    public void OnClickShowDescription()
    {
        setDescription.Invoke(blockInfoData, blockTypeColorImage);
    }
}
