using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        UserDto dto = new UserDto()
        {
            Id = 10,
            Name = "my",
            Acc = new AccountDto() { Account = "我" },
            AccArr = new AccountDto[] { new AccountDto() { Account = "它" } },
            Dicts = new Dictionary<string, AccountDto>(),
            Lists = new List<AccountDto>() { new AccountDto() { Account = "Helloworld!" } },
            Pos = new MyStruct() { X = 1, Y = 2 }
        };
        dto.Dicts.Add("11", new AccountDto() { Account = "10101" });

        // 本地文件
        // 序列化
        using (FileStream fs = File.Create(Application.dataPath + "/user.bin"))
        {
            //RuntimeTypeModel.Default.Serialize();
            ProtoBuf.Serializer.Serialize(fs, dto);
        }

        //反序列化
        using (FileStream fs = File.OpenRead(Application.dataPath + "/user.bin"))
        {
            UserDto temp = ProtoBuf.Serializer.Deserialize<UserDto>(fs);

            print(temp.Id);
            print(temp.Name);
            print(temp.Acc.Account);
            print(temp.AccArr[0].Account);
            print(temp.Dicts["11"].Account);
            print(temp.Lists[0].Account);
            print(temp.Pos.X + "   " + temp.Pos.Y);
        }

        // 网络
        print("wangluo");
        //PbSerialize(dto);
        UserDto Dtoa = PbDeserialize<UserDto>(PbSerialize(dto));
        print(Dtoa.Id);
        print(Dtoa.Name);
        print(Dtoa.Acc.Account);
        print(Dtoa.AccArr[0].Account);
        print(Dtoa.Dicts["11"].Account);
        print(Dtoa.Lists[0].Account);
        print(Dtoa.Pos.X + "   " + Dtoa.Pos.Y);
    }
    private byte[] PbSerialize(object value)
    {
        if (value == null)
            return null;
        byte[] buffer = null;
        using (MemoryStream ms = new MemoryStream())
        {
            Serializer.Serialize(ms, value);
            buffer = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, buffer, 0, (int)ms.Length);
        }
        return buffer;
    }

    // TODO 泛型委托
    private T PbDeserialize<T>(byte[] buffer)
    {
        T t = default(T);
        using (MemoryStream ms = new MemoryStream(buffer))
        {
            t = Serializer.Deserialize<T>(ms);
        }
        return t;
    }
}
