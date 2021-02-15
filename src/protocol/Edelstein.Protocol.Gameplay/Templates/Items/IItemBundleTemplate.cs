namespace Edelstein.Protocol.Parsing.Templates.Items
{
    public interface IItemBundleTemplate : IItemTemplate
    {
        double UnitPrice { get; }
        short MaxPerSlot { get; }
    }
}
