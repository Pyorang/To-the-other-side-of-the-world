using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

[Serializable]
public class RowBlock
{
    public Block[] rowBlock;
}

public class Floor : MonoBehaviour
{
    private static readonly int FloorLength = 5;
    [SerializeField] private RowBlock[] Blocks = new RowBlock[FloorLength];

    private void Start()
    {
        ResetStage();
    }

    public void ResetStage()
    {
        foreach(var colBlock in Blocks)
        {
            foreach(var rowBlock in colBlock.rowBlock)
            {
                rowBlock.blockDurability = 1;
                rowBlock.gameObject.SetActive(true);
                BlockType blockType = DataTableManager.Instance.GetRandomBlock(GameManager.Instance.currentStage);
                rowBlock.SetBlockType(blockType);
            }
        }
    }

    public void DestroyLeftBlocks()
    {
        foreach(var colBlock in Blocks)
        {
            foreach(var rowBlock in colBlock.rowBlock)
            {
                if (rowBlock.gameObject.activeSelf == true)
                {
                    rowBlock.gameObject.SetActive(false);
                }
            }
        }
    }

    public Block[] GetNearBlocks(Block block)
    {
        for (int i = 0; i < FloorLength; i++)
        {
            for (int j = 0; j < FloorLength; j++)
            {
                if (Blocks[i].rowBlock[j] == block)
                {
                    Block[] nearBlocks = new Block[4];
                    nearBlocks[0] = (i > 0) ? Blocks[i - 1].rowBlock[j] : null; // Up
                    nearBlocks[1] = (i < FloorLength - 1) ? Blocks[i + 1].rowBlock[j] : null; // Down
                    nearBlocks[2] = (j > 0) ? Blocks[i].rowBlock[j - 1] : null; // Left
                    nearBlocks[3] = (j < FloorLength - 1) ? Blocks[i].rowBlock[j + 1] : null; // Right
                    return nearBlocks;
                }
            }
        }

        return null;
    }

    #region 스테이지 클리어 관련 메서드
    public void ProcessStageClear()
    {
        if(CheckStageClear())
        {
            GameManager.Instance.currentStage++;
            StartCoroutine(AdjustNextStage());
        }
    }

    public bool CheckStageClear()
    {
        foreach(var block in Blocks)
        {
            foreach(var rowBlock in block.rowBlock)
            {
                if (rowBlock.gameObject.activeSelf == false)
                    continue;
                if (!(rowBlock.GetBlockType() == BlockType.ExplosionBlock || rowBlock.GetBlockType() == BlockType.GasBlock))
                    return false;
            }
        }

        DestroyLeftBlocks();
        return true;
    }

    private IEnumerator AdjustNextStage()
    {
        //NOTE : 스테이지 초기화하는 시간으로 재설정해야함. 임의로 설정한 값
        yield return new WaitForSeconds(2f);

        ResetStage();
    }
    #endregion
}
