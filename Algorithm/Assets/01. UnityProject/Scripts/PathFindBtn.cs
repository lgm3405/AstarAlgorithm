using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindBtn : MonoBehaviour
{
    //! A star Find ��ư�� ���� ���
    public void OnClickAstarFindBtn()
    {
        PathFinder.Instance.FindPath_Astar();
    }        // OnClickAstarFindBtn()
}
