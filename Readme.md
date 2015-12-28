## **Ohana3DS Rebirth**

### **What is Ohana3DS?**

Ohana3DS is a work in progress tool used to view, extract, and ultimately replace data from decrypted 3DS roms.

Examples of such are the ability to view Models, Textures, even some animations.

Ohana3DS aims to be a one stop shop for all 3DS rom viewing, extracting, editing needs.

### **What's this whole "Rebirth" deal?**

Originally written in Visual Basic, The Rebirth edition is a reboot of the tool in C#, which is more powerful, flexible, and stricter than the Visual Basic langage.

This allows the Programmer more horsepower with fewer issues, as the strictness of the langage helps prevent common errors.

C# tends to be an "It either works or it doesn't" kind of language.

Currently, Rebirth does NOT support decrypting roms, so other sources are needed for such.

However, Rebirth boasts more features and support than it's original counterpart.

### **Where can I get this tool?**

Recent updated builds as well as depreciated (legacy) builds, are provided at:

https://gbatemp.net/threads/wip-ohana3ds-tool.392576/


### **But... I want BLEEDING EDGE!**

Your in luck. Getting bleeding edge is simple with a small bit of tech know how.

You will need Visual Studio Community 2015 (free with Microsoft account)
Simply follow the steps outlined at:

https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx

to get started.

After the IDE (Integrated Developer Environment) is installed, you can simply visit

https://github.com/gdkchan/Ohana3DS-Rebirth

to view the source.

For those code saavy, you can obtain this source using GIT tools, or for those not so tech saavy, do not explore the contents, but look for the "Dowload ZIP" button and download the source.

If you downloaded the Zip, you need to extract it to a folder somewhere on your computer.

Make sure you can easily access it for the next step. If you used Git, simply proceed.

After the folder is extracted, look for the "_Ohana3DS Rebirth.sln_" file, and open it.

This should automatically bring up Visual Studio Community.

From here, you will be presented with a weird interface.

If you get an error about opening solutions from trusted source, this is fine and a good thing.

Simply uncheck the box that says "warn me for every project" (might be differently worded, as mine no longer warns me), and click ok.

Now the solution is loaded.

Don't worry if you don't see anything, you have made progress.

Next, we will be using the menu.

If you are unfamiliar with Visual Studio, follow these directions explicitly.

First, you will click the dropdown box that says "debug".

Do NOT confuse this with the menu version of "debug".

This will have another dropdown box next to it that says "Any CPU".

Change "debug" to "release".

All this does from what I can tell, is prevent extra annoying debugging crap if the program runs into an error.

As a user, "release" is preferred anyhow.

This should be noted as optional though, as either way works.

Next, in the actual menu, you will see a "build" option.

Clicking this will present you with another menu.

You want to click on "Build Ohana3DS Rebirth".

Simply wait for the compiler to work it's magic, and you will see:

_========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========_

From here, simply navigate to the ohana project folder you downloaded and extracted, find the "bin" folder, and copy the "release" folder located inside the "x86" folder.

Here is an example on my computer:

_C:\gdkchan\Ohana3DS-Rebirth\Ohana3DS Rebirth\bin\x86\Release_

You want to copy the release folder itself, not the contents inside.

Paste this folder where you see fit, as it now contains a built version of the latest Ohana.

You can safely rename the folder as you see fit, but leave the files inside alone.

Ohana can be ran at anytime by opening "Ohana3DS Rebirth.exe".

For future builds, simply delete the old folders, and repeat this process.

### **This is confusing. Is there an easier way?**

Probably.

I am barely fluent with the IDE as my expertise lies with Unity 3D + Monodevelop as my IDE, though I am having to learn the VS IDE with the latest Unity update replacing Monodevelop with Visual Studio.

This solution simply works, and for now is shared here to simply get you going until a better readme is made.

### **What's with the extra files when I compile it myself?**

Honestly I have no clue.

I follow the rule: "If it ain't broke, dont delete it."

I do know that gdkchan's releases only contain:

_Microsoft.DirectX.Direct3D.dll_

_Microsoft.DirectX.Direct3DX.dll_

_Microsoft.DirectX.dll_

and _Ohana3DS Rebirth.exe_

Deleting the extra files doesn't seem to break anything, but delete the extra files at your own risk.

### **Off topic, but who are you, what is "Unity", What is "Monodevelop" and why did you write this $^%& readme?**

The name's **Fallenleader**, I am learning as an Indie Game Developer, currently working on a 3D Pokemon PC/Android remake for the original Pokemon games Red/Blue that heavily depends on Ohana to rip models and hopefully animations soon (those are actually critical).

Unity 3D is a 3D engine and is unrelated to this project.

Info on Unity can be found easily by searching on Google.

Monodevelop is another IDE like Visual Studio, but is not needed and is useless for this project.

As for this readme I wanted to contribute any way I could.

Right now, as should be, gdkchan is focusing on making this epic beast of a program. 

So until he makes a better readme, this is what I am able to contribute for now.

Unifortuantely, I am not the best at making readme's without pictures, and couldn't figure out how to embed them.

Hopefully he won't mind the footnote XD
