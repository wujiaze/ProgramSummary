using ProtoBuf;

[ProtoContract]
public class AccountDto 
{
    [ProtoMember(1)]
    public string Account { get; set; }
    
}
