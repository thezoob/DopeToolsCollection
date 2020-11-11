# DopeCurves

![DopeCurves](https://i.imgur.com/2R1QLfD.png)

## Thanks for downloading DopeCurves.

### Please watch the video here: https://www.youtube.com/c/thezoobtube to learn how to use DopeCurves.

## FAQ:

 1. Why should I use DopeCurves in my project?
	- DopeCurves is a Custom Unit for Bolt that provides 7 commonly used animations (Ease In, Ease Out, Easy Ease, Linear, Bounce, Bounce 2, and Spring) created by a Professional Animator as Read Only AnimationCurves for you to use in your project.
	- You can Tween everything from Float values to Vector3 values which can be used to add beautiful animations to any moving Objects in Unity.

 2. Why isn't the unit showing up in the Fuzzy Finder?
	- Make sure you have Bolt installed in your Project.
	- Make sure you 'Build Unit Options...' from the Tools>Bolt menu.
	- If you've tried both but still can't get it working then let us know on the LASM Discord channel linked below.

 3. What is an AnimationCurve?
	- An AnimationCurve stores a collection of Keyframes that can be evaluated over time. 

 4. How do I use it?
	- In the FuzzyFinder type in "DopeCurves" and add the unit to your Graph (Macro). 
	- It's a good idea to cache (Set) the Curves you need as Variables in a Start Method and call (Get) the Variable later on in your project.
	- Please refer the Demo scene for use cases.

 5. Why are there so many SuperUnits, where can I find them?
	- There are a few SuperUnits (SU_SuperUnitName) provided with this package which will make using DopeCurves easier and more fun while keeping your Graph clean and tidy.
	- SuperUnits are customizable by the programmer too so if you want to extend the features further you can.
	- You can find the SuperUnits by starting type "SU_Dope" in the FuzzyFinder or under the Macros sub menu in the Fuzzy Finder.

 6. Which SuperUnits are included? What do they do?
	- **The SuperUnits included are as follows:**

		- **SU_DopeCurves_Float** - Tween a Float Value from A to B and is Evaluated within a specified Duration.
		- **SU_DopeCurves_Toggle_Float** - Tween a Float Value from A to B when first triggered, then B to A when triggered again and is Evaluated within a specified Duration. Cannot be interrupted while animation is playing.

		- **SU_DopeCurves_Vector3** - Tween a Vector3 Value from A to B and is Evaluated within a specified Duration.
		- **SU_DopeCurves_Toggle_Vector3** - Tween a Vector3 Value from A to B when first triggered, then B to A when triggered again and is Evaluated within a specified Duration. Cannot be interrupted while animation is playing.

		- **SU_DopeCurves_Color** - Tween a Color Value from A to B and is Evaluated within a specified Duration.
		- **SU_DopeCurves_Toggle_Color** - Tween a Color Value from A to B when first triggered, then B to A when triggered again and is Evaluated within a specified Duration. Cannot be interrupted while animation is playing.

		- **SU_RangeMapper_Float** - Clamps an incoming float value between a Pair of Values and Outputs them to a Value between another Pair of Values.
		- **SU_RangeMapper_Vector3** - Clamps an incoming Vector3 value between a Pair of Values and Outputs them to a Value between another Pair of Values.
		- **SU_RangeMapper_Generic** - Clamps an incoming value between a Pair of Values and Outputs them to a Value between another Pair of Values. (Generic version of RangeMapper Super Unit)


### Copyright 2020 Zubair Parkar.


###### https://www.thezoob.net
###### https://instagram.com/thezoob.art


SUPPORT
--------------------------------------
This Package is hosted on Github.
Store Listing can be found here: https://lifeandstylemedia.com/creators.php

Please post any question to the Life And Style Media Discord:
https://discord.gg/r4CYmf
