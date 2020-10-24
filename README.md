FPS
https://github.com/BagelMaster5000/FPS

•WASD - Move
•Mouse - Look
•Left Click - Shoot
•Right Click - Look Through Scope
•Shift While Moving - Sprint
•R - Reload
•Escape - Pause/Unpause

Innovation Added:
•Health regen after not getting hit for a while.
•Two enemy types: laser and bullet
•Spawners around the map spawn new enemies, but only do so when the player isn’t looking in their direction. So, enemies will never spawn within a player’s field of view.
•Hitmarkers. White when hit enemy and red when killed enemy.

Extra Stuff:
•Camera movement and rotation are lerped so player movement still occurs in fixed update every 0.02 seconds; but, the camera can move at any frame rate preventing the game from looking stuttery.
•I’m using a shader kit called Flatkit that I’ve owned for a while.
•Guns have all variables saved in scriptable objects and game is designed to handle multiple weapon types. Didn't get time to implement them for this project though.
•Visuals are separated from logic, making the code much cleaner. (Using Unity Events).
•Enemies use the Unity navmesh for pathfinding.