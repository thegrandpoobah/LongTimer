Saliences.LongTimer.Timer
=========================

LongTimer is a drop-in replacement for .NET's built in `System.Timers.Timer` class which increases the maximum allowable Interval to `double.MaxValue` as opposed to the built-in implementation's `int.MaxValue`. It uses a port of Mono's unit tests for the System.Timers.Timer class to ensure that the behavior matches 1:1 to the built-in class.

Licensing Caveat
================

The library is licensed under the MIT license. The image used for the Component's toolbox icon is taken from the Visual Studio Image Library. Although the exact license of the asset is not accurately defined by Microsoft, by all indications on the Internet, the usage is permissible. 