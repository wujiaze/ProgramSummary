---
--- Lua 函数
---     1、函数的定义:
---         形如 function funName(arg0,arg1...)
---                  [函数体]
---             end
---     2、函数的性质:
---         1、函数无需定义也不能定义返回类型
---         2、一个函数可以返回任意数量与任意类型的数据，采用 return 关键字
---         3、可以将函数赋值给变量，通过调用变量，来调用函数 (类似C#的委托形式)
---            也可以将函数作为参数，通过使用参数，来调用函数 (类似C#的委托形式)
---     3、全局函数 和 局部函数
---         类似 全局变量 和 局部变量，添加 local 关键字
---     4、匿名函数
---        不需要函数名，定义函数的时候，直接将函数赋给变量，通过调用变量来调用函数
--- Created by Administrator.
--- DateTime: 2019/1/6 15:20
---

-- 无参无返函数
function fun1()
    print("无参无返函数")
end
print("调用函数")
fun1()
result = fun1()  --返回的是nil
print(result)

--有参无返函数
function fun2(num1)
    print("有参无返函数 "..num1)
end

fun2(100)
print(num1)     --函数的参数是局部变量

--无参有返函数
function fun3()
    return "你好",2,"111"
end

res1,res2=fun3()
print("res1 "..res1,"res2 "..res2)--返回3个值,可以用 <=3个变量来接收

res1,res2,res3,res4 =fun3()
print(res1,res2,res3,res4)        --返回3个值,最后一个值为 nil

--有参有返函数
function fun4(arg0)
    return arg0+1
end
res4 = fun4(10)
print("res4 "..res4)

--将函数赋值给变量
function fun5()
    print("将函数赋值给变量")
end
res5 = fun5
print("调用变量")
res5()

--函数作为一个参数，进行赋值
print("将函数作为参数")
function PrintInfo(result)
    print("计算的结果")
    print(result)
end

function AddNum(num1,num2,printFunc)
    local res = num1 + num2
    printFunc(res)
end
AddNum(10,1,PrintInfo)




--return 是跳出最外层
function fun6()
    for i = 1, 10 do
        print(i)
        for k = 2, 10 do
            print(k)
            return
        end
    end
end
fun6()

--局部函数
local function LocalFun()
    print("局部函数")
end
LocalFun()

-- 匿名函数
HideFunc = function (num1)
    print("调用了匿名函数")
    print(num1*2)
end
HideFunc(2)


Speek ={}
function Speek.sss()
    print("rrrrr")
end
Speek.sss()