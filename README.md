# Fishopwatch

## ⁉️What is it?

**Fishtopwatch** is a basic .NET MAUI app I was challenged to make by a friend, solely for the purpose of determining how long it had been since he and his roommates had cleaned the house. However, Fishtopwatch can be used to set multiple stopwatches that run at once, and can be paused/resumed from a handy list view.

> Note that is project is solely for stopwatches, not timers. When I made this project I forgot that timers counted down, not up 🤦

## 🛣️App Roadmap

-   Clean up the UI
    -   Add FontAwesome icons to the buttons to make them more appealing 🙂
-   Add support for color-picking to help visually differentiate colors
    -   _Note_: The NuGet package for [Maui.ColorPicker](https://github.com/nor0x/Maui.ColorPicker) is installed, but commented out. MCT's `IconTintColorBehavior` doesn't seem to play nice with converters or non-named colors, but will need to experiment. The backup would just be a group of pre-determined radio buttons.
    -   Is there another unique identifier we could use?
-   Fix the binding on the detail page
    -   Currently, the "Timer Running" animation doesn't properly stop and start on the detail page when the stopwatch does, due to improper binding
