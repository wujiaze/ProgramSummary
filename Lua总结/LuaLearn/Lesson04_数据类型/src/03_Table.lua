---
--- Lua 表
--- 表 增加数据
--- Created by Administrator.
--- DateTime: 2019/1/6 20:57
---
table111 = {name = 1,name2 = 2,name3 = 3,name5 =10}

--num = table.maxn(table111)
--print(num)
--
--local tt =table111
--
--function table111.WPeek()
--    print(tt.name)
--end
--
--a = table111
--table111 =nil
--
--a.WPeek()

function table111:ppp()
    print(self.name)
end

a = table111
table111 =nil
a:ppp()