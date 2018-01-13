using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLootFactory : MonoBehaviour {


    public GameObject _nuggetPrefab;
    public GameObject _thighPrefab;
    public GameObject _wholePrefab;
    public GameObject _loot;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateChickenLoot()
    {
        Vector2 position = Storage.Instance.LootPosition;

        float range = UnityEngine.Random.Range(0, 100);
        float numberOfLoot = Random.Range(0, 3);
        float totalPoints = 0f;


        
        for (int i = 0; i < numberOfLoot; i++)
        {
            GameObject loot;
            if (range <= 50)
            {
                loot = Instantiate(_nuggetPrefab, position, Quaternion.identity);
                totalPoints += loot.GetComponent<Nugget>().Points; 
            }
            else if (range > 50 && range < 80)
            {
                loot = Instantiate(_thighPrefab, position, Quaternion.identity);
                totalPoints += loot.GetComponent<Thigh>().Points;
            }
            else
            {
                loot = Instantiate(_wholePrefab, position, Quaternion.identity);
                totalPoints += loot.GetComponent<Whole>().Points;

            }

            loot.AddComponent<Rigidbody2D>();
        }

        Storage.Instance.Points += totalPoints;
    
    }
}
