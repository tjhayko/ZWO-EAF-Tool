# ZWO EAF Tool


This software is explicitly without an warranty.
You are using this explicitly at your own risk.

I created this tool to allow people to reset the position (step count) of ZWO EAF Focusers.  It now also supports moving a number of steps larger than the normal maximum range of the ZWO EAF.  This works by resetting the current focuser step count, moving the maximum number of steps, and repeating until it's move the desired number of steps.  Once there, you can set the current focuser step count to any value you'd like, and use the standard ZWO ASCOM driver.

I am working on a ASCOM driver that does all of this transparently, and provides a range of focuser step counts.  I'm much closer if this works correctly, but there is still a little work to be done


It works on my machine, and in the testing that I've been able to do so far, and I would appreciate if any issues you find are reported.

The SDK API allows this, but ASCOM drivers do not, so I created simple form to provide access to this.

The ASCOM Driver should not be required for this tool to run.

You can download the installer here: https://github.com/tjhayko/ZWO-EAF-Tool/raw/main/EAF-Tool-setup.exe

Please let me know if you have any problems. 
