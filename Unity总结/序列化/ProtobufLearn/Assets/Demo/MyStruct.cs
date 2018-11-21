using ProtoBuf;

[ProtoContract]
public struct MyStruct 
{
    [ProtoMember(1)]
    public int X { get; set; }
    [ProtoMember(2)]
    public int Y { get; set; }
}
