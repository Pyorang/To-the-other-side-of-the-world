using UnityEngine;
using UnityEngine.UI;

public class BackGround : SingletonBehaviour<BackGround>
{
    [SerializeField] private float OffSetY;
    [SerializeField] private float EndPositionY;
    [SerializeField] private Vector3 ResetPosition;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();
    }

    public void MoveImageDown()
    {
        this.gameObject.transform.position += new Vector3(0, OffSetY, 0);

        if(this.gameObject.transform.position.y >= EndPositionY)
        {
            this.gameObject.transform.position = ResetPosition;
        }
    }
}
