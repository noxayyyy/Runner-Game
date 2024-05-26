static public class Constants {
    // Scene Names
    public const string SCENE_SPLASH = "Splash";
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_LEVEL_SELECT = "LevelSelect";
    public const string SCENE_GAMEPLAY = "Gameplay";

    // Player Prefs
    public const int NUMBER_OF_LEVELS = 10;
    public const string PREF_LEVEL_NUMBER = "levelNumber";
    public const string PREF_LEVEL_STATES = "levelStates";
    public const string PREF_COINS = "Coins";

    // Object Tags
    public const string TAG_PLAYER = "Player";
    public const string TAG_PLANE = "Plane";
    public const string TAG_FINISH_LINE = "FinishLine";

    // Project Layers
    public const int LAYER_DEFAULT = 0;
    public const int LAYER_PREVIEW = 3;
    public const int LAYER_UI = 5;
    
    // Coins
    public const int COINS_VALUE_EACH = 5; // amount of coins added per coin picked up

    // Player Movement Speeds
    public const float PLAYER_GRAVITY = 10.0f; // acceleration
    public const float PLAYER_AUTOSPEED_Z = 10.0f;
    public const float PLAYER_AUTOSPEED_X = 3.0f;

    // Player Animation Timers
    public const float PLAYER_ANIM_STOP_TIME = 2.5f;
    public const float PLAYER_ANIM_ROTATE_TIME = 2.0f;
    public const float PLAYER_ANIM_CELEBRATION_TIME = 3.0f;

    // Animator Variables
    public const string ANIMATOR_FLOAT_BLEND = "Blend";
    public const string ANIMATOR_BOOL_STOPPED = "isStopped";
    public const string ANIMATOR_BOOL_LEVELCOMPLETE = "levelComplete";
}
