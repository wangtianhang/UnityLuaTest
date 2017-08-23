LoadResAndStartupClass = {}
LoadResAndStartupClass.__index = LoadResAndStartupClass;

function LoadResAndStartupClass.New()
	local ret = {};
	setmetatable(ret, LoadResAndStartupClass)
	return ret;
end

function LoadResAndStartupClass:LoadResAndStartup()

	print("加载表格等")

	Application.LoadLevel("login")

end