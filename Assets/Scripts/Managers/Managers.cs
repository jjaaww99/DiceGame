using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;

    public static Managers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Managers>();

                if (instance == null)
                {
                    GameObject gameObject = new GameObject("Managers");
                    instance = gameObject.AddComponent<Managers>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
