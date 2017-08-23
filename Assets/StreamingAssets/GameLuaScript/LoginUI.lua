
LoginUI = {}

function LoginUI:Init()
	--typeUILabel = typeof(UILabel);
	--print(typeUILabel)
	uilabel = labelGo:GetComponent(typeof(UILabel));
	uilabel.text = "haha";
end

function LoginUI:OnClick()
    print("LUA OnClick")
end

