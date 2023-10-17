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

    IEnumerator SpeedUp() {
        while (this.enabled)
        {
            recordedScore = GM.GetComponent<GameManager>().score;
            spawnRate *= Mathf.Pow(.995f, recordedScore - 10);
            yield return new WaitForSeconds(spawnRate);
            Spawn();
        }
    }

    //public void SetLoop()
    //{
    //    InvokeRepeating(nameof(DiffRamp), 1, 1);
    //}



}
