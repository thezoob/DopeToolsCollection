
# DopeAds Unity

## Thanks for Downloading DopeAds.
### DopeAds is a collection of Scripts and SuperUnits to make Ad creation with Bolt easier. It provides the OnUnityAds events from IUnityAdsListener Interface that are not available by default.
![DopeAds](https://i.imgur.com/o1Z4x47.png)



### How to use:

1. Make sure Ads are enabled in your Project.
2. Add UnityEngine.Advertisements to Assembly Options and both Advertisement and Advertisement.Banner in the Type Options under Unit Options Wizard.
  - ![Assemblies](https://i.imgur.com/0XNotIr.png)
  - ![Types](https://i.imgur.com/ZMLsCOX.png)
3. Create a new project in Unity Dashboard and get the appropriate GameIDs (iOS or Android)
4. Create a new GameObject with name AdManager, attach the Ads.cs script to it.
5. Add a Flow Machine and create a new Macro. Add the Initialize method to a Start Event to Initialize the Ads. (This is required if you want Callbacks.)
6. Add OnUnityAds Super Unit to the same Macro.
7. Elsewhere in your project you can add call the PlayInterstitialAd or PlayRewardedAd functions by adding Super Units of the same name.


-----------------------------------------
MIT License

Copyright 2020 Zubair Parkar. https://thezoob.net

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
