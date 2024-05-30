using System.Collections;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;
using UnityEngine;

namespace TwitchCommandPack.Commands;

public class ScreenFlipCommand : ITwitchCommand {
    public static bool isEnabled = false;
    public static float timeInSeconds = 5;
    public Player player;

    public string CommandFeedback(string _user, string[] _command) {
        if (!isEnabled) {
            return $"{_user} is our doctor!";
        }

        return $"{_user} makes everyone dizzy...";
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        player = Player.FindMainPlayer();

        isEnabled = true;

        player.uAntPlayer.StartCoroutine(ExecuteCountdown().WrapToIl2Cpp());

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isScreenFlipCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        if (_command.Length != 3) {
            return false;
        }

        string firstWord = _command[1];
        string secondWord = _command[2];

        if (firstWord != "screen" || secondWord != "flip") {
            return false;
        }

        return true;
    }

    protected IEnumerator ExecuteCountdown() {
        yield return new WaitForSeconds(timeInSeconds);

        isEnabled = false;
    }
}