using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Monosingleton<GameManager>
{
    [SerializeField]
    public CharacterController Player;
    [SerializeField]
    public BoxCollider2D Collider;

    public bool isPlaying = false;

    public List<IEnumerator> m_UpdateList;

    private void Awake()
    {
        isPlaying = true;

        m_UpdateList = new List<IEnumerator>();
        m_UpdateList.Add(MainUpdate());
    }


    [HideInInspector]
    public float deltaTime = 0f;
    private void Update()
    {
        deltaTime = Time.deltaTime;
        for (int i = 0; i < m_UpdateList.Count; ++i)
        {
            if (m_UpdateList[i].MoveNext() == false)
                m_UpdateList.RemoveAt(i--);
        }

        InputManager.Instance.InputUpdate();

    }
    
    private IEnumerator MainUpdate()
    {
        while (true)
        {
            if (CollisionCheck())
            {
                Player.StopJump();
            }
            yield return null;
        }
    }
    public void AddUpdate(IEnumerator updater)
    {
        m_UpdateList.Add(updater);
    }

    private bool CollisionCheck()
    {
        if (Player.FootCollider.bounds.Intersects(Collider.bounds))
            return true;
        return false;
    }
}
