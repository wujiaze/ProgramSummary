---
--- 运算符
---
--- 算数运算符:  +  -  *  /   %  ^
--- 关系运算符： < <= > >=  == ~=
--- 逻辑运算符： and  or  not
---
--- Created by Administrator.
--- DateTime: 2019/1/5 18:40
---

--- 算数运算符
num1 = 10
num2 = 3
res1 = num1 + num2  --加法
res2 = num1 - num2  --减法
res3 = num1 * num2  --乘法
res4 = num1 / num2  --除法(相当于C#中float除法)
res5 = num1 % num2  --取余
res6 = num1 ^ num2  --次幂
print("加法: "..res1)print("减法: "..res2)print("乘法: "..res3)
print("除法: "..res4)print("取余: "..res5)print("次幂: "..res6)

--- 关系运算符
--数值类型比较
print("num1>num2: "..tostring(num1>num2))
print("num1~=num2: "..tostring(num1~=num2)) --数值类型比较，比较数据的大小
--string类型比较
str1 = "你好"
str2 = "你好"
str3 = str1
print("str1 == str2: "..tostring(str1==str2))
print("str1 ~= str3: "..tostring(str1~=str3)) --string类型比较，比较数据的内容
--boolean类型比较
bool1 = false
bool2 = true
print("bool1 == bool2 "..tostring(bool1 == bool2)) --boolean类型比较，比较数据
--nil类型比较
value1 = nil
value2 = nil
print("value1 == value2 "..tostring(value1 ==value2)) --nil类型，只有nil=nil，其他都不会相等
--table类型
table1 = {1,2,3}
table2 = {1,2,3}
print("table1 == table2 "..tostring(table1 == table2)) --table类型，Lua中是比较引用是否相同
--function、userdata 也是比较引用是否相同，只有当变量引用同一个内存时，才相等

---逻辑运算符 --真假判断
--- 1、只有 false nil 为假，其余为真
--- 2、and: 第一个操作数为假则返回第一个操作数，不然返回第二个操作数
--- 3、or : 第一个操作数为真则返回第一个操作数，不然返回第二个操作数
--- 4、not: 操作数为假则返回真，操作数为真则返回假
--- 5、有短路规则(和C#一样)，所以不要在逻辑运算中，改变变量的值
--- 6、注意：Lua 中 可以有各种类型 用来做逻辑判断，所以返回的是操作数
---         C#中只有false 和 true 用来做逻辑判断，返回的也是操作数，但是只有 false 或 true 两个操作数，所以看上去返回的是false 或 true

num1 = 10
num2 = 20
num3 = 30
print(num1>num2 and num2 < num3) --第一个操作数为:false 代表假 ,第二个操作数为:true 代表真,根据 and 的规则，返回第一个操作数 false
print(num1>num2 or num2 < num3)  --第一个操作数为:false 代表假 ,第二个操作数为:true 代表真,根据 or  的规则，返回第二个操作数 true
print(not(num1>num2))            --操作数为:false 代表假,根据 not 的规则，返回真,用 true  表示
print(not(nil))                  --操作数为:nil   代表假,根据 not 的规则，返回真,用 true  表示
print(not(false))                --操作数为:false 代表假,根据 not 的规则，返回真,用 true  表示
print(not("ni"))                 --操作数为:"ni"  代表真,根据 not 的规则，返回假,用 false 表示
print(not(1))                    --操作数为:1     代表真,根据 not 的规则，返回假,用 false 表示

print(10 and 20)                 --第一个操作数为:10    代表真 ,第二个操作数为:20 代表真,根据 and 的规则，返回第二个操作数 20
print(nil and 20)                --第一个操作数为:nil   代表假 ,第二个操作数为:20 代表真,根据 and 的规则，返回第一个操作数 nil
print(false and 20)              --第一个操作数为:false 代表假 ,第二个操作数为:20 代表真,根据 and 的规则，返回第一个操作数 false
print(10 or 20)                  --第一个操作数为:10    代表真 ,第二个操作数为:20 代表真,根据 or  的规则，返回第一个操作数 10
print(false or 20)               --第一个操作数为:false 代表假 ,第二个操作数为:20 代表真,根据 or  的规则，返回第二个操作数 20