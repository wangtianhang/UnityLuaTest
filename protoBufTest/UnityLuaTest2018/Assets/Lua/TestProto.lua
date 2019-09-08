local common_pb = require 'Protol.common_pb'
local person_pb = require 'Protol.person_pb'

function Decoder()
    local msg = person_pb.Person()
    msg:ParseFromString(TestProtol.data)

    print('============================')
    for k, v in pairs(msg) do
        if type(v) == "table" then
            print(tostring(k) , ' ' , tostring(v) , ' v[1] ' , v[1])
        end
    end
    --print('msg.age ' ..  msg.age)
    --print('tostring(msg) ' ..  tostring(msg))
    --[[
    for k, v in pairs(msg) do
        print(tostring(k) .. ' ' .. tostring(v))
    end
    --]]
    print('============================')
    
    --tostring 不会打印默认值
    print('person_pb decoder: '..tostring(msg)..'age: '..msg.age..'\nemail: '..msg.email)
end

function Encoder()
    local msg = person_pb.Person()
    msg.header.cmd = 10010
    msg.header.seq = 1
    msg.id = '1223372036854775807'
    msg.name = 'foo'
    --数组添加                              
    msg.array:append(1)
    msg.array:append(2)
    --extensions 添加
    local phone = msg.Extensions[person_pb.Phone.phones]:add()
    phone.num = '13788888888'
    phone.type = person_pb.Phone.MOBILE
    local pb_data = msg:SerializeToString()
    TestProtol.data = pb_data
end