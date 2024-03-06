using UnityEngine;

public abstract class ABehaviorTree : MonoBehaviour
{
    private ANode _root;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private EnemyInfoManager _enemyInfo;
    [SerializeField] private PlayerInfoManager _playerInfo;
    [SerializeField] private WorldInfoManager _worldInfo;
    [SerializeField] private string _currentStateName;

    public virtual void Start()
    {
        _root = SetupTree();
        _enemyController.SetInfo(_enemyInfo, _playerInfo, _worldInfo);
        _root.SetControllerAndInfo(_enemyController, _enemyInfo, _playerInfo, _worldInfo);
    }

    public virtual void Update()
    {
        if (_root != null)
        {
            _root.Evaluate();
        }
        _currentStateName = _enemyInfo.GetCurrentStateName();
    }

    protected abstract ANode SetupTree();
}