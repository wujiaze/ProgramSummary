---
--- Lua 的数据类型
---     1、nil      空类型
---     2、number   数值类型
---     3、string   字符串类型
---     4、boolean  布尔类型
---     5、table    表类型
---     6、function 函数类型
---     7、userdata 表示任意存储在变量中的C数据类型
---     8、thread   线程类型(在Lua其实是一种协程的概念)
---
--- 特别注意：类似 C#的引用类型、值类型
---          1、nil、string、number、boolean  类似 值类型
---          2、table、function、userdata     类似 引用类型
---          3、重点：当变量被赋值为 nil 时， 变量 类似值类型 ，则释放了栈内存
---                                        变量 类似引用类型，则删除了引用，当某个内存地址的引用数为0时，Lua内存自动会清理
---          4、变量赋值变量时，类似值类型  ，则复制了一份内存
---                           类似引用类型，则复制了一个引用，指向同一个内存
--- Created by Administrator.
--- DateTime: 2019/1/5 17:24
---

print("字符串类型  "..type("你好"))
print("数值类型  "..type(11))
print("布尔类型  "..type(true))
print("nil类型  "..type(nil))
print("table类型  "..type({10,20,1}))

function fun()
    return 10
end
print("函数类型  "..type(fun))


print("测试nil")
str = 10
str = nil

nnn1 = 10
nnn2 = nnn1
nnn2 = nil
print(nnn1)

nnn3 = {10,10,10}
nnn4 = nnn3
print(nnn4)
nnn4 = nil
print(nnn3)