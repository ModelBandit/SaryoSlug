using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    public SpineAnimatorController Player;
    [SerializeField]
    public BoxCollider2D PlayerBound;

    [SerializeField]
    public BoxCollider2D Collider;


    public List<IEnumerator> m_UpdateList;

    private void Awake()
    {
        m_UpdateList = new List<IEnumerator>();
        m_UpdateList.Add(MainUpdate());
    }
    private void Update()
    {
        for (int i = 0; i < m_UpdateList.Count; ++i)
        {
            if (m_UpdateList[i].MoveNext() == false)
                m_UpdateList.RemoveAt(i--);
        }

    }

    private IEnumerator MainUpdate()
    {
        while (true)
        {
            if (CollisionCheck())
            {
                Player.StopJump();
                Player.AnimationOverrideClear(2);
            }
            yield return null;
        }
    }

    private bool CollisionCheck()
    {
        if (PlayerBound.bounds.Intersects(Collider.bounds))
            return true;
        return false;
    }
}
