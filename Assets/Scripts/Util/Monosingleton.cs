using UnityEngine;

public class Monosingleton<T>: MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance = null;

    public static T Instance
    {
        get
        {
            m_instance = (T)FindObjectOfType(typeof(T));
            if (m_instance == null)
                m_instance = (T)FindObjectOfType(typeof(T));

            if(m_instance == null)
            {
                GameObject go = new GameObject();
                m_instance = go.AddComponent<T>();
                go.name = typeof(T).Name;
            }

            return m_instance;
        }
    }

    private void Awake()
    {
        if(FindObjectOfType(typeof(T)) != null &&
            FindObjectOfType(typeof(T)) != this)
        {
            Destroy(this.gameObject);
        }
    }
}