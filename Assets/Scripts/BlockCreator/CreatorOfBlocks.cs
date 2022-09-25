using UnityEngine;

public class CreatorOfBlocks : BlockInitaliazer
{
    //Sington
    public static CreatorOfBlocks Instance;

    //Takes Blocks at Behind
    private int unbusyBlockIndex = 0;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    //Forward Block
    //Take Next Pos Of Block When Joint Want to Grab for players Current Pos At Z axis
    public Transform PosTakeForPlayerForwardBlock(int playerPosZ)
    {
        return _deadBlockPool[playerPosZ].transform;
    }

    //Object Pooling Method
    public void UpdatePosForFront()
    {
        for (int i = 0; i < 10; ++i)
        {
            //Positions Update
            _lastBlockPos.y += Random.Range(-1.2f, 1.2f);

            //Behinds Blocks Goes Front
            _deadBlockPool[unbusyBlockIndex].transform.position = _lastBlockPos;

            ++unbusyBlockIndex;

            if (unbusyBlockIndex > _deadBlockPool.Count - 1)
            {
                ScoreMultiplierCreate(_lastBlockPos);
                unbusyBlockIndex = 0;
            }

            _lastBlockPos.z += 1;
        }
    }

    //Change Pos
    //Vector 3 Value Type Struct
    private void ScoreMultiplierCreate(Vector3 pos)
    {
        pos.y -= Random.Range(9.5f, 10.5f);
        _multplierObj.transform.position = pos;

        _multplierObj.SetActive(true);
    }
}
