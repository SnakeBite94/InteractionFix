# Mouse and Interaction Fixes
Mod that fixes several interacion issues in Car Mechanic Simulator 2018. 
This was originally a fork of MouseFix mod by Sauler (https://github.com/Sauler/MouseFix), just to move it to Unity Mod Manager.
Most of the patch logic was however written from scratch, and more interaction functionality and fixes were added.

### Features:
- Enables the game to run in background.
- Mouse lock is disabled when ingame cusror is visible.
- Bypassed cursor movement smoothing. Game cursor position is the same as system cursor position, but the cursor is still rendered by the game and therefore may lag at low fps
- Pause menu key toggle (ESC), inventory key toggle (I)
- Start game straight to main menu (needs UMM configuration change - see below)
- All fades to/from black 4x faster
- More hotkeys
    - Enter (bindable) - Accept order (order list)
    - Esc (bindable) - "No" or "Close" button in dialog popups
    - X - Sell Parts (inventory) / Decline Order (order list)
    - F - Focus search / Finish order (order details)
    - Mouse side buttons Forward and Back
        - Forward (last opened shop) and back (homepage) (shops)
        - Switch category (invenotry, warehouse)

### Known issues:
- F to focus search in shop does not work

### How to use
1. Download [Unity Mod Manager](https://www.nexusmods.com/site/mods/21), unzip somewhere
2. Download latest [release](https://github.com/SnakeBite94/InteractionFix/releases/latest) of this mod
3. Run UnityModManager.exe
4. Choose "Car Mechanic Simulator 2018" from Game selection dropdown
5. Set game Folder (by default "C:\Program Files (x86)\Steam\steamapps\common\Car Mechanic Simulator 2018")
6. Press install, this will install the mod manager & loader to CMS2018
7. Go to "Mods" tab, Install Mod or Drag&Drop the InteractionFix.zip file into the bottom part of the window
8. Status should be OK
9. Run the game, mod manager should open and should show InteractionFix as enabled

### Start straight to main menu 
 Default UMM configuration for CMS2018 injects mods when main menu is loaded. To enable this mod to skip intros, this has to be changed.
1. In UnityModManager folder, open UnityModManagerConfig.xml
2. Find "Car Mechanic Simulator 2018" GameInfo entry
3. Change the configuration to look like this:
```
<EntryPoint>[Assembly-CSharp-firstpass.dll]IntroPlayer.Awake:After</EntryPoint>
<StartingPoint>[Assembly-CSharp-firstpass.dll]IntroPlayer.Awake:After</StartingPoint>
<UIStartingPoint>[Assembly-CSharp-firstpass.dll]MainMenuManager.Awake:After</UIStartingPoint>
```
4. Open UnityModManager and press Uninstall, then Install again, so that the new configuration is used
5. Open CMS2018 -> the game should skip directly to main menu
