using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public enum BlockType
{
    CommonBlock,
    ExplosionBlock,
    EnhancedBlock,
    GasBlock,
}

public interface IDestructStrategy
{
    void Destruct(Block block);
}

public class Block : MonoBehaviour, IPointerDownHandler
{
    public int blockDurability = 1;

    [SerializeField] private BlockType blockType;
    private IDestructStrategy destructStrategy;

    public BlockType GetBlockType() { return blockType; }
    public IDestructStrategy GetDestructStrategy()
    {
        return destructStrategy;
    }

    public void SetBlockType(BlockType blocktype)
    {
        this.blockType = blocktype;

        switch (blockType)
        {
            case BlockType.ExplosionBlock:
                destructStrategy = new ExplosionBlockDestructStrategy();
                break;
            case BlockType.EnhancedBlock:
                blockDurability = 2;
                destructStrategy = new EnhancedBlockDestructStrategy();
                break;
            case BlockType.GasBlock:
                destructStrategy = new GasBlockDestructStrategy();
                break;
            default:
                destructStrategy = new CommonBlockDestructStrategy();
                break;
        }

        SetBlockSprite(blockType);
    }

    public void SetBlockSprite(BlockType blockType)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        string spriteName = blockType.ToString();
        string path = "Textures/Blocks/" + spriteName;

        Sprite loadedSprite = Resources.Load<Sprite>(path);

        if (loadedSprite != null)
            spriteRenderer.sprite = loadedSprite;
        else
            Debug.LogError($"Sprite not found at path: {path}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (InGameManager.Instance.GameState != GameState.Playing)
            return;

        if (blockDurability > 0)
        {
            if(PickAx.Instance.gameObject.activeSelf)
                PickAx.Instance.gameObject.SetActive(false);

            blockDurability--;

            SetPickAxActive();

            if(blockDurability <= 0)
                StartCoroutine(DestructProcess());
        }
    }

    public void SetPickAxActive()
    {
        //AudioManager.Instance.PlaySound("PickAxSound");
        Vector3 targetPosition = transform.position;
        PickAx.Instance.transform.position = targetPosition;
        PickAx.Instance.gameObject.SetActive(true);
    }

    private IEnumerator DestructProcess()
    {
        yield return new WaitForSeconds(PickAx.Instance.GetAnimator().GetCurrentAnimatorStateInfo(0).length);
         
        destructStrategy.Destruct(this);
    }
}
