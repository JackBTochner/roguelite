# Spawning Enemies

Enemies are spawned after entering a location.


Waves will not spawn until the last wave has been cleared.

## Prerequisites
To start using the EnemySpawnManager, there needs to be:

Within the Scene:
1)  An A* prefab object (this is the navmesh that the enemies will use to spawn and walk upon).
2)  EnemySpawnManager prefab (this comes with mostly empty values).

Optionally, you might also need:
1) PlayerExit prefab (with the "PlayerExit" tag).

All of these can be found in the Assets/Prefabs folder.

## Assigning enemies to spawn
Under waves, click the plus button to add a new wave.

Populate the enemiesToSpawn array with enemy prefabs under Assets/Prefabs/Enemies.
Feel free to use duplicate entries in order to spawn more than one of the same type of enemy.

(shown here, this wave will spawn 3 BasicEnemy and 1 BeamEnemy)

Repeat to add another wave.
