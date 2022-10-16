# 3D Picker

- Player collects objects with a picker to end of the platform.
- There are 15 randomly generated levels.
- Skybox is selected automatically at the beginning of each level. You can reach them via GameManager object in the GameScene.
- Levels gets harder.
- If the specified number of objects are collected, the player can move to the next level, otherwise the level is played again.
- If the player's level is more than the created level, the levels continue randomly.
- At the end of the successful level, gold is earned as much as the amount of collected objects. This information is kept in PlayerPrefs and the total amount is displayed on the game screen.
- Please use Device Simulator or a real device to test. Picker controller works with Touch Input.


# Level Editor

- To reach editor scene => Assets/Scene/LevelEditorScene.
- You can find my custom inspector in the inspector of the LevelEditorManager object in the hierarchy.
- Please create and remove objects from custom inspector.
- You can make position and rotation changes from the scene, the editor saves them while saving the level.
- Levels are stored in Assets/Resources/Levels.

### Generate Random Level

- You can randomly generate as many objects as you want for each region.

### New Blank Level

- Clicking the button opens an empty level.

### Load Level

- Loads selected level.

### Generate Platform Color

- Changes the color of the platform randomly.

### Create Object

- Creates the desired collectible object in front of the last collectible object in the scene.

### Remove Object

- When a collectible object is created on the scene, the remove button of that object is also created in this area. You can remove that object by clicking the button.

### Set Complete Counts

- The number of completions for each zone can be determined here.

### Save Level

- You can save the level if the completion inputs are valid.
- If you are saving a new level, it is saved in Resources/Levels.
- If you have made changes to an existing level, that level is saved.
- Level numbers are given automatically when saving according to the number of levels available.
