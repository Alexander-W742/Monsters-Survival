using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mc;
    public GameObject targetMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mc = FindAnyObjectByType<MapController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mc.currentChunck = targetMap;
        }
    }

    private void OldOnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mc.currentChunck = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (mc.currentChunck == targetMap)
            {
                mc.currentChunck = null;
            }
        }
    }
}
