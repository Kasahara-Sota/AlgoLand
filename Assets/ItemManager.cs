using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject[] Items;
    [SerializeField] Transform[] _players;
    [SerializeField] Transform _floor;
    HashSet<Transform> _tiles;
    public static ItemManager Instance;
    void Start()
    {
        Instance = this;
        _tiles = new HashSet<Transform>();
        for (int i = 0; i < _floor.childCount; i++)
        {
            _tiles.Add(_floor.GetChild(i));
        }
        var cts = new CancellationTokenSource();
        GenerateInterval(cts);
    }
    async UniTask GenerateInterval(CancellationTokenSource cts)
    {
        while(true)
        {
            GenerateItem();
            await UniTask.Delay(5000);
        }
    }
    public void GenerateItem()
    {
        HashSet<Transform>[] p = new HashSet<Transform>[_players.Length];
        for (int i = 0; i < _players.Length; i++)
        {
            Collider[] hits = Physics.OverlapSphere(_players[i].position, 3, LayerMask.GetMask("Floor"));
            p[i] = new HashSet<Transform>();
            for (int j = 0; j < hits.Length; j++)
            {
                p[i].Add(hits[j].transform);
            }
        }
        for (int i = 1; i < _players.Length; i++)
        {
            p[0].UnionWith(p[i]);
        }
        p[0].SymmetricExceptWith(_tiles);
        Transform[] t = p[0].Where(x => x.childCount == 0).ToArray();
        if(t.Length == 0 || t.Length < _tiles.Count / 2)
        {
            return;
        }
        int itemIndex = Random.Range(0, Items.Length);
        int tileIndex = Random.Range(0, t.Length);
        GameObject obj = Instantiate(Items[itemIndex], t[tileIndex].position + Vector3.up, Quaternion.identity);
        obj.transform.SetParent(t[tileIndex]);
    }
}
