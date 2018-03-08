# Gesture-UI-Development-Project
> Module: Gesture UI Development / 4th Year     
> Lecturer: Damien Costello     
> by - [Weichen Wang](https://w326004741.github.io/)

Gesture UI Development Project - B.Sc. (hons) in Software Development

#### Remove System Diagnostics Debugger Code:
```C#
     protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    this.DebugSettings.EnableFrameRateCounter = true;
            //}
#endif
```
## References:
- [SplitView Control](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/split-view)   
- [SplitView Control Chinese docs](http://lib.csdn.net/article/csharp/32756)
- [RelativePanel Chinese docs](https://www.jianshu.com/p/338d9046a872)