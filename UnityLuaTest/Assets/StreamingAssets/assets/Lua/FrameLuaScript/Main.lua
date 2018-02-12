GameObject = require "UnityEngine.GameObject"
Application = require "UnityEngine.Application"
Resources = require "UnityEngine.Resources"

function GameMain()	

	print("Main")

	updateRes = UpdateResClass:New();
	updateRes.UpdateRes();

end

function GameOnLevelWasLoaded(level)

	print("OnLevelWasLoaded " .. level)

	if(level == "loadResAndStartup") then
		loadResAndStartup = LoadResAndStartupClass.New();
		loadResAndStartup:LoadResAndStartup()
	end

	if(level == "login") then
		login = LoginClass.New()
		login:Login()
	end

end