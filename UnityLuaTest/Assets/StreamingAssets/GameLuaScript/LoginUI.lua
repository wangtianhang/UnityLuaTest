
LoginUIClass = {}
LoginUIClass.__index = LoginUIClass

function LoginUIClass.New(gameObject)
	local ret = {};
	setmetatable(ret, LoginUIClass)
	
	ret.gameObject = gameObject;
	
	return ret;
end

function LoginUIClass:Init()
	--typeUILabel = typeof(UILabel);
	--print(typeUILabel)
	print("LoginUIClass:Init")
	
	uilabel = self.gameObject:GetComponent(typeof(UILabel));
	uilabel.text = "haha";
	
	luaBehaviour = self.gameObject:GetComponent(typeof(LuaBehaviour));
	luaBehaviour:AddClick(self.OnClick);
end

function LoginUIClass:OnClick()
    print("LUA OnClick")
end

