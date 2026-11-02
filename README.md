# https://youtu.be/B-ER2eypaOU

# lab10

Save & Load

Pressing S saves the game and pressing L loads it. The score goes into a binary file (score) using BinaryFormatter, and the locations of the player and every target go into a JSON file (locations) using JsonUtility. Bullets are not saved.
ISaveable is the blueprint for anything that can be saved. It holds an ID, a kind , the object’s data as JSON, and a way to load that data back in. SaveableBehaviour is the base class that gives every object its own unique ID so it can be found again later. TransformSaver is the one that actually does the saving. It sits on the Shooter and the Target prefab and stores the position, rotation, and scale.
SavingService does all the file work. When saving, it finds everything that is an ISaveable, turns each one into an entry, and writes them all to the JSON file, then writes the score to the binary file. SaveLoadController is just the listener for the S and L keys that tells SavingService when to go.
When loading, the score is read first and passes to the ScoreManager, which fires its event to update ui. Then every target currently on screen is destroyed. The player is found again using its saved ID and transformed back. The targets can’t be found by ID since they are made while the game runs, so each saved target is rebuilt through the Builder using TargetSpawner, giving it the right speed, size, and points for its kind. Its saved position is restored. The direction a target was moving is kept in the sign of its X scale, so a negative scale means it was going left.

