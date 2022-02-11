3.0.0
- Moved from BepInEx to Unity Mod Manager, renamed injection point
- Rewritten all patches
    - Run in background - different injection point, old one was no longer working
    - Mouse movement and locking
        - mouse smoothing is now disabled based on 'cursorIsEnable', which should be more reliable    
        - if Unity Mod Manager is open, then show cursor; otherwise hide.
        - if 'cursorIsEnable', then unlock cursor from window; otherwise lock in window.
