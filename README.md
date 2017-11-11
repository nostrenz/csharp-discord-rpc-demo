# C# Discord RPC demo

A quick demontration of the Discord RPC feature with a simple C# WPF app, made from the Unity demo provided in the official repository.

* Official documentation: https://discordapp.com/developers/docs/rich-presence/how-to
* Official SDK repository: https://github.com/discordapp/discord-rpc

## How do I use it?

### 1 - Run the app

Download a release and run `DiscordRpcDemo.exe` **or** build the app from source by cloning this repo, opening `DiscordRpcDemo.sln` in Visual Studio and runing the solution.

![Main window](https://raw.githubusercontent.com/nostrenz/cshap-discord-rpc-demo/master/screenshots/window.png)

### 2 - Add it as a game in Discord

Open the settings panel in the Discord Desktop app, go to **Games**, click "**Add it!**" and select the app ("Discord RPC demo").

### 3 - Obtain a Client ID

Now you will need a Client ID. To obtain it just go to [the Discord developper applications panel](https://discordapp.com/developers/applications/me) and click "**New App**".
Give it a nice App name, click the "**Create app**" button, then the "**Enable Rich Presence**" button.

### 4 - Mess with it

Copy the **Client ID** at the top of the page and paste it in the **Client ID** field in the app.
Click "**Initialize**" then "**Update**" and you should see this below your username and in your Discord profile:

![Discord Profile](https://raw.githubusercontent.com/nostrenz/cshap-discord-rpc-demo/master/screenshots/profile.png)

Of couse you can change what's in each fields, press "**Update**" and you'll see it changed on Discord after a few seconds.
Also, hitting **RunCallbacks** should tells you about errors or disconnections if any.
