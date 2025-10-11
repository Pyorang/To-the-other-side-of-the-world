using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BlockInfoBtn : MonoBehaviour
{
    public Image blockTypeColorImage;
    [SerializeField] private Image blockImage;

    [HideInInspector]
    public UnityEvent<string, Image> setDescription;

    private string id;

    public void SetContent(string id)
    {
        this.id = id;
        BlockInfoModel blockInfoData = DataTableManager.Instance.GetBlockInfoData(id);
        blockTypeColorImage.sprite = Resources.Load<Sprite>($"Textures/BlockUI/Icon/{blockInfoData.Type}");
        blockImage.sprite = Resources.Load<Sprite>($"Textures/BlockUI/Icon/{blockInfoData.ID}");   
    }

    public void OnClickShowDescription()
    {
        setDescription.Invoke(id, blockTypeColorImage);
    }
}
