
UpdateResClass = {}
UpdateResClass.__index = UpdateResClass;
function UpdateResClass : New()
	local ret = {}
	setmetatable(ret, UpdateResClass)
	return ret;
end


function UpdateResClass:UpdateRes()

	print("updateRes")

	UnityEngine.Application.LoadLevel("loadResAndStartup")

end