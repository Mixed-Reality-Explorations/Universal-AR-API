# Universal-AR-API
This repo contains a unity wrapper around ARKit / ARCore APIs for Image Recognition.

Eventually, ARFoundation will cover this use-case. But as of May 2019, it's still extremely difficult to write an image-recognition AR app that works out of the box on both Android and iOS. 

This code exposes an API that allows a developer to focus on the interaction and experience design. The wrapper automatically detects which hardware the app is built for, and correctly calls the appropriate function for each operating system.

## Verify / Demo 
1. Clone the repo.
2. Open the folder in Unity (tested on 2018.3.8f1)
3. In the project view, open the UAR folder -> Scenes -> UARTest
4. Open Build Settings. Select the platform you'd like to build for (Android or iOS). This may involve importing many assets to your system, which may take some time.
5. Once you hit build, it'll generate the app file. 
    - For Android (Jelly Bean and above)
        - It creates an .apk file that you can push to your device using ```adb install <apk name>```
        - Once uploaded to your phone, the app is called "arapi".
        - Open the app on your phone, then open one of [the Android example images included in this repo.](https://github.com/Mixed-Reality-Explorations/Universal-AR-API/tree/master/Assets/GoogleARCore/Examples/AugmentedImage/Images)
        - Pointing your phone at the image should make a grey block appear in front of the image.
    

## Usage
1. Download the repo.
2. Import the UAR package into your project.
3. Include a UARInit prefab into your scene.
4. The UARInit prefab includes an ARCore Ccmera as well as an ARKit camera.
    - To change the settings of either camera, double-click on the camera and update the settings.
5. Instead of calling the ARCore or ARKit APIs directly, call the Universal AR API instead.

```
public static Action<IAnchor> IAnchorAdded;
public static Action<IAnchor> IAnchorUpdated;
public static Action<IAnchor> IAnchorRemoved;

public static Action<WAnchor> WAnchorAdded;
public static Action<WAnchor> WAnchorUpdated;
public static Action<WAnchor> WAnchorRemoved;
```

6. IAnchor actions are Image Anchor actions, and WAnchor actions are World Anchor actions.
7. See the [UARTest example code](https://github.com/Mixed-Reality-Explorations/Universal-AR-API/blob/master/Assets/UAR/Scripts/UARTest.cs) for the simple usage example in the above verification demo.

![Imgur](https://i.imgur.com/fKJdX7d.png)
