function Main()	

	print("Main")

	updateRes = UpdateResClass:New();
	updateRes:UpdateRes();

end

function OnLevelWasLoaded(level)

	print("OnLevelWasLoaded " .. level)

	if(level == "loadResAndStartup") then
		LoadResAndStartup.LoadResAndStartup()
	end

	if(level == "login") then
		Login.Login()
	end

end