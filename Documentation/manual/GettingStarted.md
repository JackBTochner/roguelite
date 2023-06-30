# Getting started


## Setting up the project

**Prerequisite installs**

- Latest version of [Git](https://git-scm.com/downloads) (Extremely important.)
- A source control desktop app such as [Github Desktop.](https://desktop.github.com/)
- Unity Editor 2022.2.4f1
    -   This specific version is necessary and you should be prompted to install it after trying to open the project folder in [Unity Hub](https://unity.com/download). If that fails it might be found in the [Unity download archive.](https://unity.com/releases/editor/archive)


### **Installation**


**Git**

Select and download the Latest Source Release for your corresponding platform of Git https://git-scm.com/downloads.

Install Git with default settings and restart your computer.

**Github Desktop**

Download your corresponding version of Github Desktop https://desktop.github.com/.

Install with default settings and sign-in with your Github account that has access to the repository.

Select and clone the repository in the right hand side quick menu, OR select "Clone a repository from the Internet" option, OR go to the [repository url](https://github.com/pmuenjohn/roguelite), and in the green _<> CODE_ button, select Open with Github Desktop.
Clone into your desired project location. (i.e. C:\Users\Percy\Documents\repo)

**Unity**

At the moment only UnityHub is required to be installed. Skipping a default (non 2022.2.4) Unity Editor download might be ideal if it gives you the option.

From the Unity hub homescreen, select Open and navigate to your cloned local repository (i.e. C:\Users\Percy\Documents\repo\roguelite).

When attempting to open the project, it should prompt you to download the correct version of the Unity Editor (2022.2.4).
Select all default options for this Unity Editor download and install.

After opening the project, it should take some time to build libraries and import assets. 

## Navigating the project

Within the Asset directory (viewable through the Project window), there are 5 relevant subdirectories:
**Animations, Characters, Prefabs, Scenes, Scripts.**

**Animations** : Contains animation data and animation controllers for UI, VFX and eventually non character mesh animations.

**Characters** : Contains character specific data such as Portrait sprites and imported meshes, alongside animated meshes and their animation controllers.

**Prefabs** : Contains the completed prefab objects with all relevant components which can be dragged into gameplay scenes and used.

**Scenes** : Contains all scenes, separated into main Gameplay Locations and additive scenes such as Persistent and Gameplay Manager scenes.

**Scripts** : Contains all script and code for the game.
