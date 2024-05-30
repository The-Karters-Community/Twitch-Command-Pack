using System.Collections;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;
using UnityEngine;

namespace TwitchCommandPack.Commands;

public class FreezeCommand: ITwitchCommand {
    public static bool isEnabled = false;
    public static float timeInSeconds = 5;

    public Player player;

    public string CommandFeedback(string _user, string[] _command) {
        string feedback = $"{_user} transformed {player.GetName()} into a statue!";

        return feedback;
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        player = Player.FindMainPlayer();

        isEnabled = true;
        
        player.uPixelKartPhysics.kartController.StartCoroutine(this.ExecuteCountdown().WrapToIl2Cpp());

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isFreezeCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        int amountOfTerms = _command.Length - 1;

        if (amountOfTerms != 1) {
            return false;
        }

        string firstTerm = _command[1];

        return firstTerm == "freeze";
    }

    protected IEnumerator ExecuteCountdown() {
        yield return new WaitForSeconds(timeInSeconds);

        isEnabled = false;
    }
}