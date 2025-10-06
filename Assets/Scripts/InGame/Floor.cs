using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class RowTransform
{
    public Transform[] rowTransform;
}

public class Floor : MonoBehaviour
{
    private static readonly int FloorLength = 5;
    private Block[ , ] Blocks = new Block[FloorLength, FloorLength];
    [SerializeField] private RowTransform[] InstantiateLocations = new RowTransform[FloorLength];

    [Header("블록 할당")]
    private IObjectPool<Block> BlockPool;
    [SerializeField] private GameObject BlockPrefab;

    private void Awake()
    {
        BlockPool = new ObjectPool<Block>(CreateBlock, GetBlock, ReleaseBlock, DestroyBlock, maxSize: 25);
    } 

    private void Start()
    {
        ResetStage();
    }

    public void ResetStage()
    {

        int row = 0;
        int col = 0;

        foreach(var colArray in InstantiateLocations)
        {
            foreach(var spawnPoint in colArray.rowTransform)
            {
                Block SpawnBlock;

                BlockType blockType = DataTableManager.Instance.GetRandomBlock(GameManager.Instance.currentStage);
                SpawnBlock = BlockPool.Get();

                SpawnBlock.SetBlockType(blockType);
                SpawnBlock.gameObject.transform.position = spawnPoint.position;
                SpawnBlock.gameObject.GetComponent<SpriteRenderer>().sortingOrder = FloorLength * col + row;
                Blocks[col, row] = SpawnBlock;
                row++;
            }
            row = 0;
            col++;
        }
    }

    public void DestroyLeftBlocks()
    {
        for (int i = 0; i < FloorLength; i++)
        {
            for (int j = 0; j < FloorLength; j++)
            {
                if (Blocks[i, j] != null)
                {
                    BlockPool.Release(Blocks[i, j]);
                }
            }
        }
    }

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
            if (block == null)
                continue;
            if (!(block.GetBlockType() == BlockType.ExplosionBlock || block.GetBlockType() == BlockType.GasBlock))
                return false;
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

    #region 오브젝트 풀링 관련 메서드
    private Block CreateBlock()
    {
        Block block = Instantiate(BlockPrefab).GetComponent<Block>();
        block.SetManagedPool(BlockPool);
        return block;
    }

    private void GetBlock(Block block)
    {
        block.gameObject.SetActive(true);
    }

    private void ReleaseBlock(Block block)
    {
        block.gameObject.SetActive(false);
        block.hasDestroyed = false;
        block.blockDurability = 1;

        for (int i = 0; i< FloorLength; i++)
        {
            for(int j = 0; j< FloorLength; j++)
            {
                if(Blocks[i,j] == block)
                {
                    Blocks[i, j] = null;
                    return;
                }
            }
        }
    }

    private void DestroyBlock(Block block)
    {
        Destroy(block.gameObject);
    }
    #endregion
}
