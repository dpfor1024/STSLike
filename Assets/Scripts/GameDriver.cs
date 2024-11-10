using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;



public class GameDriver : MonoBehaviour
{
    public CardBank StartDeck;//ÆðÊ¼ÅÆ

    [Header("Manager")]
    [SerializeField] private CardManager cardManager;
    [SerializeField] private CardDeckManager cardDeckManager;
    [SerializeField] private CardDisplayManager cardDisplayManager;
    
    private List<CardBase> _playerDeck = new List<CardBase>();
    private List<GameObject> _enemys = new List<GameObject>();

    [Header("Character")]
    [SerializeField] private Transform enemyPoint;


    [SerializeField] private AssetReference enemyBase;



    private void Start()
    {
        cardManager.Init();
        CreatePlayer();
        CreateEnemy(enemyBase);
    }

    private void CreatePlayer()
    {
        foreach (var item in StartDeck.Items) { 
            for(int i = 0; i < item.Amount; i++)
            {
                _playerDeck.Add(item.CardBase);
            }
        }

        Init();
    }

    public void Init()
    {
        cardDisplayManager.Init(cardManager);
        cardDeckManager.LoadDeck(_playerDeck);
        cardDeckManager.ShuffleDeck();
        cardDeckManager.DrawCard(5);
    }


    private void CreateEnemy(AssetReference assetReference)
    {
        var handle = Addressables.LoadAssetAsync<EnemyBase>(assetReference);
        handle.Completed += operationResult =>
        {
            var tem = operationResult.Result;
            var enemy = Instantiate(tem.Prefab, enemyPoint);
            Assert.IsNotNull(enemy);
            enemy.gameObject.GetComponent<CharacterObject>().CharacterBase = tem;
            _enemys.Add(enemy);
        };

    }
}
