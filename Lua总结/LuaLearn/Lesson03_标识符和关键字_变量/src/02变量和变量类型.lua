--Lua变量
--变量是弱类型，所以不需要指明变量的类型，并且中途可以改变类型，类似Python

num = 10
num = "eee"
print(num)

--变量的多重赋值
num1,num2,num3 = 10,20,40,50
print(num1,num2,num3)  --结果 10 20 40 ，可以连续赋值且一一对应，多余的就忽略
num1,num2,num3 = 10,20
print(num1,num2,num3)  --结果 10 20 nil，可以连续赋值且一一对应，没有的就赋值nil

--变量类型
--1、nil     空类型
--2、boolean 布尔类型	  false、nil 表示为假，其余都为真
--3、string  字符串类型   没有字符类型
--4、number  数字类型	  表示所有数字，没有整数、小数的区分
--5、table	 表类型	      表示一个集合，序号从1开始            类似C#中的数组或集合
--获取变量的类型：type(变量名)

print(type(str)) -- 不进行任何赋值，为nil
str=false
print(type(str))
str="ss"
print(type(str))
str=-1.5
print(type(str))
str={1,2,3,4}
print(type(str))
str=nil
print(type(str))

--- 全局变量 和 局部变量
print("全局变量 和 局部变量")
gNum =10                        --全局变量
local gLocalNum =20             --局部变量
if true then
    local localNum1 = 30        --函数中的局部变量
    gFunNum = 40                --函数中的全局变量
end
print(gNum,gLocalNum,localNum1,gFunNum) --Lua中 默认的变量都是全局变量，包括各种函数中也是全局变量
--                                      只有明确 定义 local 才是局部变量




--Lua 语法
--1、lua语句的结尾 带不带分号都一样
--2、lua没有字符和字符串之分，所以 单引号 和 双引号 的功能一样
--3、Lua 采用了自动内存管理,对于没有使用的内存，会自动清理
--4、Lua 所有的下标都是从 1 开始的


--获取脚本所在的路径
lfs =require("lfs")
path = lfs.currentdir()
print(path)

info = debug.getinfo(1);
path =info.source
print(path)

