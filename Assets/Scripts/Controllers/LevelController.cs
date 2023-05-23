using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelContainer _levelContainer;
    [SerializeField] private List<float> yCoordinatLines;

    [Inject] private ShootController _shootController;
    [Inject] private UIController _uiController;
    
    private int _enemyOnLevelCount;
    private LevelData _currentLevelData;
    private Transform _level;
    private List<Enemy> _enemies;
    

    private void Awake()
    {
        _enemies = new List<Enemy>();
        _level = new GameObject("LevelContainer").transform;
        LoadLevelData();
    }

    private void LoadLevelData()
    {
        StopAllCoroutines();
        _currentLevelData = _levelContainer.NextLevel(_currentLevelData);
        _enemyOnLevelCount = 0;
        int indexLine = 0;
        foreach (var enemyTypeData in _currentLevelData._enemies)
        {
            _enemyOnLevelCount += enemyTypeData.count;
            Vector3 pos = new Vector3(0, yCoordinatLines[indexLine]);
            Transform enemiesLine = Instantiate(enemyTypeData.prefab, pos, Quaternion.identity, 
                _level).transform;
            int countItems = enemiesLine.GetChild(0).childCount;
            for (int i = 0; i < countItems; i++)
            {
                enemiesLine.GetChild(0).GetChild(i).GetComponent<Enemy>().Init(this);
            }

            indexLine++;

        }

        StartCoroutine(RandoMAlienShoot());
    }

    public void KillOneAlien(Enemy alien)
    {
        _uiController.AddScore(alien.score);
        _enemyOnLevelCount--;
        _enemies.Remove(alien);
        Destroy(alien.gameObject);
        if (_enemyOnLevelCount <= 0)
        {
            _enemies.Clear();
            ClearLevelContainer();
            LoadLevelData();
            _uiController.AddLevel();
        }
    }

    private void ClearLevelContainer()
    {
        int count = _level.childCount;
        for (int i = 0; i < count; ++i)
        {
            Destroy(_level.GetChild(i).gameObject);
        }
    }

    public void AddToList(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    private IEnumerator RandoMAlienShoot()
    {
        int index = Random.Range(0, _enemyOnLevelCount);
        _shootController.EnemyShoot(_enemies[index].transform.position);
        yield return new WaitForSeconds(2f);
        StartCoroutine(RandoMAlienShoot());
    }
}
