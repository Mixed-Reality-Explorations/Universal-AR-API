# Universal-AR-API
This repo contains a unity wrapper around ARKit / ARCore APIs for Image Recognition.

Eventually, ARFoundation will cover this use-case. But as of May 2019, it's still extremely difficult to write an image-recognition AR app that works out of the box on both Android and IOS. 

This code exposes an API that allows a developer to focus on the interaction and experience design. The wrapper automatically detects which hardware the app is built for, and correctly calls the appropriate function for each operating system.
