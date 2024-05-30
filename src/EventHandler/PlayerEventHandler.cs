using System.Reflection;
using TheKartersModdingAssistant;
using TheKartersModdingAssistant.Event;
using TwitchCommandPack.Commands;
using UnityEngine;

namespace TwitchCommandPack.EventHandler;

public static class PlayerEventHandler {
    public static void Initialize() {
        PlayerEvent.onFixedUpdate += OnFixedUpdate;
        PlayerEvent.onFixedUpdateAfter += OnFixedUpdateAfter;
    }

    public static void OnFixedUpdate(Player player) {
        if (AutoDriveCommand.player is null || !player.IsHuman()) {
            return;
        }

        player.IsControlledByAi(AutoDriveCommand.isEnabled);
    }

    public static void OnFixedUpdateAfter(Player player) {
        if (FreezeCommand.player is null || !player.IsHuman()) {
            return;
        }

        if (FreezeCommand.isEnabled) {
            player.uPixelKartPhysics.kartController.ResetKartVelocities();
        }
    }
}