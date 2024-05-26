using System;
using UnityEngine;

public class LevelDataHandler : MonoBehaviour {
    static public LevelDataHandler instance;
    static private readonly int _numberOfLevels = Constants.NUMBER_OF_LEVELS;
    static private readonly bool[] _levelStates = new bool[_numberOfLevels];

    void Awake() {
        if (instance && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        readUnlockedLevels();
    }

    static public void unlockLevel(int levelID) {
        if (levelID >= _levelStates.Length) {
            ErrorHandling.logError("levelID is greater than levelStates.Length");
            return;
        }
        _levelStates[levelID] = true;
    }

    static public void writeUnlockedLevels() {
        string states = "";
        foreach (bool state in _levelStates) {
            states += Convert.ToInt32(state).ToString();
        }
        DataPersistenceManager.save(Constants.PREF_LEVEL_STATES, states);
    }

    static public void readUnlockedLevels() {
        string unlocked = DataPersistenceManager.loadString(Constants.PREF_LEVEL_STATES); // binary string where each char represents lock state of level
        _levelStates[0] = true;
        int i;
        for (i = 1; i < Mathf.Min(_numberOfLevels, unlocked.Length); i++) {
            _levelStates[i] = Convert.ToBoolean(char.GetNumericValue(unlocked[i]));
        }
        while (i < _numberOfLevels) {
            _levelStates[i] = false;
            i++;
        }
    }

    static public int getLevelCount() {
        return _numberOfLevels;
    }

    static public bool getLevelState(int levelID) {
        return _levelStates[levelID - 1];
    }

    void OnApplicationQuit() {
        writeUnlockedLevels();
    }
}
