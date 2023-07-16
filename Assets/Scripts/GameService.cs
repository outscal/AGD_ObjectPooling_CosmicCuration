#region Namespaces
using UnityEngine;
using TMPro;
using CosmicCuration.Audio;
using CosmicCuration.Enemy;
using CosmicCuration.Bullets;
using CosmicCuration.VFX;
using CosmicCuration.Player;
using CosmicCuration.UI;
using CosmicCuration.Utilities;
using CosmicCuration.PowerUps;
using MainMenuUI;
#endregion


public class GameService : GenericMonoSingleton<GameService>
{
    public DifficultyLevel difficultyLevel;

    #region Dependencies

    private PlayerService playerService;
    private EnemyService enemyService;
    private PowerUpService powerUpService;
    private VFXService vfxService;
    private SoundService soundService;
    [SerializeField] private UIView uiService;
    [SerializeField] private MainMenu mainMenu;

    #endregion

    #region Prefabs
    [SerializeField] private PlayerView playerPrefab;
    [SerializeField] private BulletView playerBulletPrefab;
    [SerializeField] private EnemyView enemyPrefab;
    #endregion

    #region Scriptable Objects
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    [SerializeField] private BulletScriptableObject playerBulletScriptableObject;
    [SerializeField] private EnemyScriptableObject enemyScriptableObject;
    [SerializeField] private PowerUpScriptableObject powerUpScriptableObject;
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private VFXScriptableObject vfxScriptableObject;
    #endregion

    #region Scene References
    [SerializeField] private AudioSource audioEffectSource;
    [SerializeField] private AudioSource backgroundMusicSource;
    #endregion

    private void Start()
    {
        // Initialize all Services.
        soundService = new SoundService(soundScriptableObject, audioEffectSource, backgroundMusicSource);
        playerService = new PlayerService(playerPrefab, playerScriptableObject, playerBulletPrefab, playerBulletScriptableObject);
        powerUpService = new PowerUpService(powerUpScriptableObject);
        enemyService = new EnemyService(enemyPrefab, enemyScriptableObject);
        vfxService = new VFXService(vfxScriptableObject);
    }

    private void Update()
    {
        powerUpService?.Update();
        enemyService?.Update();
    }

    #region Getters
    public PlayerService GetPlayerService() => playerService;

    public EnemyService GetEnemyService() => enemyService;

    public PowerUpService GetPowerUpService() => powerUpService;

    public VFXService GetVFXService() => vfxService;

    public SoundService GetSoundService() => soundService;

    public UIView GetUIService() => uiService;

    public MainMenu GetMainMenu() => mainMenu;
    #endregion

    public void SetDifficultyVariables()
    {
        switch (difficultyLevel)
        {
            case DifficultyLevel.easy:
                playerScriptableObject.movementSpeed = 3;
                playerScriptableObject.rotationSpeed = 250f;
                break;

            case DifficultyLevel.medium:
                playerScriptableObject.movementSpeed = 5;
                playerScriptableObject.rotationSpeed = 350f;
                break;

            case DifficultyLevel.hard:
                playerScriptableObject.movementSpeed = 7;
                playerScriptableObject.rotationSpeed = 500f;
                break;

            default:
                playerScriptableObject.movementSpeed = 3;
                playerScriptableObject.rotationSpeed = 250f;
                break;
        }
    }

}
