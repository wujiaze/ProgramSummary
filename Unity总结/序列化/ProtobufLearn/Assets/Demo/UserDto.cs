/*
 *      Protobuf 用法
 *      
 *      1、序列化的类，必须有 [ProtoContract] 特性，所以一些Unity自带的类和结构体就不能使用了
 *      2、类中的成员 最好是 public ，internal 也行
 *      3、类中的成员 必须带有  [ProtoMember()] 特性，一般参数使用 tag 值
 *                   目的是:区分变量名对应的内存
 *                   条件：1、tag >=1
 *                        2、不同的类中的tag值可以相同，同一个类中tag值不能相同
 *                        3、同一个字段，在序列化时和反序列化时tag要保持一致，不然匹配不上就为null   
 *                        
 *      注意：不支持的类型  ArrayList  、 int[][] 各种交错数组
 */
using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;

// 使用 protobuf 序列化类对象，需要添加这个特性
[ProtoContract]
public class UserDto
{
    // 同一个类中 tag值 不能相同
    [ProtoMember(1)]
    internal int Id { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; }
    [ProtoMember(3)]
    public AccountDto Acc { get; set; }
    [ProtoMember(4)]
    public List<AccountDto> Lists { get; set; }
    [ProtoMember(5)]
    public Dictionary<string, AccountDto> Dicts { get; set; }
    [ProtoMember(7)]
    public AccountDto[] AccArr { get; set; }
    [ProtoMember(8)]
    public MyStruct Pos { get; set; }
}
