function Main()	

	print("Main")

	updateRes.updateRes()

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