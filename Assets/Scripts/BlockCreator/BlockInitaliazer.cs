using System.Collections.Generic;
using UnityEngine;

public class BlockInitaliazer : MonoBehaviour
{
    private Vector3 _spawnPosPooling = Vector3.zero;

    [Tooltip("Takes Last Blocks Position For Front Blocks")]
    protected Vector3 _lastBlockPos = Vector3.zero;

    [SerializeField]
    protected GameObject[] _deadBlockPrefabs = new GameObject[3];
    protected GameObject _multplierObj;

    //How Much Block Will Spawn
    [SerializeField]
    protected int _blockCount;

    //1-> 2 -> 3 ->
    protected List<GameObject> _deadBlockPool = new List<GameObject>();

    [Header("Hardness For Blocks")]
    private float _difficultyScale = 1;
    private int createBlockIndex;

    public void LoadBlocksAtStartOfGame(int blockCount, GameObject[] deadBlockPrefabs, GameObject multplierObj,int difficultyScale)
    {
        this._difficultyScale = difficultyScale;
        this._blockCount = blockCount;
        this._deadBlockPrefabs = deadBlockPrefabs;
        this._multplierObj = multplierObj;
        InstantiateBlocks();
    }

    protected void InstantiateBlocks()
    {
        //10 Per Block Colour
        //Random Y Pos Down Side Block
        CreateBlocks(_deadBlockPool, _deadBlockPrefabs, _blockCount / 3);
        CalculateDeadBlocksPos(ref _lastBlockPos, _blockCount);
    }

    //First Instantiate Blocks
    protected void CreateBlocks(List<GameObject> blocks, GameObject[] blockWithColor, int howMuchInstantiate)
    {
        //Create Blocks With 2 Part
        //2 loop
        for (int i = 0; i < howMuchInstantiate; ++i)
        {
            for (int j = 0; j < blockWithColor.Length; ++j)
            {
                blocks.Add(Instantiate(blockWithColor[j], _spawnPosPooling, Quaternion.identity));
                Instantiate(blockWithColor[j], _spawnPosPooling + new Vector3(0, Random.Range(-25f +_difficultyScale, -28f +_difficultyScale), 0f), Quaternion.identity, blocks[createBlockIndex].transform).transform.localScale = Vector3.one;

                ++createBlockIndex;
            }
        }

        _multplierObj = Instantiate(_multplierObj, new Vector3(0,500,0), Quaternion.identity);
    }

    
    //Set Posses Of DeadBlocks at Created
    protected void CalculateDeadBlocksPos(ref Vector3 pos, int blockCount)
    {
        blockCount -= 2;

        for (int i = 0; i < blockCount; i += 3)
        {
            _deadBlockPool[i].transform.position = _lastBlockPos;
            _lastBlockPos.y += Random.Range(1.0f, -1.0f);
            _lastBlockPos.z += 1f;

            _deadBlockPool[i + 1].transform.position = _lastBlockPos;
            _lastBlockPos.y += Random.Range(1.0f, -1.0f);
            _lastBlockPos.z += 1f;

            _deadBlockPool[i + 2].transform.position = _lastBlockPos;
            _lastBlockPos.y += Random.Range(1.0f, -1.0f);
            _lastBlockPos.z += 1f;
        }
    }
}
