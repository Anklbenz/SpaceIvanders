using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
  [Header("Player")]
  [SerializeField] private Player playerPrefab;
  [SerializeField] private Transform playerCreatePoint, poolParent;
  [SerializeField] private float playerSpeed, shotDelay;
  [SerializeField] private ShipInputReader inputObj;
  [SerializeField] private Bullet bulletPrefab;
  private Player _player;

  [Header("EnemiesSystem")]
  [SerializeField] private EnemiesHandler enemyPrefab;
  [SerializeField] private Transform enemyCreatePoint;
  private EnemiesHandler _enemiesHandler;

  [Header("UI")]
  [SerializeField] private GameObject startGameScreen,endGameScreen ;
  [SerializeField] private Text scoreText,liveText,winText, loseText;
  private UIDrawer _uiDrawer;

  [Header("Game")]
  [SerializeField] private int livesCount;
  [SerializeField] private DeadZone deadZone;
  [SerializeField] private BonusUfo bonusPrefab;
  [SerializeField] private Transform bonusCreatePoint, bunkerCreatePoint;
  [SerializeField] private GameObject bunkersPrefab;
  private GameObject _bunkers;

  [Header("GameField")]
  [SerializeField] private Camera cam;
  [SerializeField] private BoxCollider2D boundaryLeft, boundaryRight;
  private BoundariesHandler _boundariesHandler;
  private GameLoop _gameLoop;

  private void Awake(){
    _boundariesHandler = new BoundariesHandler(boundaryLeft, boundaryRight, cam);
    _boundariesHandler.SetBoundary();
    _uiDrawer = new UIDrawer(liveText, scoreText);
  }

  private EnemiesHandler CreateEnemy(){
    return Instantiate(enemyPrefab, enemyCreatePoint.position, Quaternion.identity);
  }

  private Player CreatePlayer(){
    var player = Instantiate(playerPrefab, playerCreatePoint.position, Quaternion.identity);
    player.Initialize(inputObj, playerSpeed, shotDelay, poolParent, bulletPrefab);
    return player;
  }

  private GameObject CreateBunkers(){
    return Instantiate(bunkersPrefab, bunkerCreatePoint.position, Quaternion.identity);
  }

  public void StartGame(){
    startGameScreen.SetActive(false);
    endGameScreen.SetActive(false);

    _player = CreatePlayer();
    _bunkers = CreateBunkers();
    _enemiesHandler = CreateEnemy();
    _gameLoop = new GameLoop(_enemiesHandler, _player, deadZone, bonusPrefab, bonusCreatePoint, livesCount);
    _uiDrawer.StatConnect(_gameLoop.StatsHandler);
    _gameLoop.StatsHandler.LabelsRefresh();
    
    _gameLoop.StatsHandler.LiveIsOutEvent += EndGame;
    _gameLoop.DeadZone.DeadZoneEnterEvent += EndGame;
    _gameLoop.AllEnemiesDestroyedEvent += EndGame;
  }
  
  private void EndGame(bool gameResult){
    Destroy(_player.gameObject);
    Destroy(_enemiesHandler.gameObject);
    Destroy(_bunkers.gameObject);
    _gameLoop.Dispose();

    winText.enabled = gameResult;
    loseText.enabled = !gameResult;
    
    endGameScreen.SetActive(true);
  }
}