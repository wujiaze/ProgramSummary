---
--- Lua语言流程控制语句
---
---     1、if 判断语句
---         1、单分支语句
---         2、双分支语句
---         3、多分支语句
---     2、while  循环语句
---     3、repeat 循环语句
---     4、for    循环语句
---         1、一般循环
---         2、ipairs/pairs 迭代循环(数值循环、泛型循环)
---     5、循环终止关键字 break ,只能跳出当前循环, 类似 C#中的 break
---               关键字 return,跳出最外层，不仅仅是用在循环，类似 C# 中的return
---         Lua中没有类似 C#中continue的关键字
--- Created by Administrator.
--- DateTime: 2019/1/6 11:04
---

-- if语句
print("单分支语句")
num1 = 30
num2 = 20
if (num1<num2) then     -- 可以不加小括号
    print("if 单分支语句")
end

print("双分支语句")
if num1<num2 then
    print("num1 < num2")
else
    print("num2 > num1 ")
end

print("多分支语句")
if false then
    print("分支1")
elseif false then
    print("分支2")
else
    print("分支3")
end

--while语句
index = 1
while index<=100 do
    print(index)
    index = index + 1     -- Lua语言没有 ++ -- 计算
end

--repeat 语句   类似C#的 DoWhile语句,不同在于，Lua是满足条件跳出循环，C#是不满足条件跳出循环
print("repeat")
repeat
    print(index)
    index = index-1
until index < 100

--for循环语句
-- for 变量名称=变量初始值，结束值(包含)，(步长,可正可负)
for i = 1, 10 do        --这里的 变量i 是局部变量
    print(i)
end

for j = 10, 1, -4 do
    print(j)
end


--for + ipairs/pair 构成的循环，类似C#的 Foreach 循环
table1 = {10,20,30,40,50}       --类似C# 中的数组
for k in ipairs(table1) do      --一般就采用 ipairs
    print(k)                    --只能输出下标
end
for k, v in ipairs(table1) do
    print(k,v)                  --输出下标和值
end
for k, v in pairs(table1) do    --也可以采用 pairs
    print(k,v)
end

table2 = {10,nil,20}            --数组中有nil，表示序号中断
for k, v in ipairs(table2) do   --ipairs，遇到nil直接停止
    print(k,v)
end
for k, v in pairs(table2) do    --pairs，遇到nil，直接跳过继续
    print(k,v)
end

table3 = {num1 = "1",num2 = "2",num3 = 3} --类似C#中的字典
for k, v in pairs(table3) do    --只能使用pairs
    print(k,v)
end

print("测试 break 关键字")
for i = 1, 10 do
    print("i "..i)
    for j = 1, 10 do
        print("j "..j)
        for k = 1, 10 do
            print("k "..k)
            break
        end
        break
    end
    break
end

print("测试 return 关键字")
for i = 1, 10 do
    print("i "..i)
    for j = 1, 10 do
        print("j "..j)
        for k = 1, 10 do
            print("k "..k)
            return
        end
    end
end