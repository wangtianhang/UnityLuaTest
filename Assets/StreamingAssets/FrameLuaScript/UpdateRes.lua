
function Main()	

print("updateRes")

UnityEngine.Application.LoadLevel("loadResAndStartup")

end

function OnLevelWasLoaded(level)

print("OnLevelWasLoaded " .. level)

LoadResAndStartup()

end