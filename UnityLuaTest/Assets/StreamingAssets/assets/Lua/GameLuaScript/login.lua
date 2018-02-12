
LoginClass = {}
LoginClass.__index = LoginClass;

function LoginClass.New()
	local ret = {};
	setmetatable(ret, LoginClass)
	return ret;
end

function LoginClass:Login()

	print("login")

	uiRootPrefab = Resources.Load("ui/UI Root");
	uiGo = GameObject.Instantiate(uiRootPrefab);

	labelPrefab = Resources.Load("ui/LoginUI");
	labelGo = GameObject.Instantiate(labelPrefab);
	labelGo.transform.parent = uiGo.transform;
	labelGo.transform.localScale = Vector3.one;

	--typeUILabel = typeof(UILabel);
	--print(typeUILabel)
	--uilabel = labelGo:GetComponent(typeof(UILabel));
	--uilabel.text = "haha";
	
	TestLua();
	
	TestProto();
	
	labelGo:AddComponent(typeof(LuaBehaviour))
	loginUI = LoginUIClass.New(labelGo);
	loginUI:Init();
end

function TestLua()

	retStr = GUtil.TestLuaCallByString("中文");
	print(retStr)
	retBytes = GUtil.GetFileData("/assets/bytes/NewyearFamilywarreward.byte")
	print(type(retBytes))
	print(retBytes)
	GUtil.HandleFileData(retBytes);
	
	tableParam = {}
	tableParam["luaKey"] = "luaValue";
	for k,v in pairs(tableParam)
		do print("key ".. k .. " value "..v)
	end
	
	retDic = GUtil.TestLuaCallByLuaDicTable(tableParam)
	print(type(retDic))
	for k,v in pairs(retDic)
		do print("key " .. k .. " value " .. v)
	end
	
	tableParam2 = {}
	tableParam2[1] = "luaArray1";
	tableParam2[2] = "luaArray2";
	retArray = GUtil.TestLuaCallByLuaArrayTable(tableParam2)
	print(type(retArray))
	for k,v in ipairs(retArray)
		do print("key ".. k .. " value " .. v)
	end
	
	IntoCSharpParamInstance = GUtil.CreateIntoCSharpParam()
	IntoCSharpParamInstance.m_intoParam = tableParam2
	OutCSharpParamInstance = GUtil.TestLuaCallByClass(IntoCSharpParamInstance)
	for k,v in ipairs(OutCSharpParamInstance.m_outParam)
		do print("key ".. k .. " value " .. v)
	end
	print("OutCSharpParamInstance type " ..  type(OutCSharpParamInstance))
	

	
end

function TestProto()
	local common_pb = require 'Protol.common_pb'
	local person_pb = require 'Protol.person_pb'
	
	local msg = person_pb.Person()   
	msg.header.cmd = 10010                                 
    msg.header.seq = 1
    msg.id = '1223372036854775807'            
    msg.name = 'foo'
	msg.array:append(1)                              
    msg.array:append(2)
	
	local pb_data = msg:SerializeToString()
	
	local PacketInstance = GUtil.CreatePacket();
	PacketInstance.m_bytes = pb_data;
	local PacketInstance2 = GUtil.HanldeProtoBuf(PacketInstance)
	
	local msg2 = person_pb.Person()
	msg2:ParseFromString(PacketInstance2.m_bytes)
	print(msg2.id)
end

