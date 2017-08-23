GameObject = require "UnityEngine.GameObject"

function Login()

print("login")

uiRootPrefab = UnityEngine.Resources.Load("ui/UI Root");
uiGo = GameObject.Instantiate(uiRootPrefab);

labelPrefab = UnityEngine.Resources.Load("ui/LoginUI");
labelGo = GameObject.Instantiate(labelPrefab);
labelGo.transform.parent = uiGo.transform;
labelGo.transform.localScale = Vector3.one;

--typeUILabel = typeof(UILabel);
--print(typeUILabel)
uilabel = labelGo:GetComponent(typeof(UILabel));
uilabel.text = "haha";

end