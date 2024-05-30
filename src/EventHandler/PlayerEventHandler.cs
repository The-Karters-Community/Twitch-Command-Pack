using TheKartersModdingAssistant;
using TheKartersModdingAssistant.Event;
using TwitchCommandPack.Commands;

namespace TwitchCommandPack.EventHandler;

public static class PlayerEventHandler {
    public static void Initialize() {
        PlayerEvent.onFixedUpdateAfter += OnFixedUpdateAfter;
    }

    public static void OnFixedUpdateAfter(Player player) {
        if (FreezeCommand.isEnabled) {
            player.uPixelKartPhysics.kartController.ResetKartVelocities();
        }
    }
}