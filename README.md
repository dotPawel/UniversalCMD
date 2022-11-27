![unicmd banner linux](https://user-images.githubusercontent.com/89011403/204157512-2c3ade5c-6d4a-4e23-a668-b9ec1931c8e2.png)
## THIS IS NOT FINISHED AND IS VERY UNSTABLE

This port is not guaranteed to be finished.<br />

## Running UniCMD on your Linux installation
Binaries in the releases section are compiled for Ubuntu so Ubuntu/Debian based distros will work best <br />
1. Extract the "publish" folder in your chosen location<br />
2. Open terminal inside the "publish" folder and run<br />
``` ./UniCMD ``` 
3. (Optional) It is recommended to provide execute permissions to avoid some issues <br />
``` chmod 777 ./UniCMD ```<br />

And your UniCMD instance should be up and running! (many crashes may and will occur) <br />

## Compiling UniCMD for Linux
The command i used for compiling UniCMD for Linux is <br />
``` dotnet publish -c debug -r ubuntu.16.04-x64 --self-contained ``` <br />
<br />
After compiling your UniCMD binary (publish folder) will be located in <br />
```bin/Debug/net7.0/ubuntu.16.04-x64```
