using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] List<Transform> GoldLines = new List<Transform>();

    private void GoldActive()
    {
        var rnd = Random.Range(0, 7);

        if (rnd > 1 && rnd < 5)
        {
            for (int i = 0; i <= (rnd % 3); i++)
            {
                GoldLines[i].gameObject.SetActive(true);

                foreach (Transform child in GoldLines[i])
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
    private void GoldInActive()
    {
        for(int i = 0; i < GoldLines.Count; i++)
        {
            foreach (Transform child in GoldLines[i])
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoldInActive();
            transform.position = WayManager.instance.NewPosition();

            GoldActive();

        }
    }
}
