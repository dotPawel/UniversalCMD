# ![Unicmd banner](https://user-images.githubusercontent.com/89011403/187036721-ad778434-1502-4378-b0e2-30aa343a8618.png)
  
UniversalCMD is a **command line utility tool** written in C# (to put it simply, something like Microsoft's CMD)  
made to be portable and easy to use. Containing a full set of file management commands and more, also a AeroCL replacement

*UniversalCMD needs .NET 7.0 Runtime to run*

(Windows terminal recommended)

<p align="center">
  <img src="https://user-images.githubusercontent.com/89011403/226937038-d282ca1b-bd49-4abf-90d3-ba6612913322.png" />
</p>


![fs](https://user-images.githubusercontent.com/89011403/226941736-aba1976e-712e-4734-97d7-6ccb37d567d8.png)
Upon start up UniversalCMD checks for the following data, if missing UniversalCMD attempts to create them automaticly
```
UniCMD.data\  
  ╚ Macros\ - Macros folder
  ╚ autoexec.cfg - Commands executed after startup
  ╚ config.cfg - User config
```

![unicmd banner 4](https://user-images.githubusercontent.com/89011403/212902032-d6f20440-5042-4df1-91ec-b4f328d6e2ba.png)
UniversalCMD contains 3 customization features

+ StartText - allows for changing the welcome text
+ PromptText - allows for changing the prompt bar (somewhat like ohmyzsh but really simple)
+ TextModules - allows for inserting data into text (not really customization but used heavily when customizing)

(my setup)

![Zrzut ekranu 2023-03-23 133323](https://user-images.githubusercontent.com/89011403/227204910-cb475bc1-08df-4eee-8beb-32fa9673c8d8.png)

![unicmd banner 3](https://user-images.githubusercontent.com/89011403/187044183-d36343db-e355-4354-a8bf-cd9ca39d2ee5.png)

If for any reason you need AeroCL it still can be started from UniversalCMD using ``acl_bb start``

![acl](https://user-images.githubusercontent.com/89011403/226953710-4cc5a571-4451-42ea-81ba-2bdb11b2165f.png)

![uniscript banner](https://user-images.githubusercontent.com/89011403/212897047-e1fa894b-6d0a-4eaf-8462-6ede8ec12310.png)

UniScript is UniversalCMD's very simple scripting language, made mainly for automating simple tasks

UniScript is also used by the macros feature

![uniscript example](https://user-images.githubusercontent.com/89011403/212900244-25629047-3298-45cb-8fd4-7a42e56bbcdf.png)

