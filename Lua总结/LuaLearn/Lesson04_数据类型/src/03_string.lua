---
--- Lua 字符串的一般规定
---     定义: 由数字、字母、下划线组成的一串字符
---     1、单行/多行字符串
---     2、字符串连接
---     3、字符串与数字之间的相互转换
---     4、转义字符串
---     5、字符串函数
--- Created by Administrator.
--- DateTime: 2019/1/5 16:18
---

print("单行/多行字符串")
strSingle = "单行字符串"
stringMul = [[
第一行
     第二行
           第三行]]
print(strSingle)
print(stringMul)

--字符串连接
print("字符串连接..")
str = "11"
print("str = "..str)  --Lua中 .. 用来连接字符串，C#中的 + 号其实采用了重载方法，但是Lua没有这么复杂，直接使用..来连接
print(1 .."number")   --数字在前面，需要空一格
print("number"..1)

--字符串与数字之间的相互转换
print("字符串与数字之间的相互转换")
---隐式转换
--字符串转数字
str1 = "22"
str2 = "3.2"
value = str1 + str2
print(value)       --Lua中 + 只能用来做数字之间的运算，又因为Lua的变量可以改变类型，所以字符串会转换数字，最后进行相加
print(type(value)) --最后是数字类型，注意点：必须是能转换成数字的字符串，否则出错
--数字转字符串
num =99
print("数字: "..num)
---显示转换
str = "3.14"
num2 = tonumber(str)
print(num2..type(num2))
num4 =19
str19 = tostring(num4)
print(str19..type(str19))

---只能使用显示转换的情况
table = {1,"2",3}
print(tostring(table)) -- Table 不能隐式自动转字符串，强转字符串得到的是内存地址
n = nil
print("N = "..tostring(n))         -- nil 不能隐式自动转字符串，必须显示转字符串

--转义字符串
print("你\r好")        --回车
print("Hello\nWorld")  --换行
print("H\r\nH")        --回车换行
print("Path\\a")       --反斜杠
print("你\"好\"")      --双引号

---字符串函数
str1 = "luaC#Python"
str2 = "SQLiteManager"

--查找字符串
res1 = string.find(str2,"a")        --从下标1开始，返回第一个匹配项
res2 = string.find(str2,"a",9) --从下标9开始，返回第一个匹配项
print(res1,res2)

--获取字符串长度，和C#不同，得到的是字节的数量，而不是字符的数量
str3 = "222"
print(string.len(str3))
print(#str3) --长度为3
str4="你好"
print(string.len(str4))
print(#str4) --长度为9，根据所占的字节计算，这里是UTF8编码，一个中文一般占3个字节

--字符串大小写转换
print(string.upper(str1))
print(string.lower(str2))

--字符串截取
result1 = string.sub(str1,2,2)  --从下标2开始，截取2的长度
result2 = string.sub(str1,2)       --从下标2开始，截取剩余全部
print(result1)
print(result2)

--字符串反转
resultReverse = string.reverse(str1)
print(resultReverse)

--字符串格式化输出
--%d :占位符，表示数值类型
--%s :占位符，表示字符串类型
str = string.format("你是谁%d,我是%s",1,"2")
print(str)

