# Universal-AR-API
This repo contains a unity wrapper around ARKit / ARCore APIs for Image Recognition.

Eventually, ARFoundation will cover this use-case. But as of May 2019, it's still extremely difficult to write an image-recognition AR app that works out of the box on both Android and iOS. 

This code exposes an API that allows a developer to focus on the interaction and experience design. The wrapper automatically detects which hardware the app is built for, and correctly calls the appropriate function for each operating system.

## Verify 
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
