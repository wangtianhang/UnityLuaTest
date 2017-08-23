
function Main()	

print("updateRes")

UnityEngine.Application.LoadLevel("loadResAndStartup")

end

function OnLevelWasLoaded(level)

print("OnLevelWasLoaded " .. level)

if(level == "loadResAndStartup") then
	LoadResAndStartup()
end

if(level == "login") then
	Login()
end

end