using System.Drawing;

namespace Edelstein.Protocol.Parsing.Templates.Fields
{
    public interface IFieldTemplate
    {
        FieldOpt Limit { get; }
        Rectangle Bounds { get; }

        int? FieldReturn { get; }
        int? ForcedReturn { get; }

        string ScriptFirstUserEnter { get; }
        string ScriptUserEnter { get; }

        double MobRate { get; }
        int MobCapacityMin { get; }
        int MobCapacityMax { get; }
    }
}
