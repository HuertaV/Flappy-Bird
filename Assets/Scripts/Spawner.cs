using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;
    public int recordedScore;
    public GameManager GM;

    private void Start()
    {
        spawnRate = 1f;
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
        StopCoroutine("SpeedUp");
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }

    public void DiffRamp()
    {
        StartCoroutine("SpeedUp");
        CancelInvoke(nameof(Spawn));
    }

    IEnumerator SpeedUp()
    {

        while (GM.gameActive)
        {
            recordedScore = GM.score;
            if (recordedScore < 40)
                spawnRate *= Mathf.Pow(.9975f, recordedScore - 10);
            yield return new WaitForSeconds(spawnRate);
            if (GM.gameActive)
            {
                Spawn();
            }
        }

    }
    public void Stop()
    {

        Debug.Log("Spedd Up Ended. fdjkhfedghjkfdhjk");
    }

    //public void SetLoop()
    //{
    //    InvokeRepeating(nameof(DiffRamp), 1, 1);
    //}



}
