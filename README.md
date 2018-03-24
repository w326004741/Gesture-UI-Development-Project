# Gesture-UI-Development-Project
> Module: Gesture UI Development / 4th Year     
> Lecturer: Damien Costello     
> by - [Weichen Wang](https://w326004741.github.io/)

## Introduction
My project is **Universal Windows Platform(UWP) Music Player** with **[Myo Armband](https://www.myo.com/)**. Find out more about universal apps here: https://msdn.microsoft.com/en-us/windows/uwp/get-started/whats-a-uwp. 

My Project was completed using [Thalmic labs Myo Armband](https://www.myo.com/). The Myo Armband reads the electrical activity of your muscles to control technology with gestures and motion. I use [Myo Armband](https://www.myo.com/) to control the Music Player and do some functions with gestures: **Play▶️, Pause⏸, Stop⏹, Previous⏮ and Next⏭.**


## Purpose of the application
The purpose of this application is to demonstrate how to use the [Myo Armband](https://www.myo.com/) to detect the electrical activity of muscles and control gestures and highly sensitive motion sensors detected by proprietary EMG muscle sensors to control the application to perform a series of operations. The [Myo Armband](https://www.myo.com/) has an **Electromyogram (EMG) sensor** that directly senses muscle activity and movement to read muscle activity in a refined manner.

This project is an example of the integration of **UWP** projects with **Myo**. It is a good demonstration of the use of Myo gesture control music player to perform a series of operations. 

#### If you want to know more details, the relevant code for this project and Myo is here([Music Player with Myo](https://github.com/w326004741/Gesture-UI-Development-Project/wiki/Music-Player-with-Myo)).

## Gestures identified as appropriate for this application
The **Myo** is an armband equipped with several Electromyography (EMG) sensors that can recognize hand gestures and the movement of the arms.Based on the electrical impulses generated by muscles, 7 EMG sensors are responsible to recognize and perform each gesture. ![image](https://github.com/w326004741/Gesture-UI-Development-Project/blob/master/image/3031521800414_.pic.jpg)
For this project, Myo provides the following intuitive hand movements/ gestures:

#### 1. Double Tap 

The user double tap their two fingers(Thumb and Middle) and this will command the Music Player to Play Music.
#### 2. Fist

The user making a fist and the Music Player to Pause Music.
#### 3. Wave In

The user waves in (Left) their wrist and the Music Player to Play Previous Song.
#### 4. Wave Out

The user waves out (Right) their wrist and the Music Player to Play Next Song.
#### 5. Fingers Spread

The user making a fingers spread and the Music Player to Stop Music.
## Hardware used in creating the application
1. Black Myo Armband(1)
2. Standard Micro-USB Cable(1)
3. Standard Micro-USB Cable(1)
4. Myo Sizing Clips(10)
5. Windows PC or Laptop, Mac(windows virtual machine)
## Architecture for the solution

## Conclusions & Recommendations



## References:
- [SplitView Control](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/split-view)   
- [SplitView Control Chinese docs](http://lib.csdn.net/article/csharp/32756)
- [RelativePanel Chinese docs](https://www.jianshu.com/p/338d9046a872)
- [Myo UWP Tutorial](https://elbruno.com/2016/08/02/myo-windows-10-uwp-apps-myo-and-visual-studio-2015/)
- [Get the CoreDispatcher(UI)](https://stackoverflow.com/questions/16477190/correct-way-to-get-the-coredispatcher-in-a-windows-store-app)
