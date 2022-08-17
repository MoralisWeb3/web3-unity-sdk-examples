namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    public enum ReturnType
    {
        Binary,
        Text,
        Image,
        RequiresDeserialization
    }
    public interface ISerializationOption
    {
        ReturnType  ReturnType { get; }
        
        string ContentType { get; }
        
        T Deserialize<T>(string text);
        
    }
}