# ![banner_1](https://github.com/dotPawel/UniversalCMD/assets/89011403/4abb1023-7414-45a8-b32b-74fb978240e0)
  
UniversalCMD is a **command line utility tool** written in C# (something like Windows' CMD or Unix's bash except much simpler)  
Made to be portable and easy to use. Containing a full set of file management commands, it's own package manager and more.

*UniversalCMD needs .NET 8.0 Runtime to run*

(Windows terminal recommended)

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)

<p align="center">
  <img src="https://github.com/dotPawel/UniversalCMD/assets/89011403/ab3d8a97-a87e-41c9-ab99-82c4d5779602" />
</p>


## ![bannel_file_structure](https://github.com/dotPawel/UniversalCMD/assets/89011403/599a24a8-bd1c-4d90-b508-eb067ad47fd5)
Upon start up UniversalCMD checks for the following data, if missing UniversalCMD attempts to create them automaticly.
```
UniCMD.data\  
  ╚ Macros\ - Macros folder
  ╚ UniPKG\ - UniPKG data (Package manager)
   ╚ pkginfo\ - Installed package data 
  ╚ autoexec.cfg - Commands executed after startup
  ╚ config.cfg - User config
```

## ![banner_customization](https://github.com/dotPawel/UniversalCMD/assets/89011403/eec6d3a4-5cf7-4802-bbb6-e8f88b51df9c)
UniversalCMD can be customized using 3 built-in features

+ StartText - used for changing the welcome text
+ PromptText - used for changing the prompt bar
+ TextModules - allows for inserting data into text (can be used outside customization)

(my setup below)

![my_unicmd_setup](https://github.com/dotPawel/UniversalCMD/assets/89011403/9cdb0d3f-6e38-45d4-9e56-d2f5fbc7086a)

## ![banner_unipkg](https://github.com/dotPawel/UniversalCMD/assets/89011403/b6d1425b-bc9b-4816-b652-4d51152786cd)

UniPKG is UniversalCMD's built-in package manager hosted at https://unipkg.vercel.app/  
Learn how to make your own package at https://unipkg.vercel.app/how2pkg.html

![unipkg](https://github.com/dotPawel/UniversalCMD/assets/89011403/12ad1423-825d-4c86-9dcb-05e2d0b65343)

## ![banner_uniscript](https://github.com/dotPawel/UniversalCMD/assets/89011403/de1bb6c2-619b-49c4-a440-4ebd6bfc87c5)

UniScript allows for executing commands from a file as seen below, made for automating simple tasks, UniScript .unsc files can be executed as macros and using the 'uniscript' command

![unsc](https://github.com/dotPawel/UniversalCMD/assets/89011403/629d168f-34c2-4559-b37e-043e7a2b7d68)

## ![banner_unidkit](https://github.com/dotPawel/UniversalCMD/assets/89011403/b6c21bbd-85ee-4e94-a904-4a15ded44cfe)

UniDKIT is the officall development kit for UniCMD, it comes complete with syntax highlighting, autocomplete and a set of tools for UniPKG

Source code at https://github.com/dotPawel/UniDKIT

![unidkit](https://github.com/dotPawel/UniversalCMD/assets/89011403/576f62ca-5859-4210-982c-4335ebbac1a4)

Install it via UniPKG using ``unipkg /inst UniDKIT`` or click [here](https://github.com/dotPawel/UniDKIT/releases/latest) to get redirected to the release section in the UniDKIT repository


