# Mouse and Interaction Fixes
Mod that fixes several interacion issues in Car Mechanic Simulator 2018. 
This was originally a fork of MouseFix mod by Sauler (https://github.com/Sauler/MouseFix), converted to Unity Mod Manager instead of BepInEx.
Most of the patch logic was however rewritten from scratch since then, and more interaction functionality and fixes were added.

### Features:
- Enables the game to run in background.
- Mouse lock is disabled when ingame cusror is visible.
- Bypassed cursor movement smoothing. Game cursor position is the same as system cursor position, but the cursor is still rendered by the game and therefore can be inaccurate when on low fps

### Roadmap:
- Enable inventory toggle
- Press space in Inventory to invoke the "Sell parts" button 

### How to use
1. Download [Unity Mod Manager](https://www.nexusmods.com/site/mods/21), unzip somewhere
2. Download latest [release](https://github.com/SnakeBite94/MouseFix/releases/latest) of this mod
3. Run UnityModManager.exe
4. Choose "Car Mechanic Simulator 2018" from Game selection dropdown
5. Set game Folder (by default "C:\Program Files (x86)\Steam\steamapps\common\Car Mechanic Simulator 2018")
6. Press install, this will install the mod manager & loader to CMS18
7. Go to "Mods" tab, Install Mod or Drag&Drop the InteractionFix.zip file into the bottom part of the window
8. Status should be OK
9. Run the game, mod manager should open and should show InteractionFix as enabled
