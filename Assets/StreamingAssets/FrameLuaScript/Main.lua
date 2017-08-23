GameObject = require "UnityEngine.GameObject"
Application = require "UnityEngine.Application"
Resources = require "UnityEngine.Resources"

function Main()	

	print("Main")

	updateRes = UpdateResClass:New();
	updateRes.UpdateRes();

end

function OnLevelWasLoaded(level)

	print("OnLevelWasLoaded " .. level)

	if(level == "loadResAndStartup") then
		loadResAndStartup = LoadResAndStartupClass.New();
		loadResAndStartup:LoadResAndStartup()
	end

	if(level == "login") then
		Login.Login()
	end

end