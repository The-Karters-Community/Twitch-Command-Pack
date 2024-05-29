using System;
using System.Collections.Generic;
using System.Linq;
using KinematicCharacterController;
using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;

namespace TwitchCommandPack.Commands;

public class TeleportCommand: ITwitchCommand {
    public Player player;
    public Player target;

    public string CommandFeedback(string _user, string[] _command) {
        string feedback = $"{_user} is a spatial magician and make {player.GetName()} teleports to {target.GetName()}!";

        target = null;

        return feedback;
    }

    public bool ExecuteCommand(string _user, string[] _command) {
        KinematicCharacterMotorState targetState = target.uPixelKartPhysics.kartController.Motor.GetState();

        player.uPixelKartPhysics.kartController.Motor.ApplyState(targetState);
        player.uPixelKartPhysics.kartController.kartPhysics.SetSteeringPhysicsRotation(targetState.Rotation);

        player.SetCurrentLapCount(target.GetCurrentLapCount());

        return true;
    }

    public bool IsActivated() {
        return TwitchCommandPack.Get().data.isTeleportCommandEnabled;
    }

    public bool ShouldExecuteCommand(string _user, string[] _command) {
        if (_command.Length > 3) {
            return false;
        }

        if (!IsFirstTermAccepted(_command[1])) {
            return false;
        }

        player = Player.FindMainPlayer();

        if (_command.Length == 2) {
            ExecuteWhenOneTerm(player);
        } else {
            ExecuteWhenTwoTerms(player, _command[2]);
        }

        if (target is null || player.GetIndex() == target.GetIndex()) {
            return false;
        }

        return true;
    }

    protected bool IsFirstTermAccepted(string term) {
        List<string> allowedFirstWord = new() {"teleport", "tp"};

        return allowedFirstWord.Contains(term);
    }

    protected void ExecuteWhenOneTerm(Player player) {
        Random random = new();
        int position;

        do {
            position = random.Next(1, 9);
        } while (position == player.GetPosition());

        target = FindPlayerByPosition(position);
    }

    protected void ExecuteWhenTwoTerms(Player player, string term) {
        if (int.TryParse(term, out _)) {
            target = FindPlayerByPosition(int.Parse(term));
        } else {
            target = FindPlayerByName(term);
        }
    }

    protected Player FindPlayerByPosition(int position) {
        List<Player> players = Player.GetPlayersSortedByPosition();

        return players[position - 1];
    }

    protected Player FindPlayerByName(string name) {
        return Player.GetActivePlayers().FirstOrDefault(p => p.GetName() == name);
    }
}