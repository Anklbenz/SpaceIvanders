using UnityEngine;
using UnityEngine.UI;

public class GameBoot : MonoBehaviour
{
  [Header("Player")]
  [SerializeField] private Player playerPrefab;
  [SerializeField] private Transform playerCreatePoint, poolParent;
  [SerializeField] private float playerSpeed, shotDelay;
  [SerializeField] private ShipInputReader inputObj;
  [SerializeField] private Bullet bulletPrefab;

  [Header("EnemiesSystem")]
  [SerializeField] private EnemiesHandler enemyPrefab;
  [SerializeField] private Transform enemyCreatePoint;

  [Header("UI")]
  [SerializeField] private GameObject startGameScreen,endGameScreen ;
  [SerializeField] private Text scoreText,liveText,winText, loseText;

  [Header("Game")]
  [SerializeField] private int livesCount;
  [SerializeField] private DeadZone deadZone;
  [SerializeField] private BonusUfo bonusPrefab;
  [SerializeField] private Transform bonusCreatePoint, bunkerCreatePoint;
  [SerializeField] private GameObject bunkersPrefab;

  [Header("GameField")]
  [SerializeField] private Camera cam;
  [SerializeField] private BoxCollider2D boundaryLeft, boundaryRight;
  
  private Player _player;
  private GameObject _bunkers;
  private UIDrawer _uiDrawer;
  private GameLoop _gameLoop;
  private EnemiesHandler _enemiesHandler;
  private BoundariesHandler _boundariesHandler;

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
    
    _gameLoop.StatsHandler.LiveIsOutEvent -= EndGame;
    _gameLoop.DeadZone.DeadZoneEnterEvent -= EndGame;
    _gameLoop.AllEnemiesDestroyedEvent -= EndGame;
    _gameLoop.Dispose();

    winText.enabled = gameResult;
    loseText.enabled = !gameResult;
    
    endGameScreen.SetActive(true);
  }
}