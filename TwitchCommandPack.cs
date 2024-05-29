using BepInEx;
using BepInEx.Configuration;
using TheKarters2Mods;
using TheKarters2Mods.Patches;
using TheKartersModdingAssistant;
using TwitchCommandPack.Commands;
using TwitchCommandPack.Core;

namespace TwitchCommandPack;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(TheKartersModdingAssistant.MyPluginInfo.PLUGIN_GUID, ">=0.2.0")]
[BepInDependency(TwitchBasicCommandsSDK_BepInExInfo.PLUGIN_GUID, ">=1.0.0")]
public class TwitchCommandPack: AbstractPlugin {
    public static TwitchCommandPack instance;

    /// <summary>
    /// Get the plugin instance.
    /// </summary>
    /// 
    /// <returns>TwitchCommandPack</returns>
    public static TwitchCommandPack Get() {
        return instance;
    }

    public ConfigData data = new();

    /// <summary>
    /// TwitchCommandPack constructor.
    /// </summary>
    public TwitchCommandPack(): base() {
        pluginGuid = MyPluginInfo.PLUGIN_GUID;
        pluginName = MyPluginInfo.PLUGIN_NAME;
        pluginVersion = MyPluginInfo.PLUGIN_VERSION;

        harmony = new(pluginGuid);
        logger = new(Log);

        instance = this;
    }

    /// <summary>
    /// Patch all the methods with Harmony.
    /// </summary>
    public override void ProcessPatching() {
        BindFromConfig();

        if (data.isModEnabled) {
            logger.Info($"{this.pluginName} has been enabled.", true);

            // Put all methods that should patched by Harmony here.
            harmony.PatchAll(typeof(PixelEasyCharMoveKartController__SteerInput));

            harmony.PatchAll(typeof(PixelSDK_CameraEvents__OnPreCull));
            harmony.PatchAll(typeof(PixelSDK_CameraEvents__OnPreRender));
            harmony.PatchAll(typeof(PixelSDK_CameraEvents__OnPostRender));

            harmony.PatchAll(typeof(Ant_KartParticlesWorker__WorkerUpdate));

            // Then, add methods to the SDK actions.
            // Eg:
            //GameEvent.onGameStart += () => logger.Log("(From action) The game has been started.");

            TwitchBasicCommandsSDK.Instance.RegisterCommand(new ReverseDirectionInputsCommand());
            TwitchBasicCommandsSDK.Instance.RegisterCommand(new ScreenMirrorCommand());
            TwitchBasicCommandsSDK.Instance.RegisterCommand(new ScreenFlipCommand());
            TwitchBasicCommandsSDK.Instance.RegisterCommand(new TeleportCommand());
        }
    }

    /// <summary>
    /// Bind configurations from the config file.
    /// </summary>
    public void BindFromConfig() {
        BindGeneralConfig();
        BindCustomizationConfig();
    }

    /// <summary>
    /// Bind general configurations from the config file.
    /// </summary>
    protected void BindGeneralConfig() {
        ConfigEntry<bool> isModEnabled = Config.Bind(
            ConfigCategory.General,
            nameof(isModEnabled),
            true,
            "Whether the mod is enabled."
        );

        data.isModEnabled = isModEnabled.Value;
    }

    /// <summary>
    /// Bind customization configurations from the config file.
    /// </summary>
    protected void BindCustomizationConfig() {
        ConfigEntry<bool> isReverseDirectionInputsCommandEnabled = Config.Bind(
            ConfigCategory.Customization,
            nameof(isReverseDirectionInputsCommandEnabled),
            true,
            "Whether the reverse direction inputs command is enabled."
        );

        ConfigEntry<bool> isScreenMirrorCommandEnabled = Config.Bind(
            ConfigCategory.Customization,
            nameof(isScreenMirrorCommandEnabled),
            true,
            "Whether the screen mirror command is enabled."
        );

        ConfigEntry<bool> isScreenFlipCommandEnabled = Config.Bind(
            ConfigCategory.Customization,
            nameof(isScreenFlipCommandEnabled),
            true,
            "Whether the screen flip command is enabled."
        );

        ConfigEntry<bool> iTeleportCommandEnabled = Config.Bind(
            ConfigCategory.Customization,
            nameof(iTeleportCommandEnabled),
            true,
            "Whether the teleport command is enabled. Usage: \"teleport [pos|name]\" or \"tp [pos|name]\". If no position or name is given, it will randomly choose another player."
        );

        data.isReverseDirectionInputsCommandEnabled = isReverseDirectionInputsCommandEnabled.Value;
        data.isScreenMirrorCommandEnabled = isScreenMirrorCommandEnabled.Value;
        data.isScreenFlipCommandEnabled = isScreenFlipCommandEnabled.Value;
        data.isTeleportCommandEnabled = iTeleportCommandEnabled.Value;
    }
}
